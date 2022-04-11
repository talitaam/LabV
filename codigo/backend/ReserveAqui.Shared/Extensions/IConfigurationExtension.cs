using Microsoft.Extensions.Configuration;

namespace ReserveAqui.Shared.Extensions
{
    public static class IConfigurationExtension
    {
        public static IConfiguration ObterConfiguracao(this IConfiguration configuration)
        {
            return configuration.GetSection("AppConfiguration");
        }

        public static IConfiguration ObterConfiguracao(this IConfiguration configuration, string chave)
        {
            return configuration.GetSection($"AppConfiguration:{chave}");
        }

        public static IConfiguration ObterConfiguracao(this IConfiguration configuration, params string[] chaves)
        {
            return configuration.GetSection($"{string.Join(':', chaves)}");
        }

        public static TSection ObterConfiguracao<TSection>(this IConfiguration configuration, string chave)
        {
            return configuration.GetSection($"AppConfiguration:{chave}").Get<TSection>();
        }

        public static TSection ObterConfiguracao<TSection>(this IConfiguration configuration, params string[] chaves)
        {
            return configuration.GetSection($"{string.Join(':', chaves)}").Get<TSection>();
        }

        public static string ObterSecao(this IConfiguration configuration, string chave)
        {
            return configuration.GetSection("AppConfiguration").GetSection(chave).Value;
        }

    }
}
