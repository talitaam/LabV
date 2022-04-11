using ReserveAqui.WebApi.Model;
using ReserveAqui.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace ReserveAqui.WebApi.Util
{
    public class GlobalExceptionHandler
    {
        public async Task Invoke(HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception == null) return;

            ErroModel erro;

            switch (exception)
            {
                case ParametroInvalidoException ex:
                    context.Response.StatusCode = 400;
                    erro = new ErroModel(ex);
                    break;
                case PermissaoNegadaException ex:
                    context.Response.StatusCode = 401;
                    erro = new ErroModel(ex);
                    break;
                case AplicacaoException ex:
                    context.Response.StatusCode = 500;
                    erro = new ErroModel(ex);
                    break;
                default:
                    context.Response.StatusCode = 500;
                    erro = new ErroModel("Erro ao executar operação.", exception);
                    break;
            }

            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var json = JsonConvert.SerializeObject(erro, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await context.Response.WriteAsync(json);
        }
    }
}
