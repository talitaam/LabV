using Dapper;
using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Infra.Data.Repositories.Base;
using System.Collections.Generic;
using System.Data;
using System;
using System.Text;

namespace ReserveAqui.Infra.Data.Repositories
{
    public class ReservaRepositorio : RepositorioBase<Reserva>, IReservaRepositorio
    {
        public ReservaRepositorio(IDbMySqlContext contexto) : base(contexto) { }

        private const string SQL_LISTAR_RESERVAS_POR_LOGIN_CLIENTE = @"
            SELECT 
                id_reserva AS Id,
                horario_chegada_esperada AS HorarioChegadaEsperada,
                data_reserva AS DataReserva,
                cliente_id_cliente AS IdCliente,
                Avaliacao AS Avaliacao
            FROM reserva r
            INNER JOIN cliente c
            ON r.cliente_id_cliente = c.id_cliente
            WHERE c.login = @login
            ORDER BY data_reserva desc, id_reserva desc";

        private const string SQL_LISTAR_TODAS_RESERVAS = @"
            SELECT 
                id_reserva AS Id,
                horario_chegada_esperada AS HorarioChegadaEsperada,
                data_reserva AS DataReserva,
                cliente_id_cliente AS IdCliente,
                Avaliacao AS Avaliacao
            FROM reserva";

        private const string SQL_LISTAR_IDS_MESAS_RESERVA = @"
            SELECT
                mesa_id_mesa As IdMesa,
                id_status As IdStatus
            FROM mesa_reserva
            WHERE reserva_id_reserva = @idReserva";

        private const string SQL_LISTAR_IDS_TURNOS_RESERVA = @"
            SELECT
                turno_id_turno As IdTurno
            FROM reserva_turno
            WHERE reserva_id_reserva = @idReserva";

        private const string SQL_OBTER_RESERVA_POR_ID = @"
            SELECT id_reserva AS Id, 
                   horario_chegada_esperada AS Horario_Chegada_Esperado, 
                   data_reserva AS Data_da_Reserva,
                   cliente_id_cliente AS IdCliente,
    	           avaliacao AS Avaliacao
            FROM reserveaqui.reserva
            WHERE id_reserva = @idReserva";

        private const string SQL_INSERIR_RESERVA = @"
            START TRANSACTION;
            INSERT INTO reserveaqui.reserva
                (horario_chegada_esperada, data_reserva, cliente_id_cliente, avaliacao)
            VALUES
                (@horarioChegadaEsperada, @dataReserva, @idCliente, @avaliacao);

            SET @idReserva=(SELECT id_reserva FROM reserveaqui.reserva WHERE data_reserva = @dataReserva AND cliente_id_cliente = @idCliente and horario_chegada_esperada = @horarioChegadaEsperada LIMIT 1);";

        private const string SQL_ATUALIZAR_RESERVA = @"
            START TRANSACTION;
            UPDATE reserveaqui.reserva
            SET horario_chegada_esperada = @horarioChegadaEsperada, data_reserva = @dataReserva, avaliacao = @avaliacao
            WHERE id_reserva = @idReserva ;";

        private const string SQL_ATUALIZAR_AVALIACAO_RESERVA = @"
            UPDATE reserveaqui.reserva
            SET avaliacao = @avaliacao
            WHERE id_reserva = @idReserva ;";

        private const string SQL_ATUALIZAR_STATUS_MESA_RESERVA = @"
            UPDATE reserveaqui.mesa_reserva
            SET id_status = @idStatus
            WHERE reserva_id_reserva= @idReserva ;";


        public IEnumerable<Reserva> ListarPorLoginCliente(string login)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@login", login, DbType.AnsiString);

            return Listar(SQL_LISTAR_RESERVAS_POR_LOGIN_CLIENTE, parametros);
        }

        public IEnumerable<Reserva> ListarTodasReservas()
        {
            return Listar(SQL_LISTAR_TODAS_RESERVAS, null);
        }

