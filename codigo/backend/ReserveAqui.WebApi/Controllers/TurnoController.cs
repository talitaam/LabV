using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.WebApi.Model;
using System;
using System.Collections.Generic;

namespace ReserveAqui.WebApi.Controllers
{
    [Route("api/v1/turno")]
    [ApiController]
    public class TurnoController : ApiControllerBase
    {
        private readonly IListarTurnosAppService listarTurnosAppService;
        private readonly IInserirTurnoAppService inserirTurnosAppService;
        private readonly IAtualizarTurnoAppService atualizarTurnoAppService;
        private readonly IDeletarTurnoAppService deletarTurnoAppService;

        public TurnoController(IListarTurnosAppService listarTurnosAppService,
            IInserirTurnoAppService inserirTurnosAppService,
            IAtualizarTurnoAppService atualizarTurnoAppService,
             IDeletarTurnoAppService deletarTurnoAppService)
        {
            this.listarTurnosAppService = listarTurnosAppService;
            this.inserirTurnosAppService = inserirTurnosAppService;
            this.atualizarTurnoAppService = atualizarTurnoAppService;
            this.deletarTurnoAppService = deletarTurnoAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<TurnoModel>> Listar()
        {
            return Ok(listarTurnosAppService.Listar().MapearTurnos());
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Inserir(TurnoModel model)
        {
            //TODO: Validar autenticação
            inserirTurnosAppService.Inserir(ConverterModelParaDto(model));
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Atualizar(TurnoModel model)
        {
            //TODO: Validar autenticação
            atualizarTurnoAppService.Atualizar(ConverterModelParaDto(model));
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Deletar(int id)
        {
            //TODO: Validar autenticação
            deletarTurnoAppService.Deletar(id);
            return Ok();
        }

        private TurnoDto ConverterModelParaDto(TurnoModel model)
        {
            if (model == null)
                return null;

            return new TurnoDto()
            {
                Id = model.Id,
                Descricao = model.Descricao,
                HorarioInicio = TimeSpan.Parse(model.HorarioInicio),
                HorarioFim = TimeSpan.Parse(model.HorarioFim)
            };
        }
    }
}
