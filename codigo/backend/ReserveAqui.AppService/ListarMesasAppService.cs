using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using System.Collections.Generic;

namespace ReserveAqui.AppService
{
    public class ListarMesasAppService : IListarMesasAppService
    {
        private readonly IMesaService mesaService;

        public ListarMesasAppService(IMesaService mesaService)
        {
            this.mesaService = mesaService;
        }

        public List<MesaDto> Listar()
        {
            return mesaService.Listar();
        }
    }
}
