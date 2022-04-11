using ReserveAqui.Domain.DTO;

namespace ReserveAqui.WebApi.Model
{
    public class AtualizarClienteModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
    }

    public static class AtualizarClienteModelExtension
    {
        public static ClienteDto MapearCliente(this AtualizarClienteModel entidade)
        {
            if (entidade == null)
                return null;

            return new ClienteDto()
            {
                IdCliente = entidade.Id,
                Nome = entidade.Nome,
                Senha = entidade.Senha,
                Login = entidade.Login,
                Celular = entidade.Celular,
                Email = entidade.Email
            };
        }
    }
}
