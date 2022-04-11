using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.WebApi.Model;
using System.Collections.Generic;

namespace ReserveAqui.WebApi.Controllers
{
    [Route("api/v1/diasFuncionamento")]
    [ApiController]
    public class DiasFuncionamentoController : ApiControllerBase
    {
        private readonly IListarDiasFuncionamentoAppService listarDiasFuncionamentoAppService;
        private readonly IAtualizarDiasFuncionamentoAppService atualizarDiasFuncionamentoAppService;

        public DiasFuncionamentoController(IListarDiasFuncionamentoAppService listarDiasFuncionamentoAppService,
            IAtualizarDiasFuncionamentoAppService atualizarDiasFuncionamentoAppService)
        {
            this.listarDiasFuncionamentoAppService = listarDiasFuncionamentoAppService;
            this.atualizarDiasFuncionamentoAppService = atualizarDiasFuncionamentoAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<DiaFuncionamentoDto>> Listar()
        {
            return Ok(listarDiasFuncionamentoAppService.Listar());
        }

        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AtualizarDiasFuncionamento(AtualizarDiasFuncionamentoModel model)
        {
            atualizarDiasFuncionamentoAppService.AtualizarDiasFuncionamento(model?.DiasFuncionamento);
            return Ok();
        }
    }
}
