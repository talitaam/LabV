using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class InserirMesaAppService : IInserirMesaAppService
    {
        private readonly IMesaService mesaService;

        public InserirMesaAppService(IMesaService mesaService)
        {
            this.mesaService = mesaService;
        }

        public void Inserir(MesaDto mesa)
        {
            mesaService.Inserir(mesa);
        }
    }
}
