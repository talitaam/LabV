using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.WebApi.Controllers
{
    [Route("api/v1/status")]
    [ApiController]
    public class StatusController : ApiControllerBase
    {
        private readonly IListarStatusAppService listarStatusAppService;
        private readonly IObterStatusPorIdAppService obterStatusPorIdAppService;

        public StatusController(IListarStatusAppService listarStatusAppService,
            IObterStatusPorIdAppService obterStatusPorIdAppService)
        {
            this.listarStatusAppService = listarStatusAppService;
            this.obterStatusPorIdAppService = obterStatusPorIdAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<StatusDto>> Listar()
        {
            return Ok(listarStatusAppService.Listar());
        }

        [AllowAnonymous]
        [HttpGet("{idStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<StatusDto>> ObterPorId([FromRoute] int idStatus)
        {
            return Ok(obterStatusPorIdAppService.ObterPorId(idStatus));
        }
    }
}
