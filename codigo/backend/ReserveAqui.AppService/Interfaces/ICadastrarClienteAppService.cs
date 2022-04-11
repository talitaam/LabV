using ReserveAqui.Domain.DTO;

namespace ReserveAqui.AppService.Interfaces
{
    public interface ICadastrarClienteAppService
    {
        void Cadastrar(ClienteDto cliente);
    }
}
