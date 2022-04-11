using ReserveAqui.Domain.DTO.Autenticacao;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ReserveAqui.WebApi.Util
{
    public static class AutenticacaoUtil
    {
        private static class JwtUsuario
        {
            public static readonly string TOKEN_AUTENTICACAO = "idSessao";
            public static readonly string ID = "idUsuario";
            public static readonly string LOGIN_USUARIO = "login";
            public static readonly string EMAIL_USUARIO = "email";
            public static readonly string NOME_USUARIO = "nomeUsuario";

        }

        public static UsuarioLogado ObterUsuario(ClaimsPrincipal user)
        {
            var claimIdUsuario = user?.FindFirst(JwtUsuario.ID)?.Value;
            var claimEmailUsuario = user?.FindFirst(JwtUsuario.EMAIL_USUARIO)?.Value;
            var claimLoginUsuario = user?.FindFirst(JwtUsuario.LOGIN_USUARIO)?.Value;
            var token = user?.FindFirst(JwtUsuario.TOKEN_AUTENTICACAO)?.Value;
            var nomeUsuario = user?.FindFirst(JwtUsuario.NOME_USUARIO)?.Value;

            if (string.IsNullOrEmpty(claimIdUsuario))
                return null;

            return new UsuarioLogado()
            {
                Id = int.Parse(claimIdUsuario),
                Email = claimEmailUsuario,
                Login = claimLoginUsuario,
                Nome = nomeUsuario,
                Token = token
            };
        }

        public static string GerarTokenJwt(int idUsuario, string emailUsuario, 
            string loginUsuario, string nome, DateTime dataExpiracao)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtUsuario.ID, idUsuario.ToString()),
                new Claim(JwtUsuario.LOGIN_USUARIO, loginUsuario)
            };

            if (!string.IsNullOrWhiteSpace(emailUsuario))
                claims.Add(new Claim(JwtUsuario.EMAIL_USUARIO, emailUsuario));

            if (!string.IsNullOrWhiteSpace(loginUsuario))
                claims.Add(new Claim(JwtUsuario.LOGIN_USUARIO, loginUsuario));

            if (!string.IsNullOrWhiteSpace(nome))
                claims.Add(new Claim(JwtUsuario.NOME_USUARIO, nome));

            return GeradorJwt.Gerar(claims.ToArray(), dataExpiracao);
        }
    }
}
