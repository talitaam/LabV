using ReserveAqui.Infra.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.Domain.DTO
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public static class StatusDtoExtension
    {
        public static List<StatusDto> MapearMultiplosStatus(this IEnumerable<Status> entidades)
        {
            if (entidades == null || !entidades.Any())
                return new List<StatusDto>();

            return entidades.Select(MapearStatus).ToList();
        }

        public static StatusDto MapearStatus(this Status entidade)
        {
            if (entidade == null)
                return null;

            return new StatusDto()
            {
                Id = entidade.Id,
                Descricao = entidade.Descricao
            };
        }
    }
}