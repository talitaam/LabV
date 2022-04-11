using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ReserveAqui.Shared.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static TConfig ConfigureAppConfiguration<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }

        public static TConfig ConfigureAppConfiguration<TConfig>(this IServiceCollection services, IConfiguration configuration, Func<TConfig> provider) where TConfig : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            var config = provider();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
