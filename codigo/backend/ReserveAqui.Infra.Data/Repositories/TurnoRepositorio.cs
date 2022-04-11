using Dapper;
using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Infra.Data.Repositories.Base;
using ReserveAqui.Infra.Data.Util;
using System.Collections.Generic;
using System.Data;

namespace ReserveAqui.Infra.Data.Repositories
{
    public class TurnoRepositorio : RepositorioBase<Turno>, ITurnoRepositorio
    {
        public TurnoRepositorio(IDbMySqlContext contexto) : base(contexto) { }

        private const string SQL_LISTAR_TURNOS = @"
            SELECT 
                id_turno AS Id,
                descricao AS Descricao,
                horario_inicio AS HorarioInicio,
                horario_fim AS HorarioFim
            FROM turno";

        private const string SQL_LISTAR_TURNOS_POR_ID = @"
            SELECT 
                id_turno AS Id,
                descricao AS Descricao,
                horario_inicio AS HorarioInicio,
                horario_fim AS HorarioFim
            FROM turno
            WHERE id_turno IN ##IdsTurnos##";

        private const string SQL_INSERIR_TURNO = @"
            INSERT INTO turno 
                (descricao, horario_inicio, horario_fim) 
            VALUES
                (@descricao, @horarioInicio, @horarioFim);";

        private const string SQL_ATUALIZAR_TURNO = @"
            UPDATE turno 
               SET descricao = @descricao, horario_inicio = @horarioInicio, horario_fim = @horarioFim
            WHERE id_turno = @id";

        private const string SQL_OBTER_POR_ID = @"
            SELECT 
                id_turno AS Id,
                descricao AS Descricao,
                horario_inicio AS HorarioInicio,
                horario_fim AS HorarioFim
            FROM turno
            WHERE id_turno = @id";

        private const string SQL_DELETAR_TURNO = @"
            DELETE FROM turno 
            WHERE id_turno = @id";

        public IEnumerable<Turno> Listar()
        {
            return Listar(SQL_LISTAR_TURNOS, null);
        }

        public IEnumerable<Turno> ListarPorIds(List<int> idsTurnos)
        {
            var parametroIdsTurnos = SqlUtil.FormatarListaParametrosInteiros(idsTurnos);
            var sql = SQL_LISTAR_TURNOS_POR_ID.Replace("##IdsTurnos##", parametroIdsTurnos);

            return Listar(sql, null);
        }

        public Turno ObterPorId(int id)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@id", id, DbType.Int32);

            return Obter(SQL_OBTER_POR_ID, parametros);
        }

        public void Inserir(Turno turno)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@descricao", turno.Descricao, DbType.AnsiString);
            parametros.Add("@horarioInicio", turno.HorarioInicio, DbType.Time);
            parametros.Add("@horarioFim", turno.HorarioFim, DbType.Time);

            Execute(SQL_INSERIR_TURNO, parametros);
        }

        public void Atualizar(Turno turno)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@id", turno.Id, DbType.Int32);
            parametros.Add("@descricao", turno.Descricao, DbType.AnsiString);
            parametros.Add("@horarioInicio", turno.HorarioInicio, DbType.Time);
            parametros.Add("@horarioFim", turno.HorarioFim, DbType.Time);

            Execute(SQL_ATUALIZAR_TURNO, parametros);
        }

        public void Deletar(int id)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@id", id, DbType.Int32);

            Execute(SQL_DELETAR_TURNO, parametros);
        }
    }
}
