using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using System.Collections.Generic;

namespace ReserveAqui.AppService
{
    public class ListarTurnosAppService : IListarTurnosAppService
    {
        private readonly ITurnoService turnoService;

        public ListarTurnosAppService(ITurnoService turnoService)
        {
            this.turnoService = turnoService;
        }

        public List<TurnoDto> Listar()
        {
            return turnoService.Listar();
        }
    }
}
