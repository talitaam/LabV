using System;

namespace ReserveAqui.Shared.Exceptions
{
    [Serializable]
    public class ParametroInvalidoException : AplicacaoException
    {
        public ParametroInvalidoException() : base(Mensagens.PARAMETROS_INVALIDOS) { }
        public ParametroInvalidoException(string mensagem) : base(mensagem) { }
    }
}
