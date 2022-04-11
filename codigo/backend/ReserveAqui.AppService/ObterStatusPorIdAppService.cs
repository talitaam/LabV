using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class ObterStatusPorIdAppService : IObterStatusPorIdAppService
    {
        private readonly IStatusService statusService;

        public ObterStatusPorIdAppService(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        public StatusDto ObterPorId(int id)
        {
            return statusService.ObterPorId(id);
        }
    }
}
