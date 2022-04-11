using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace ReserveAqui.AppService
{
    public class ListarMesasDisponiveisAppService : IListarMesasDisponiveisAppService
    {
        private readonly IMesaService mesaService;

        public ListarMesasDisponiveisAppService(IMesaService mesaService)
        {
            this.mesaService = mesaService;
        }

        public List<MesaDto> ListarDisponiveis(DateTime dataReserva, List<int> idsTurnos)
        {
            return mesaService.ListarMesasDisponiveisPorTurnos(dataReserva, idsTurnos);
        }
    }
}
