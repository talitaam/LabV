using ReserveAqui.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.Domain.DTO
{
    public class MesaDto
    {
        public int Id { get; set; }
        public int QuantCadeiras { get; set; }
        public string NomeMesa { get; set; }
    }

    public static class MesaDtoExtension
    {
        public static List<MesaDto> MapearMesas(this IEnumerable<Mesa> entidades)
        {
            if (entidades == null || !entidades.Any())
                return new List<MesaDto>();

                return entidades.Select(MapearMesa).ToList();
        }

        public static MesaDto MapearMesa(this Mesa entidade)
        {
            if (entidade == null)
                return null;

            return new MesaDto()
            {
                Id = entidade.Id,
                QuantCadeiras = entidade.QuantCadeiras,
                NomeMesa = entidade.NomeMesa


            };
        }
    }
}
