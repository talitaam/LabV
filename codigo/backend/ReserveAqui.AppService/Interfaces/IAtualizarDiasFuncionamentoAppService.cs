using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IAtualizarDiasFuncionamentoAppService
    {
        void AtualizarDiasFuncionamento(List<DiaFuncionamentoDto> diasFuncionamento);
    }
}
