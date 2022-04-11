using ReserveAqui.Domain.DTO;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IObterClientePorIdAppService
    {
        ClienteDto ObterPorId(int id);
    }
}
