using ReserveAqui.Infra.Data.Entities;
using System.Collections.Generic;

namespace ReserveAqui.Infra.Data.Interfaces
{
    public interface IReservaRepositorio
    {
        Reserva ObterPorId(int id);
        IEnumerable<Reserva> ListarPorLoginCliente(string login);
        IEnumerable<Reserva> ListarTodasReservas();
        IEnumerable<MesaReserva> ListarMesasReservaPorId(int id);
        IEnumerable<int> ListarTurnosReservaPorId(int id);
        void Inserir(Reserva reserva);
        void Atualizar(Reserva reserva);
        void AtualizarStatusMesaReserva(int idReserva, int idStatus);
        void AtualizarAvaliacaoReserva(int idReserva, int avaliacao);
    }
}
