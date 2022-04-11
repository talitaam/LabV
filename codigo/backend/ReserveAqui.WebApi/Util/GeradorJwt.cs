using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReserveAqui.WebApi.Util
{
    public static class GeradorJwt
    {
        public static string Segredo { get; private set; }
        public static byte[] Chave { get; private set; }

        public static void Configurar(string segredo)
        {
            Segredo = segredo ?? throw new ArgumentNullException(nameof(segredo));
            Chave = Encoding.ASCII.GetBytes(Segredo);
        }

        public static string Gerar(Claim[] claims, DateTime dataExpiracao)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = dataExpiracao.ToUniversalTime(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Chave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
