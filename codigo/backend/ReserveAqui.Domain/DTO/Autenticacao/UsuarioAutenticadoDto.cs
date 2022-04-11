using System;

namespace ReserveAqui.Domain.DTO.Autenticacao
{
    public class UsuarioAutenticadoDto
    {
        public ClienteDto Usuario { get; set; }
        public DateTime DataExpiracao { get; set; }
        public DateTime DataAgora { get; set; }
        public string TokenJwt { get; set; }
    }
}
