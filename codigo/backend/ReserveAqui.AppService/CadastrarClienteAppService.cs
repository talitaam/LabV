using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class CadastrarClienteAppService : ICadastrarClienteAppService
    {
        private readonly IClienteService clienteService;

        public CadastrarClienteAppService(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        public void Cadastrar(ClienteDto cliente)
        {
            clienteService.Cadastrar(cliente);
        }
    }
}
