using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class InserirTurnoAppService : IInserirTurnoAppService
    {
        private readonly ITurnoService turnoService;

        public InserirTurnoAppService(ITurnoService turnoService)
        {
            this.turnoService = turnoService;
        }

        public void Inserir(TurnoDto turno)
        {
            turnoService.Inserir(turno);
        }
    }
}
