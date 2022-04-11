using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Shared.Validacao;
using System.Collections.Generic;
using System.Linq;
using ReserveAqui.Shared;

namespace ReserveAqui.Domain.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepositorio reservaRepositorio;

        public ReservaService(IReservaRepositorio reservaRepositorio)
        {
            this.reservaRepositorio = reservaRepositorio;
        }

        public List<int> ListarMesasReservaPorId(int idReserva)
        {
            return reservaRepositorio.ListarMesasReservaPorId(idReserva).Select(m => m.IdMesa).ToList();
        }

        public List<ReservaDto> ListarPorLoginCliente(string login)
        {
            Validate.That(login).IsNotNullOrWhiteSpace();

            var reservas = reservaRepositorio.ListarPorLoginCliente(login);

            foreach (var reserva in reservas)
            {
                var mesasReserva = reservaRepositorio.ListarMesasReservaPorId(reserva.Id).ToList();

                int? idStatus = mesasReserva.FirstOrDefault()?.IdStatus;

                reserva.IdStatus = idStatus.GetValueOrDefault(0);
                reserva.IdMesas = mesasReserva.Select(m => m.IdMesa).ToList();
                reserva.IdTurnos = reservaRepositorio.ListarTurnosReservaPorId(reserva.Id).ToList();
            }

            return reservas.MapearReservas();
        }

        public List<ReservaDto> ListarTodasReservas()
        {
            var reservas = reservaRepositorio.ListarTodasReservas();

            foreach (var reserva in reservas)
            {
                var mesasReserva = reservaRepositorio.ListarMesasReservaPorId(reserva.Id).ToList();

                int? idStatus = mesasReserva.FirstOrDefault()?.IdStatus;

                reserva.IdStatus = idStatus.GetValueOrDefault(0);
                reserva.IdMesas = mesasReserva.Select(m => m.IdMesa).ToList();
                reserva.IdTurnos = reservaRepositorio.ListarTurnosReservaPorId(reserva.Id).ToList();
            }

            return reservas.MapearReservas();
        }

        public void Inserir(ReservaDto reserva)
        {
            Validate.That(reserva).IsNotNull();
            Validate.That(reserva.HorarioChegadaEsperada).IsNotNull();
            Validate.That(reserva.DataReserva).IsNotNull();
            Validate.That(reserva?.Cliente.IdCliente).IsNotNull();
            Validate.That(reserva.Status).IsNotNull();
            Validate.That(reserva.Turnos).IsNotNull();
            Validate.That(reserva.Mesas).IsNotNull();

            reservaRepositorio.Inserir(ConverterParaEntidade(reserva));
        }

        public ReservaDto ObterPorId(int id)
        {
            var reserva = reservaRepositorio.ObterPorId(id);

            if (reserva == null)
                return null;

            var mesasReserva = reservaRepositorio.ListarMesasReservaPorId(id).ToList();

            int? idStatus = mesasReserva.FirstOrDefault()?.IdStatus;

            reserva.IdStatus = idStatus.GetValueOrDefault(0);
            reserva.IdMesas = mesasReserva.Select(m => m.IdMesa).ToList();
            reserva.IdTurnos = reservaRepositorio.ListarTurnosReservaPorId(id).ToList();

            return reserva.MapearReserva();
        }

        public void Atualizar(ReservaDto reserva)
        {
            Validate.That(reserva).IsNotNull();
            Validate.That(reserva.Id).IsNotNull();
            Validate.That(reserva.HorarioChegadaEsperada).IsNotNull();
            Validate.That(reserva.DataReserva).IsNotNull();
            Validate.That(reserva.Mesas).IsNotNullOrEmpty();
            Validate.That(reserva.Turnos).IsNotNullOrEmpty();

            var reservaObtida = ObterPorId(reserva.Id);
            Validate.That(reservaObtida).IsNotNull(Mensagens.ERRO_RESERVA_NAO_ENCONTRADA);

            reserva.Cliente = reservaObtida.Cliente;

            reservaRepositorio.Atualizar(ConverterParaEntidade(reserva));
        }

        public void AtualizarStatusMesasReserva(int idReserva, int idStatusReserva)
        {
            Validate.That(idReserva).IsGreaterThan(0);
            Validate.That(idStatusReserva).IsGreaterThan(0);

            var mesasReserva = reservaRepositorio.ListarMesasReservaPorId(idReserva).ToList();

            Validate.That(mesasReserva).IsNotNullOrEmpty(Mensagens.ERRO_RESERVA_NAO_ENCONTRADA);

            reservaRepositorio.AtualizarStatusMesaReserva(idReserva, idStatusReserva);
        }

        public void AtualizarAvaliacao(int idReserva, int avaliacao)
        {
            Validate.That(idReserva).IsGreaterThan(0);
            Validate.That(avaliacao).IsGreaterThan(0).IsLowerThanOrEqualTo(5);

            var reservaObtida = ObterPorId(idReserva);
            Validate.That(reservaObtida).IsNotNull(Mensagens.ERRO_RESERVA_NAO_ENCONTRADA);
            Validate.That(reservaObtida.Status.Id).IsEqualTo(3, Mensagens.ERRO_RESERVA_NAO_FINALIZADA);
            Validate.That(reservaObtida.Avaliacao).IsNull(Mensagens.ERRO_RESERVA_JA_AVALIADA);

            reservaRepositorio.AtualizarAvaliacaoReserva(idReserva, avaliacao);
        }

        private Reserva ConverterParaEntidade(ReservaDto reserva)
        {
            return new Reserva()
            {
                Id = reserva.Id,
                HorarioChegadaEsperada = reserva.HorarioChegadaEsperada,
                DataReserva = reserva.DataReserva,
                IdCliente = reserva.Cliente.IdCliente,
                Avaliacao = reserva.Avaliacao,
                IdStatus = reserva.Status.Id,
                IdMesas = ConverterMesasParaEntidade(reserva.Mesas),
                IdTurnos = ConverterTurnosParaEntidade(reserva.Turnos)
            };
        }

        private List<int> ConverterMesasParaEntidade(List<MesaDto> mesas)
        {
            if (mesas == null || !mesas.Any())
                return new List<int>();

            return mesas.Select(mesa => mesa.Id).ToList();
        }
        private List<int> ConverterTurnosParaEntidade(List<TurnoDto> turnos)
        {
            if (turnos == null || !turnos.Any())
                return new List<int>();

            return turnos.Select(turno => turno.Id).ToList();
        }
    }
}
