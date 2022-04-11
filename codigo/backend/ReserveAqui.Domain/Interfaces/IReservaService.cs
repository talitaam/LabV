using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.Domain.Interfaces
{
    public interface IReservaService
    {
        ReservaDto ObterPorId(int id);
        List<ReservaDto> ListarPorLoginCliente(string login);
        List<ReservaDto> ListarTodasReservas();
        void Inserir(ReservaDto reserva);
        void Atualizar(ReservaDto reserva);
        void AtualizarStatusMesasReserva(int idReserva, int idStatusReserva);
        void AtualizarAvaliacao(int idReserva, int avaliacao);
        List<int> ListarMesasReservaPorId(int idReserva);
    }
}
