using ReserveAqui.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveAqui.WebApi.Model
{
    public class MesaModel
    {
        public int Id { get; set; }
        public string QuantCadeiras { get; set; }
        public string NomeMesa { get; set; }
        
    }

    public static class MesaDtoExtension
    {
        public static List<MesaModel> MapearMesas(this List<MesaDto> entidades)
        {
            if (entidades == null || !entidades.Any())
                return new List<MesaModel>();

                return entidades.Select(MapearMesa).ToList();
        }

        public static MesaModel MapearMesa(this MesaDto dto)
        {
            if (dto == null)
                return null;

            return new MesaModel()
            {
                Id = dto.Id,
                QuantCadeiras = dto.QuantCadeiras.ToString(),
                NomeMesa = dto.NomeMesa,
                
            };
        }
    }
}
