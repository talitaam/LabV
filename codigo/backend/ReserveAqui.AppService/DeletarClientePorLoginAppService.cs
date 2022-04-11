using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class DeletarClientePorLoginAppService : IDeletarClientePorLoginAppService
    {
        private readonly IClienteService clienteService;

        public DeletarClientePorLoginAppService(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        public void DeletarPorLogin(string login)
        {
            clienteService.DeletarPorLogin(login);
        }
    }
}
