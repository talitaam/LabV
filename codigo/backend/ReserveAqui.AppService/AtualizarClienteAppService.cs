using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class AtualizarClienteAppService : IAtualizarClienteAppService
    {
        private readonly IClienteService clienteService;

        public AtualizarClienteAppService(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        public void Atualizar(ClienteDto cliente)
        {
            clienteService.Atualizar(cliente);
        }
    }
}
