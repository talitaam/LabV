using System;

namespace ReserveAqui.Domain.DTO.Autenticacao
{
    public class DadosAutenticacaoDto
    {
        public int CodigoLaboratorio { get; set; }
        public string UsuarioAd { get; set; }
        public string CpfUsuario { get; set; }
        public DateTime DataExpiracao { get; set; }
        public DateTime DataAgora { get; set; }
    }
}
