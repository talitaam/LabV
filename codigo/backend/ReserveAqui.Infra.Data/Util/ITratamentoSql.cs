using Dapper;

namespace ReserveAqui.Infra.Data.Util
{
    public interface ITratamentoSql
    {
        (string, DynamicParameters) Executar(string sql, DynamicParameters parametros);
    }
}