        public IEnumerable<MesaReserva> ListarMesasReservaPorId(int id)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@idReserva", id, DbType.Int32);

            return Query<MesaReserva>(SQL_LISTAR_IDS_MESAS_RESERVA, parametros);
        }

        public IEnumerable<int> ListarTurnosReservaPorId(int id)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@idReserva", id, DbType.Int32);

            return Query<int>(SQL_LISTAR_IDS_TURNOS_RESERVA, parametros);
        }

        public Reserva ObterPorId(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@idReserva", id, DbType.Int32);

            return Obter(SQL_OBTER_RESERVA_POR_ID, parametros);
        }

        public void Inserir(Reserva reserva)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@horarioChegadaEsperada", reserva.HorarioChegadaEsperada, DbType.Time);
            parametros.Add("@dataReserva", reserva.DataReserva, DbType.DateTime);
            parametros.Add("@idCliente", reserva.IdCliente, DbType.Int32);
            parametros.Add("@avaliacao", reserva.Avaliacao, DbType.Int32);

            var SQL_INSERIR_RESERVA_CONCAT = SQL_INSERIR_RESERVA;

            foreach (int idMesa in reserva.IdMesas)
            {
                SQL_INSERIR_RESERVA_CONCAT += $"INSERT INTO reserveaqui.mesa_reserva(mesa_id_mesa, reserva_id_reserva, id_status) VALUES ({idMesa}, @idReserva, {reserva.IdStatus});";
            }

            foreach (int idTurno in reserva.IdTurnos)
            {
                SQL_INSERIR_RESERVA_CONCAT += $"INSERT INTO reserveaqui.reserva_turno(reserva_id_reserva, turno_id_turno) VALUES (@idReserva, {idTurno});";
            }

            SQL_INSERIR_RESERVA_CONCAT += $"COMMIT;";

            Execute(SQL_INSERIR_RESERVA_CONCAT, parametros);
        }

        public void Atualizar(Reserva reserva)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@idReserva", reserva.Id, DbType.Int32);
            parametros.Add("@horarioChegadaEsperada", reserva.HorarioChegadaEsperada, DbType.Time);
            parametros.Add("@dataReserva", reserva.DataReserva, DbType.DateTime);
            parametros.Add("@avaliacao", reserva.Avaliacao, DbType.Int32);

            var sql = new StringBuilder(SQL_ATUALIZAR_RESERVA);

            sql.AppendLine($"DELETE FROM reserveaqui.mesa_reserva where reserva_id_reserva = {reserva.Id};");

            foreach (int idMesa in reserva.IdMesas)
            {
                sql.AppendLine($"INSERT INTO reserveaqui.mesa_reserva(mesa_id_mesa, reserva_id_reserva, id_status) VALUES ({idMesa}, {reserva.Id}, {reserva.IdStatus});");
            }

            sql.AppendLine($"DELETE FROM reserveaqui.reserva_turno where reserva_id_reserva = {reserva.Id};");

            foreach (int idTurno in reserva.IdTurnos)
            {
                sql.AppendLine($"INSERT INTO reserveaqui.reserva_turno(reserva_id_reserva, turno_id_turno) VALUES ({reserva.Id}, {idTurno});");
            }

            sql.AppendLine($"COMMIT;");

            Execute(sql.ToString(), parametros);
        }

        public void AtualizarStatusMesaReserva(int idReserva, int idStatus)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@idReserva", idReserva, DbType.Int32);
            parametros.Add("@idStatus", idStatus, DbType.Int32);

            Execute(SQL_ATUALIZAR_STATUS_MESA_RESERVA, parametros);
        }

        public void AtualizarAvaliacaoReserva(int idReserva, int avaliacao)
        {
            DynamicParameters parametros = new DynamicParameters();

            parametros.Add("@idReserva", idReserva, DbType.Int32);
            parametros.Add("@avaliacao", avaliacao, DbType.Int32);

            Execute(SQL_ATUALIZAR_AVALIACAO_RESERVA, parametros);
        }
    }
}
