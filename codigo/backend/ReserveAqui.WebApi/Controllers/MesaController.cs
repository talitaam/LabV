using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.Shared.Validacao;
using ReserveAqui.WebApi.Model;
using ReserveAqui.WebApi.SignalRChat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.WebApi.Controllers
{
    [Route("api/v1/mesa")]
    [ApiController]
    public class MesaController : ApiControllerBase
    {
        private readonly IListarMesasAppService listarMesasAppService;
        private readonly IListarMesasDisponiveisAppService listarMesasDisponiveisAppService;
        private readonly IInserirMesaAppService inserirMesasAppService;
        private readonly IAtualizarMesaAppService atualizarMesaAppService;
        private readonly IDeletarMesaAppService deletarMesaAppService;

        private readonly IHubContext<ChatHub> hubContext;

        public MesaController(IListarMesasAppService listarMesasAppService,
            IListarMesasDisponiveisAppService listarMesasDisponiveisAppService,
            IInserirMesaAppService inserirMesasAppService,
            IAtualizarMesaAppService atualizarMesaAppService,
             IDeletarMesaAppService deletarMesaAppService,
            IHubContext<ChatHub> hubContext)
        {
            this.listarMesasAppService = listarMesasAppService;
            this.listarMesasDisponiveisAppService = listarMesasDisponiveisAppService;
            this.inserirMesasAppService = inserirMesasAppService;
            this.atualizarMesaAppService = atualizarMesaAppService;
            this.deletarMesaAppService = deletarMesaAppService;
            this.hubContext = hubContext;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<MesaModel>> Listar()
        {
            return Ok(listarMesasAppService.Listar().MapearMesas());
        }

        [AllowAnonymous]
        [HttpGet("disponiveis")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<MesaModel>> ListarDisponiveis([FromQuery] DateTime dataReserva, string idsTurnos)
        {
            Validate.That(idsTurnos).IsNotNullOrWhiteSpace();
            var turnos = idsTurnos.Split(",").Select(id => id.Trim()).Select(int.Parse).ToList();

            return Ok(listarMesasDisponiveisAppService.ListarDisponiveis(dataReserva, turnos).MapearMesas());
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Inserir(MesaModel model)
        {
            inserirMesasAppService.Inserir(ConverterModelParaDto(model));
            hubContext.Clients.All.SendAsync("listarMesasNovamente", true);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Atualizar(MesaModel model)
        {
            atualizarMesaAppService.Atualizar(ConverterModelParaDto(model));
            hubContext.Clients.All.SendAsync("listarMesasNovamente", true);
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Deletar(int id)
        {
            deletarMesaAppService.Deletar(id);
            hubContext.Clients.All.SendAsync("listarMesasNovamente", true);
            return Ok();
        }

        private MesaDto ConverterModelParaDto(MesaModel model)
        {
            if (model == null)
                return null;

            return new MesaDto()
            {
                Id = model.Id,
                QuantCadeiras = int.Parse(model.QuantCadeiras),
                NomeMesa = model.NomeMesa

            };
        }
    }
}
