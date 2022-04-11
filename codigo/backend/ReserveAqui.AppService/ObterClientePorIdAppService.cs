using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class ObterClientePorIdAppService : IObterClientePorIdAppService
    {
        private readonly IClienteService clienteService;

        public ObterClientePorIdAppService(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        public ClienteDto ObterPorId(int id)
        {
            return clienteService.ObterPorId(id);
        }
    }
}
