using ReserveAqui.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveAqui.WebApi.Model
{
    public class TurnoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioFim { get; set; }
    }

    public static class TurnoDtoExtension
    {
        public static List<TurnoModel> MapearTurnos(this List<TurnoDto> entidades)
        {
            if (entidades == null || !entidades.Any())
                return new List<TurnoModel>();

            return entidades.Select(MapearTurno).ToList();
        }

        public static TurnoModel MapearTurno(this TurnoDto dto)
        {
            if (dto == null)
                return null;

            return new TurnoModel()
            {
                Id = dto.Id,
                Descricao = dto.Descricao,
                HorarioInicio = dto.HorarioInicio.ToString(),
                HorarioFim = dto.HorarioFim.ToString()
            };
        }
    }
}
