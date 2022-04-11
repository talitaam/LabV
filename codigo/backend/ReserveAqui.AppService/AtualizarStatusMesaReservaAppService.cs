using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class AtualizarStatusMesaReservaAppService : IAtualizarStatusMesaReservaAppService
    {
        private readonly IReservaService reservaService;

        public AtualizarStatusMesaReservaAppService(IReservaService reservaService)
        {
            this.reservaService = reservaService;
        }

        public void AtualizarStatusMesaReserva(int idReserva, int idStatus)
        {
            reservaService.AtualizarStatusMesasReserva(idReserva, idStatus);
        }
    }
}
