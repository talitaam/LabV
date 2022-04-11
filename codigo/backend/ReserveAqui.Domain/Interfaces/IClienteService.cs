using ReserveAqui.Domain.DTO;

namespace ReserveAqui.Domain.Interfaces
{
    public interface IClienteService
    {
        ClienteDto ObterPorId(int idCliente);
        void DeletarPorLogin(string login);
        ClienteDto ObterPorLogin(string login);
        ClienteDto ObterPorLoginSenha(string login, string senha);
        void Cadastrar(ClienteDto cliente);
        void Atualizar(ClienteDto cliente);
    }
}
