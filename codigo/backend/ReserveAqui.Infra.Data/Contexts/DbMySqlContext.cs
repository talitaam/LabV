using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ReserveAqui.Infra.Data.Contexts.Base;
using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Shared.Extensions;

namespace ReserveAqui.Infra.Data.Contexts
{
    public class DbMySqlContext : DbContextBase, IDbMySqlContext
    {
        public DbMySqlContext(IConfiguration configuration) : base()
        {
            Conexao = new MySqlConnection(configuration.ObterConfiguracao<string>("AppConfiguration", "ConexaoMySql"));
        }
    }
}
