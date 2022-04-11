using ReserveAqui.Domain.DTO;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IObterStatusPorIdAppService
    {
        StatusDto ObterPorId(int id);
    }
}
