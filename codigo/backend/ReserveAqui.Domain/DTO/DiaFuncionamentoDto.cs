using ReserveAqui.Infra.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.Domain.DTO
{
    public class DiaFuncionamentoDto
    {
        public string DescricaoDia { get; set; }
        public int DiaSemana { get; set; }
        public bool Ativo { get; set; }
    }

    public static class DiaFuncionamentoDtoExtension
    {
        public static List<DiaFuncionamentoDto> MapearDiasFuncionamento(this IEnumerable<DiaFuncionamento> entidades)
        {
            if (entidades == null || !entidades.Any())
                return new List<DiaFuncionamentoDto>();

            return entidades.Select(MapearDiaFuncionamento).ToList();
        }

        public static DiaFuncionamentoDto MapearDiaFuncionamento(this DiaFuncionamento entidade)
        {
            if (entidade == null)
                return null;

            return new DiaFuncionamentoDto()
            {
                Ativo = entidade.Ativo,
                DescricaoDia = entidade.DescricaoDia,
                DiaSemana = entidade.DiaSemana
            };
        }
    }
}
