using ReserveAqui.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.WebApi.Model
{
    public class ReservaModel
    {
        public int Id { get; set; }
        public string HorarioChegadaEsperada { get; set; }
        public DateTime DataReserva { get; set; }
        public ClienteDto Cliente { get; set; }
        public int? Avaliacao { get; set; }
        public StatusDto Status { get; set; }
        public List<MesaModel> Mesas { get; set; }
        public List<TurnoModel> Turnos { get; set; }
    }

    public static class ReservaDtoExtension
    {
        public static List<ReservaModel> MapearReservas(this List<ReservaDto> dtos)
        {
            if (dtos == null || !dtos.Any())
                return new List<ReservaModel>();

            return dtos.Select(MapearReserva).ToList();
        }

        public static ReservaModel MapearReserva(this ReservaDto dto)
        {
            if (dto == null)
                return null;

            return new ReservaModel()
            {
                Id = dto.Id,
                DataReserva = dto.DataReserva,
                HorarioChegadaEsperada = dto.HorarioChegadaEsperada.ToString(),
                Cliente = dto.Cliente,
                Avaliacao = dto.Avaliacao,
                Mesas = dto.Mesas.MapearMesas(),
                Turnos = dto.Turnos.MapearTurnos(),
                Status = dto.Status
            };
        }
    }
}
