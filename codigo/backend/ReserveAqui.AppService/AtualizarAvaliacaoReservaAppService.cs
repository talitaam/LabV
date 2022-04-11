using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class AtualizarAvaliacaoReservaAppService : IAtualizarAvaliacaoReservaAppService
    {
        private readonly IReservaService reservaService;

        public AtualizarAvaliacaoReservaAppService(IReservaService reservaService)
        {
            this.reservaService = reservaService;
        }

        public void AtualizarAvaliacao(int idReserva, int avaliacao)
        {
            reservaService.AtualizarAvaliacao(idReserva, avaliacao);
        }
    }
}
