using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class ObterClientePorLoginAppService : IObterClientePorLoginAppService
    {
        private readonly IClienteService clienteService;

        public ObterClientePorLoginAppService(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        public ClienteDto ObterPorLogin(string login)
        {
            return clienteService.ObterPorLogin(login);
        }
    }
}
