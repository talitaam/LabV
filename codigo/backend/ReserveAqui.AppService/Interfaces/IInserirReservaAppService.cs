using ReserveAqui.Domain.DTO;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IInserirReservaAppService
    {
        void Inserir(ReservaDto reserva);
    }
}
