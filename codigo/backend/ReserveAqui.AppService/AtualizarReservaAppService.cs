using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using ReserveAqui.Shared;
using ReserveAqui.Shared.Exceptions;
using ReserveAqui.Shared.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.AppService
{
    public class AtualizarReservaAppService : IAtualizarReservaAppService
    {
        private readonly IReservaService reservaService;
        private readonly IMesaService mesaService;
        private readonly ITurnoService turnoService;
        private readonly IDiaFuncionamentoService diaFuncionamentoService;

        public AtualizarReservaAppService(IReservaService reservaService, IMesaService mesaService, 
            ITurnoService turnoService, IDiaFuncionamentoService diaFuncionamentoService)
        {
            this.reservaService = reservaService;
            this.mesaService = mesaService;
            this.turnoService = turnoService;
            this.diaFuncionamentoService = diaFuncionamentoService;
        }

        public void Atualizar(ReservaDto reserva)
        {
            var reservaAtual = reservaService.ObterPorId(reserva.Id);
            Validate.That(reservaAtual).IsNotNull(Mensagens.ERRO_RESERVA_NAO_ENCONTRADA);
            Validate.That(reservaAtual.Status.Id).IsEqualTo(1, Mensagens.ERRO_STATUS_INVALIDO);

            ValidarFuncionamentoRestaurante(reserva.DataReserva);

            ValidarMesasExistentesReserva(reserva.Mesas);
            ValidaTurnosExistentesReserva(reserva.Turnos);

            ValidarMesasLivres(reserva, reservaAtual);

            reservaService.Atualizar(reserva);
        }

        private void ValidarFuncionamentoRestaurante(DateTime dataReserva)
        {
            bool restauranteFuncionando = diaFuncionamentoService.ValidarFuncionamentoDia((int)dataReserva.DayOfWeek);

            Validate.That(restauranteFuncionando).IsTrue(Mensagens.ERRO_DATA_RESERVA_INVALIDA);
        }

        private void ValidarMesasExistentesReserva(List<MesaDto> mesas)
        {
            var idsMesasReserva = mesas.Select(m => m.Id).ToList();
            var mesasExistentes = mesaService.ListarPorIds(idsMesasReserva);
            Validate.That(mesasExistentes.Count).IsEqualTo(idsMesasReserva.Count, Mensagens.ERRO_MESAS_INVALIDAS);
        }

        private void ValidaTurnosExistentesReserva(List<TurnoDto> turnos)
        {
            var idsTurnosReserva = turnos.Select(m => m.Id).ToList();
            var turnosExistentes = turnoService.ListarPorIds(idsTurnosReserva);
            Validate.That(turnosExistentes.Count).IsEqualTo(idsTurnosReserva.Count, Mensagens.ERRO_TURNOS_INVALIDOS);
        }

        private void ValidarMesasLivres(ReservaDto reserva, ReservaDto reservaAtual)
        {
            var mesasAtuaisReserva = reservaAtual.Mesas.Select(m => m.Id);

            var mesasLivres = mesaService.ListarMesasDisponiveisPorTurnos(reserva.DataReserva, reserva.Turnos.Select(t => t.Id).ToList(), reserva.Id);
            var idsMesasLivres = mesasLivres.Select(m => m.Id).ToList();

            var idsMesasReserva = reserva.Mesas.Select(m => m.Id);

            if (!mesasLivres.Any() || !idsMesasReserva.All(idMesa => idsMesasLivres.Contains(idMesa)))
                throw new AplicacaoException(Mensagens.ERRO_RESERVA_MESAS_OCUPADAS);
        }
    }
}
