using ReserveAqui.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.Domain.DTO
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public TimeSpan HorarioChegadaEsperada { get; set; }
        public DateTime DataReserva { get; set; }
        public ClienteDto Cliente { get; set; }
        public int? Avaliacao { get; set; }
        public StatusDto Status { get; set; }
        public List<MesaDto> Mesas { get; set; }
        public List<TurnoDto> Turnos { get; set; }
    }

    public static class ReservaDtoExtension
    {
        public static List<ReservaDto> MapearReservas(this IEnumerable<Reserva> entidades)
        {
            if (entidades == null || !entidades.Any())
                return new List<ReservaDto>();

            return entidades.Select(MapearReserva).ToList();
        }

        public static ReservaDto MapearReserva(this Reserva entidade)
        {
            if (entidade == null)
                return null;

            return new ReservaDto()
            {
                Id = entidade.Id,
                Avaliacao = entidade.Avaliacao,
                DataReserva = entidade.DataReserva,
                HorarioChegadaEsperada = entidade.HorarioChegadaEsperada,
                Cliente = new ClienteDto() { IdCliente = entidade.IdCliente },
                Status = new StatusDto { Id = entidade.IdStatus },
                Mesas = MapearMesas(entidade.IdMesas),
                Turnos = MapearTurnos(entidade.IdTurnos)
            };
        }
        public static Reserva ConverterParaEntidade(this ReservaDto entidade)
        {
            if (entidade == null)
                return null;

            return new Reserva()
            {
                Id = entidade.Id,
                Avaliacao = entidade.Avaliacao,
                DataReserva = entidade.DataReserva,
                HorarioChegadaEsperada = entidade.HorarioChegadaEsperada,
                IdCliente = entidade.Cliente.IdCliente,
                IdStatus = entidade.Status.Id,
                IdMesas = ConverterMesasParaEntidade(entidade.Mesas),
                IdTurnos = ConverterTurnosParaEntidade(entidade.Turnos)
            };

        }
        private static List<MesaDto> MapearMesas(List<int> idsMesas)
        {
            if (idsMesas == null || !idsMesas.Any())
                return new List<MesaDto>();

            return idsMesas.Select(idMesa => new MesaDto { Id = idMesa }).ToList();
        }

        private static List<TurnoDto> MapearTurnos(List<int> idsTurnos)
        {
            if (idsTurnos == null || !idsTurnos.Any())
                return new List<TurnoDto>();

            return idsTurnos.Select(idTurno => new TurnoDto { Id = idTurno }).ToList();
        }
        private static List<int> ConverterMesasParaEntidade(List<MesaDto> mesas)
        {
            if (mesas == null || !mesas.Any())
                return new List<int>();

            return mesas.Select(mesa => mesa.Id).ToList();
        }

        private static List<int> ConverterTurnosParaEntidade(List<TurnoDto> turnos)
        {
            if (turnos == null || !turnos.Any())
                return new List<int>();

            return turnos.Select(turno => turno.Id).ToList();
        }

    }
}
