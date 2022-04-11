using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.Interfaces;

namespace ReserveAqui.AppService
{
    public class DeletarMesaAppService : IDeletarMesaAppService
    {
        private readonly IMesaService mesaService;

        public DeletarMesaAppService(IMesaService mesaService)
        {
            this.mesaService = mesaService;
        }

        public void Deletar(int id)
        {
            mesaService.Deletar(id);
        }
    }
}
