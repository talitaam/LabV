using Autofac;
using AutoMapper;
using ReserveAqui.Infra.CrossCutting.IoC;
using ReserveAqui.Shared.Configuracoes;
using ReserveAqui.Shared.Extensions;
using ReserveAqui.WebApi.SignalRChat;
using ReserveAqui.WebApi.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;

namespace ReserveAqui.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            GeradorJwt.Configurar(Configuration.ObterSecao("JwtSegredo"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
            services.AddControllers();
            services.AddLogging();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSignalR(options => {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(300);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ReserveAqui WebApi",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = @"Ainda NÃO implementado!! Copie no campo abaixo a palavra 'bearer' seguida do token JWT de autenticação. (Ex: bearer {JWT Token}).",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] {}
                    }
                });
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(GeradorJwt.Chave),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                //options.Authority = /* TODO: Insert Authority URL here */;

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
                        {
                            context.Token = accessToken;
                            //context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
                        }

                        return Task.CompletedTask;
                    }
                };

            });
            services.AddOptions();
            services.ConfigureAppConfiguration<Aplicacao>(Configuration.ObterConfiguracao());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new GlobalExceptionHandler().Invoke
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat", options =>
                {
                    options.Transports = HttpTransportType.WebSockets;
                    options.WebSockets.CloseTimeout = TimeSpan.FromMinutes(10);
                });
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
#if DEBUG
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "ReserveAqui WebApi");
#else
                c.SwaggerEndpoint($"{(env?.EnvironmentName?.ToLower() == "desenvolvimento" ? string.Empty : string.Empty)}/swagger/v1/swagger.json", "ReserveAqui WebApi");
#endif
                c.RoutePrefix = string.Empty;
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddAutofacServiceProvider();
            builder.RegisterType<ChatManager>().InstancePerLifetimeScope();
        }
    }
}
