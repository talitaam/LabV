using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using System.Collections.Generic;

namespace ReserveAqui.AppService
{
    public class AtualizarDiasFuncionamentoAppService : IAtualizarDiasFuncionamentoAppService
    {
        private readonly IDiaFuncionamentoService diaFuncionamentoService;

        public AtualizarDiasFuncionamentoAppService(IDiaFuncionamentoService diaFuncionamentoService)
        {
            this.diaFuncionamentoService = diaFuncionamentoService;
        }

        public void AtualizarDiasFuncionamento(List<DiaFuncionamentoDto> diasFuncionamento)
        {
            diaFuncionamentoService.AtualizarDiasFuncionamento(diasFuncionamento);
        }
    }
}
