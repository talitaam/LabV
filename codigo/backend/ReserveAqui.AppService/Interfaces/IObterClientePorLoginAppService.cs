using ReserveAqui.Domain.DTO;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IObterClientePorLoginAppService
    {
        ClienteDto ObterPorLogin(string login);
    }
}
