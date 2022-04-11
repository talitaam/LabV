namespace ReserveAqui.Shared
{
    public static class Mensagens
    {
        public const string PARAMETROS_INVALIDOS = "Os parâmetros informados não são válidos.";
        public const string PERMISSAO_NEGADA = "Você não possui acesso a esta funcionalidade.";
        public const string PERMISSAO_NEGADA_SISTEMA = "Você não possui acesso a este sistema.";
        public const string SESSAO_EXPIRADA = "A sessão expirou.";

        public const string ERRO_USUARIO_NAO_ENCONTRADO = "O usuário não foi encontrado.";

        public const string ERRO_LOGIN_NAO_INFORMADO = "O login não foi informado.";
        public const string ERRO_SENHA_NAO_INFORMADA = "A senha não foi informada.";
        public const string ERRO_EMAIL_NAO_INFORMADO = "O e-mail não foi informado.";
        public const string ERRO_NOME_NAO_INFORMADO = "O nome não foi informado.";
        public const string ERRO_CELULAR_NAO_INFORMADO = "O celular não foi informado.";

        public const string ERRO_TURNO_NAO_ENCONTRADO = "O turno informado não foi encontrado.";
        public const string ERRO_MESA_NAO_ENCONTRADA = "A mesa informado não foi encontrado.";
        public const string ERRO_RESERVA_NAO_ENCONTRADA = "A reserva informada não foi encontrada.";
        public const string ERRO_RESERVA_NAO_FINALIZADA = "A reserva informada não foi finalizada.";
        public const string ERRO_RESERVA_JA_AVALIADA = "A reserva informada já foi avaliada.";

        public const string ERRO_LOGIN_NAO_ENCONTRADO = "O login informado não foi encontrado.";
        public const string ERRO_LOGIN_JA_CADASTRADO = "O login informado já está cadastrado.";
        public const string ERRO_EMAIL_JA_CADASTRADO = "O e-mail informado já está cadastrado.";

        public const string ERRO_MESAS_INVALIDAS = "Uma ou mais mesas informadas são inválidas.";
        public const string ERRO_TURNOS_INVALIDOS = "Um ou mais turnos informados são inválidos.";

        public const string ERRO_DATA_RESERVA_INVALIDA = "O restaurante não estará funcionando no dia informado na reserva.";

        public const string ERRO_CLIENTE_NAO_ENCONTRADO = "O login e/ou senha informados estão incorretos.";

        public const string ERRO_RESERVA_MESAS_OCUPADAS = "A reserva não pôde ser concluída, pois as mesas informadas já foram ocupadas.";

        public const string ERRO_STATUS_INVALIDO = "A reserva não pôde ser editada, pois não encontra-se no status \"Reservado\".";
    }
}
