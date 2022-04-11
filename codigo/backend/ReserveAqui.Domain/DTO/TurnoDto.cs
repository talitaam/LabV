using ReserveAqui.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.Domain.DTO
{
    public class TurnoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFim { get; set; }
    }

    public static class TurnoDtoExtension
    {
        public static List<TurnoDto> MapearTurnos(this IEnumerable<Turno> entidades)
        {
            if (entidades == null || !entidades.Any())
                return new List<TurnoDto>();

            return entidades.Select(MapearTurno).ToList();
        }

        public static TurnoDto MapearTurno(this Turno entidade)
        {
            if (entidade == null)
                return null;

            return new TurnoDto()
            {
                Id = entidade.Id,
                Descricao = entidade.Descricao,
                HorarioInicio = entidade.HorarioInicio,
                HorarioFim = entidade.HorarioFim
            };
        }
    }
}
