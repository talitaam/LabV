using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class AtualizarMesaAppService : IAtualizarMesaAppService
    {
        private readonly IMesaService mesaService;

        public AtualizarMesaAppService(IMesaService mesaService)
        {
            this.mesaService = mesaService;
        }

        public void Atualizar(MesaDto mesa)
        {
            mesaService.Atualizar(mesa);
        }
    }
}
