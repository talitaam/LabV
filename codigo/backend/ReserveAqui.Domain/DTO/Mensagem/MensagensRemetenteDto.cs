using System.Collections.Generic;

namespace ReserveAqui.Domain.DTO.Mensagem
{
    public class MensagensRemetenteDto
    {
        public RemetenteMensagemDto Remetente { get; set; }
        public List<MensagemDto> Mensagens { get; set; }
    }
}
