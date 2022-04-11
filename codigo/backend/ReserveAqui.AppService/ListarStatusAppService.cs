using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using System.Collections.Generic;

namespace ReserveAqui.AppService
{
    public class ListarStatusAppService : IListarStatusAppService
    {
        private readonly IStatusService statusService;

        public ListarStatusAppService(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        public List<StatusDto> Listar()
        {
            return statusService.Listar();
        }

    }
}
