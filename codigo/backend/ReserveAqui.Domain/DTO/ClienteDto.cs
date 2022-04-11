using ReserveAqui.Infra.Data.Entities;

namespace ReserveAqui.Domain.DTO
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
    }

    public static class ClienteDtoExtension
    {
        public static ClienteDto MapearCliente(this Cliente entidade)
        {
            if (entidade == null)
                return null;

            return new ClienteDto()
            {
                Nome = entidade.Nome,
                IdCliente = entidade.IdCliente,
                Login = entidade.Login,
                Celular = entidade.Celular,
                Email = entidade.Email
            };
        }

        public static Cliente ConverterParaEntidade(this ClienteDto entidade)
        {
            if (entidade == null)
                return null;

            return new Cliente()
            {
                Nome = entidade.Nome,
                Login = entidade.Login,
                Senha = entidade.Senha,
                Celular = entidade.Celular,
                Email = entidade.Email
            };
        }
    }
}
