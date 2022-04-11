using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.Domain.Interfaces
{
    public interface IStatusService
    {
        List<StatusDto> Listar();
        StatusDto ObterPorId(int id);
    }
}
