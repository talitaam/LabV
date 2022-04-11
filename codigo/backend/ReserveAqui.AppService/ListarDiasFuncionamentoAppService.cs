using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using System.Collections.Generic;

namespace ReserveAqui.AppService
{
    public class ListarDiasFuncionamentoAppService : IListarDiasFuncionamentoAppService
    {
        private readonly IDiaFuncionamentoService diaFuncionamentoService;

        public ListarDiasFuncionamentoAppService(IDiaFuncionamentoService diaFuncionamentoService)
        {
            this.diaFuncionamentoService = diaFuncionamentoService;
        }

        public List<DiaFuncionamentoDto> Listar()
        {
            return diaFuncionamentoService.Listar();
        }
    }
}
