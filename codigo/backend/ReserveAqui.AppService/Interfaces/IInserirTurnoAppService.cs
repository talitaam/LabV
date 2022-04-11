using ReserveAqui.Domain.DTO;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IInserirTurnoAppService
    {
        void Inserir(TurnoDto turno);
    }
}
