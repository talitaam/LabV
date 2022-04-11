﻿using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.AppService
{
    public class ListarReservasPorClienteAppService : IListarReservasPorClienteAppService
    {
        private readonly IReservaService reservaService;
        private readonly IMesaService mesaService;
        private readonly ITurnoService turnoService;
        private readonly IStatusService statusService;
        private readonly IClienteService clienteService;

        public ListarReservasPorClienteAppService(IReservaService reservaService, IMesaService mesaService, 
            ITurnoService turnoService, IStatusService statusService, IClienteService clienteService)
        {
            this.reservaService = reservaService;
            this.mesaService = mesaService;
            this.turnoService = turnoService;
            this.statusService = statusService;
            this.clienteService = clienteService;
        }

        public List<ReservaDto> Listar(string login)
        {
            var reservas = reservaService.ListarPorLoginCliente(login);

            foreach (var reserva in reservas)
            {
                reserva.Mesas = mesaService.ListarPorIds(reserva.Mesas.Select(r => r.Id).ToList());
                reserva.Turnos = turnoService.ListarPorIds(reserva.Turnos.Select(r => r.Id).ToList());
                reserva.Status = statusService.ObterPorId(reserva.Status.Id);
                reserva.Cliente = clienteService.ObterPorId(reserva.Cliente.IdCliente);
            }

            return reservas;
        }
    }
}