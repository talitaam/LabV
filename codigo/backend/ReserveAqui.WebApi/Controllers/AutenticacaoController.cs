using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ReserveAqui.WebApi.Model;

namespace ReserveAqui.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutenticacaoController : ApiControllerBase
    {
        private readonly IAutenticarUsuarioPorLoginSenhaAppService autenticacaoAppService;

        public AutenticacaoController(IAutenticarUsuarioPorLoginSenhaAppService autenticacaoAppService)
        {
            this.autenticacaoAppService = autenticacaoAppService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioAutenticadoDto> AutenticarPorIdSessao(AutenticacaoModel model) {
            var usuario = autenticacaoAppService.Autenticar(model.Login, model.Senha);

            usuario.TokenJwt = ObterTokenJwt(usuario.Usuario, usuario.DataExpiracao);
            return Ok(usuario);
        }
    }
}