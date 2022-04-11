using ReserveAqui.Domain.DTO;
using System;
using System.Collections.Generic;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IListarMesasDisponiveisAppService
    {
        List<MesaDto> ListarDisponiveis(DateTime dataReserva, List<int> idsTurnos);
    }
}
