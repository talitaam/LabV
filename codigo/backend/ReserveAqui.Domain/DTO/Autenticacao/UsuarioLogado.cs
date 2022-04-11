namespace ReserveAqui.Domain.DTO.Autenticacao
{
    public class UsuarioLogado
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Token { get; set; }
    }
}
