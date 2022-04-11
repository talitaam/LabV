using System;

namespace ReserveAqui.Domain.DTO.Mensagem
{
    public class MensagemDto
    {
        public string CodigoPlataforma { get; set; }
        public DateTime DataHoraEnvio { get; set; }
        public string Texto { get; set; }
    }
}
