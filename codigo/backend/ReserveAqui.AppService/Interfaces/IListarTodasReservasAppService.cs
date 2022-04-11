using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IListarTodasReservasAppService
    {
        List<ReservaDto> ListarTodas();
    }
}
