using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.DTO.Autenticacao;
using ReserveAqui.WebApi.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ReserveAqui.WebApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected UsuarioLogado Usuario => ObterUsuarioSessao();

        private UsuarioLogado ObterUsuarioSessao()
        {
            return AutenticacaoUtil.ObterUsuario(User);
        }

        protected string ObterTokenJwt(ClienteDto usuario, DateTime dataExpiracao)
        {
            return AutenticacaoUtil.GerarTokenJwt(usuario.IdCliente, usuario.Email, usuario.Login, usuario.Nome, dataExpiracao);
        }
    }
}
