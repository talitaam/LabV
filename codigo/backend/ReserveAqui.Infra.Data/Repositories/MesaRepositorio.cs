using Dapper;
using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Infra.Data.Repositories.Base;
using ReserveAqui.Infra.Data.Util;
using ReserveAqui.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Data;

namespace ReserveAqui.Infra.Data.Repositories
{
    public class MesaRepositorio : RepositorioBase<Mesa>, IMesaRepositorio
    {
        public MesaRepositorio(IDbMySqlContext contexto) : base(contexto) { }

        private const string SQL_LISTAR_MESAS = @"
            SELECT 
                id_mesa AS Id,
                qtd_cadeiras AS QuantCadeiras,
                nome_mesa AS NomeMesa
            FROM mesa";

        private static string SQL_LISTAR_MESAS_POR_IDS = $@"
            {SQL_LISTAR_MESAS}
            WHERE id_mesa IN ##idsMesas##";

        private const string SQL_INSERIR_MESA = @"
            INSERT INTO mesa 
                (qtd_cadeiras, nome_mesa) 
            VALUES
                (@QuantCadeiras, @NomeMesa);";

        private const string SQL_ATUALIZAR_MESA = @"
            UPDATE mesa 
               SET qtd_cadeiras = @QuantCadeiras, nome_mesa = @NomeMesa
            WHERE id_mesa = @id";

        private const string SQL_OBTER_POR_ID = @"
            SELECT 
                id_mesa AS Id,
                qtd_cadeiras AS QuantCadeiras,
                nome_mesa AS NomeMesa
            FROM mesa
            WHERE id_mesa = @id";

        private const string SQL_DELETAR_MESA = @"
            DELETE FROM mesa 
            WHERE id_mesa = @id";

        private static readonly string SQL_LISTAR_MESAS_LIVRES = $@"
            SELECT 
                id_mesa AS Id,
                qtd_cadeiras AS QuantCadeiras,
                nome_mesa AS NomeMesa
            FROM mesa 
            WHERE id_mesa not IN (
	            SELECT m.id_mesa from mesa m
	                LEFT JOIN mesa_reserva mr on m.id_mesa = mr.mesa_id_mesa
	                LEFT JOIN reserva r on r.id_reserva = mr.reserva_id_reserva
	                LEFT JOIN reserva_turno rt ON r.id_reserva = rt.reserva_id_reserva
	            WHERE mr.id_status IN ({StatusEnum.RESERVADO}, {StatusEnum.EM_USO}) AND r.data_reserva = @dataReserva AND rt.turno_id_turno = @IdTurno ##DESCONSIDERAR_RESERVA##)";

        public IEnumerable<Mesa> Listar()
        {
            return Listar(SQL_LISTAR_MESAS, null);
        }

        public IEnumerable<Mesa> ListarMesasDisponiveisPorTurno(DateTime dataReserva, int idTurno, int? idReservaDesconsiderada = null)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@dataReserva", dataReserva, DbType.DateTime);
            parametros.Add("@IdTurno", idTurno, DbType.Int32);
            var sql = SQL_LISTAR_MESAS_LIVRES;
            if (idReservaDesconsiderada != null)
            {
                sql = sql.Replace("##DESCONSIDERAR_RESERVA##", "AND r.id_reserva <> @IdReserva");
                parametros.Add("@IdReserva", idReservaDesconsiderada, DbType.Int32);
            }
            else
            {
                sql = sql.Replace("##DESCONSIDERAR_RESERVA##", string.Empty);
            }

            return Listar(sql, parametros);
        }

        public IEnumerable<Mesa> ListarPorIds(List<int> idsMesas)
        {
            var parametroIdsMesas = SqlUtil.FormatarListaParametrosInteiros(idsMesas);
            var sql = SQL_LISTAR_MESAS_POR_IDS.Replace("##idsMesas##", parametroIdsMesas);

            return Listar(sql, null);
        }

        public Mesa ObterPorId(int id)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@id", id, DbType.Int32);

            return Obter(SQL_OBTER_POR_ID, parametros);
        }

        public void Inserir(Mesa mesa)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@quantCadeiras", mesa.QuantCadeiras, DbType.Int32);
            parametros.Add("@nomeMesa", mesa.NomeMesa, DbType.AnsiString);
            

            Execute(SQL_INSERIR_MESA, parametros);
        }

        public void Atualizar(Mesa mesa)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@id", mesa.Id, DbType.Int32);
            parametros.Add("@quantCadeiras", mesa.QuantCadeiras, DbType.Int32);
            parametros.Add("@nomeMesa", mesa.NomeMesa, DbType.AnsiString);

            Execute(SQL_ATUALIZAR_MESA, parametros);
        }

        public void Deletar(int id)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@id", id, DbType.Int32);

            Execute(SQL_DELETAR_MESA, parametros);
        }
    }
}
