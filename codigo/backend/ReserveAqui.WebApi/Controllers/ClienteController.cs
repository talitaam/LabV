using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO;
using ReserveAqui.WebApi.Model;

namespace ReserveAqui.WebApi.Controllers
{
    [Route("api/v1/cliente")]
    [ApiController]
    public class ClienteController : ApiControllerBase
    {
        private readonly IObterClientePorLoginAppService obterClientePorIdAppService;
        private readonly ICadastrarClienteAppService cadastrarClienteAppService;
        private readonly IAtualizarClienteAppService atualizarClienteAppService;
        private readonly IDeletarClientePorLoginAppService deletarClientePorLoginAppService;

        public ClienteController(IObterClientePorLoginAppService obterClientePorIdAppService,
            ICadastrarClienteAppService cadastrarClienteAppService,
            IAtualizarClienteAppService atualizarClienteAppService,
            IDeletarClientePorLoginAppService deletarClientePorLoginAppService)
        {
            this.obterClientePorIdAppService = obterClientePorIdAppService;
            this.cadastrarClienteAppService = cadastrarClienteAppService;
            this.atualizarClienteAppService = atualizarClienteAppService;
            this.deletarClientePorLoginAppService = deletarClientePorLoginAppService;
        }

        [AllowAnonymous]
        [HttpGet("{login}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClienteDto> ObterPorLogin(string login)
        {
            //TODO: Validar usuário logado
            var cliente = obterClientePorIdAppService.ObterPorLogin(login);

            return Ok(cliente);
        }

        [AllowAnonymous]
        [HttpDelete("{login}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClienteDto> DeletarPorLogin(string login)
        {
            //TODO: Validar usuário logado
            deletarClientePorLoginAppService.DeletarPorLogin(login);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CadastrarCliente(CadastroClienteModel model)
        {
            cadastrarClienteAppService.Cadastrar(model.MapearCliente());

            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AtualizarCliente(AtualizarClienteModel model)
        {
            //TODO: Validar usuário logado
            atualizarClienteAppService.Atualizar(model.MapearCliente());

            return Ok();
        }
    }
}
