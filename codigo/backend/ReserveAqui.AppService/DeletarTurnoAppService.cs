using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class DeletarTurnoAppService : IDeletarTurnoAppService
    {
        private readonly ITurnoService turnoService;

        public DeletarTurnoAppService(ITurnoService turnoService)
        {
            this.turnoService = turnoService;
        }

        public void Deletar(int id)
        {
            turnoService.Deletar(id);
        }
    }
}
