using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.WebApi.Model;
using ReserveAqui.WebApi.SignalRChat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.WebApi.Controllers
{
    [Route("api/v1/reserva")]
    [ApiController]
    public class ReservaController : ApiControllerBase
    {
        private readonly IListarReservasPorClienteAppService listarReservasPorCliente;
        private readonly IInserirReservaAppService inserirReserva;
        private readonly IAtualizarReservaAppService atualizarReserva;
        private readonly IObterClientePorIdAppService obterClientePorId;
        private readonly IListarTodasReservasAppService listarTodasReservas;
        private readonly IAtualizarStatusMesaReservaAppService atualizarStatusMesaReserva;
        private readonly IObterStatusPorIdAppService obterStatusPorIdAppService;
        private readonly IAtualizarAvaliacaoReservaAppService atualizarAvaliacaoReservaAppService;

        private readonly IHubContext<ChatHub> hubContext;

        public ReservaController(IListarReservasPorClienteAppService listarReservasPorCliente, 
            IInserirReservaAppService inserirReserva, 
            IObterClientePorIdAppService obterClientePorId,
            IAtualizarReservaAppService atualizarReserva, IListarTodasReservasAppService listarTodasReservas,
            IAtualizarStatusMesaReservaAppService atualizarStatusMesaReserva,
            IObterStatusPorIdAppService obterStatusPorIdAppService,
            IAtualizarAvaliacaoReservaAppService atualizarAvaliacaoReservaAppService,
            IHubContext<ChatHub> hubContext)
        {
            this.listarReservasPorCliente = listarReservasPorCliente;
            this.inserirReserva = inserirReserva;
            this.obterClientePorId = obterClientePorId;
            this.atualizarReserva = atualizarReserva;
            this.listarTodasReservas = listarTodasReservas;
            this.atualizarStatusMesaReserva = atualizarStatusMesaReserva;
            this.obterStatusPorIdAppService = obterStatusPorIdAppService;
            this.atualizarAvaliacaoReservaAppService = atualizarAvaliacaoReservaAppService;
            this.hubContext = hubContext;
        }

        [AllowAnonymous]
        [HttpGet("{loginCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<ReservaModel>> ListarPorCliente(string loginCliente)
        {
            return Ok(listarReservasPorCliente.Listar(loginCliente).MapearReservas());
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<ReservaModel>> ListarTodas()
        {
            return Ok(listarTodasReservas.ListarTodas().MapearReservas());
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Inserir(CadastroReservaModel model)
        {
            inserirReserva.Inserir(ConverterModelCriarParaDto(model));
            hubContext.Clients.All.SendAsync("listarMesasNovamente", true);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Atualizar(AtualizarReservaModel model)
        {
            atualizarReserva.Atualizar(ConverterModelAtualizarParaDto(model));
            hubContext.Clients.All.SendAsync("listarMesasNovamente", true);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("avaliacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AtualizarAvaliacao(AtualizarAvaliacaoReservaModel model)
        {
            atualizarAvaliacaoReservaAppService.AtualizarAvaliacao(model.Id, model.Avaliacao);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AtualizarStatusReserva(AtualizarStatusReservaModel model)
        {
            atualizarStatusMesaReserva.AtualizarStatusMesaReserva(model.IdReserva, model.IdStatus);
            hubContext.Clients.All.SendAsync("listarMesasNovamente", true);
            return Ok();
        }

        private ReservaDto ConverterModelCriarParaDto(CadastroReservaModel model)
        {
            if (model == null)
                return null;

            return new ReservaDto()
            {
                HorarioChegadaEsperada = TimeSpan.Parse(model.HorarioChegadaEsperada),
                DataReserva = model.DataReserva,
                Cliente = obterClientePorId.ObterPorId(model.Cliente),
                Avaliacao = model.Avaliacao,
                Status = obterStatusPorIdAppService.ObterPorId(1),
                Mesas = ConverterMesas(model.Mesas),
                Turnos = ConverterTurnos(model.Turnos),
            };
        }

        private ReservaDto ConverterModelAtualizarParaDto(AtualizarReservaModel model)
        {
            if (model == null)
                return null;

            return new ReservaDto()
            {
                Id = model.Id,
                HorarioChegadaEsperada = TimeSpan.Parse(model.HorarioChegadaEsperada),
                DataReserva = model.DataReserva,
                Avaliacao = model.Avaliacao,
                Status = obterStatusPorIdAppService.ObterPorId(1),
                Mesas = ConverterMesas(model.Mesas),
                Turnos = ConverterTurnos(model.Turnos),
            };
        }

        private static List<MesaDto> ConverterMesas(List<int> idsMesas)
        {
            if (idsMesas == null || !idsMesas.Any())
                return new List<MesaDto>();

            return idsMesas.Select(idMesa => new MesaDto { Id = idMesa }).ToList();
        }

        private static List<TurnoDto> ConverterTurnos(List<int> idsTurnos)
        {
            if (idsTurnos == null || !idsTurnos.Any())
                return new List<TurnoDto>();

            return idsTurnos.Select(idTurno => new TurnoDto { Id = idTurno }).ToList();
        }
    }
}
