using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IListarDiasFuncionamentoAppService
    {
        List<DiaFuncionamentoDto> Listar();
    }
}
