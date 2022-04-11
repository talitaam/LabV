using Dapper;
using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Infra.Data.Repositories.Base;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ReserveAqui.Infra.Data.Repositories
{
    public class DiaFuncionamentoRepositorio : RepositorioBase<DiaFuncionamento>, IDiaFuncionamentoRepositorio
    {
        private const string SQL_LISTAR_DIAS_FUNCIONAMENTO = @"
            SELECT 
                dia_descricao AS DescricaoDia,
                id_dia AS DiaSemana,
                ativo AS Ativo
            FROM dias_funcionamento;";

        private const string SQL_OBTER_DIAS_FUNCIONAMENTO_POR_DIA_SEMANA = @"
            SELECT 
                dia_descricao AS DescricaoDia,
                id_dia AS DiaSemana,
                ativo AS Ativo
            FROM dias_funcionamento
            WHERE id_dia = @diaSemana ;";

        private const string SQL_ATUALIZAR_DIA_FUNCIONAMENTO = @"
            UPDATE dias_funcionamento 
            SET ATIVO = @ativo
            WHERE id_dia = @id ;";

        public DiaFuncionamentoRepositorio(IDbMySqlContext contexto) : base(contexto) { }

        public IEnumerable<DiaFuncionamento> ListarDiasFuncionamento()
        {
            return Listar(SQL_LISTAR_DIAS_FUNCIONAMENTO, null);
        }

        public void AtualizarDiaFuncionamento(int idDia, bool ativo)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@ativo", ativo, DbType.Boolean);
            parametros.Add("@id", idDia, DbType.Int32);

            Execute(SQL_ATUALIZAR_DIA_FUNCIONAMENTO, parametros);
        }

        public DiaFuncionamento ObterDiaFuncionamento(int diaSemana)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@diaSemana", diaSemana, DbType.Int32);

            return Obter(SQL_OBTER_DIAS_FUNCIONAMENTO_POR_DIA_SEMANA, parametros);
        }

    }
}
