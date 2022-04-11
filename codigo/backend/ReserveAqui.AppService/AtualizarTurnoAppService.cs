using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class AtualizarTurnoAppService : IAtualizarTurnoAppService
    {
        private readonly ITurnoService turnoService;

        public AtualizarTurnoAppService(ITurnoService turnoService)
        {
            this.turnoService = turnoService;
        }

        public void Atualizar(TurnoDto turno)
        {
            turnoService.Atualizar(turno);
        }
    }
}
