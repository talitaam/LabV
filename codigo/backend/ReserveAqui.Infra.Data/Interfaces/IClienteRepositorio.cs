using ReserveAqui.Infra.Data.Entities;

namespace ReserveAqui.Infra.Data.Interfaces
{
    public interface IClienteRepositorio
    {
        Cliente ObterPorId(int id);
        Cliente ObterPorLogin(string login);
        Cliente ObterPorLoginOuEmail(string login, string email);
        Cliente ObterPorLoginSenha(string login, string senha);
        void InserirCliente(Cliente cliente);
        void AtualizarCliente(Cliente cliente);
        void DeletarPorLogin(string login);
    }
}
