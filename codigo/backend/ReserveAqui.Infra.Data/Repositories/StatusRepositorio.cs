using Dapper;
using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Infra.Data.Repositories.Base;
using System.Collections.Generic;
using System.Data;

namespace ReserveAqui.Infra.Data.Repositories
{
    public class StatusRepositorio : RepositorioBase<Status>, IStatusRepositorio
    {
        public StatusRepositorio(IDbMySqlContext contexto) : base(contexto) { }

        private const string SQL_OBTER_STATUS_POR_ID = @"
            SELECT 
                Id_status AS ID,
                Descricao AS Descricao
            FROM Status
            where id_status = @id";

        private const string SQL_LISTAR_STATUS = @"
            SELECT 
                Id_status AS ID,
                Descricao AS Descricao
            FROM Status";

        public List<Status> Listar()
        {
            return Listar(SQL_LISTAR_STATUS, null);
        }

        public Status ObterPorId(int id)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32);

            return Obter(SQL_OBTER_STATUS_POR_ID, parametros);
        }
    }
}
