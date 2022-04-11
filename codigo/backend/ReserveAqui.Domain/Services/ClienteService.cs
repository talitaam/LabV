using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Shared;
using ReserveAqui.Shared.Exceptions;
using ReserveAqui.Shared.Validacao;

namespace ReserveAqui.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepositorio clienteRepositorio;

        public ClienteService(IClienteRepositorio clienteRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
        }

        public ClienteDto ObterPorId(int idCliente)
        {
            return clienteRepositorio.ObterPorId(idCliente).MapearCliente();
        }
        
        public ClienteDto ObterPorLogin(string login)
        {
            return clienteRepositorio.ObterPorLogin(login).MapearCliente();
        }

        public ClienteDto ObterPorLoginSenha(string login, string senha)
        {
            Validate.That(login).IsNotNullOrWhiteSpace(Mensagens.ERRO_LOGIN_NAO_INFORMADO);
            Validate.That(senha).IsNotNullOrWhiteSpace(Mensagens.ERRO_SENHA_NAO_INFORMADA);

            return clienteRepositorio.ObterPorLoginSenha(login, senha).MapearCliente();
        }

        public void DeletarPorLogin(string login)
        {
            clienteRepositorio.DeletarPorLogin(login);
        }

        public void Cadastrar(ClienteDto cliente)
        {
            Validate.That(cliente).IsNotNull();
            Validate.That(cliente.Login).IsNotNullOrWhiteSpace(Mensagens.ERRO_LOGIN_NAO_INFORMADO);
            Validate.That(cliente.Email).IsNotNullOrWhiteSpace(Mensagens.ERRO_EMAIL_NAO_INFORMADO);
            Validate.That(cliente.Senha).IsNotNullOrWhiteSpace(Mensagens.ERRO_SENHA_NAO_INFORMADA);
            Validate.That(cliente.Celular).IsNotNullOrWhiteSpace(Mensagens.ERRO_CELULAR_NAO_INFORMADO);
            Validate.That(cliente.Nome).IsNotNullOrWhiteSpace(Mensagens.ERRO_NOME_NAO_INFORMADO);
            
            ValidarLoginEmailDisponiveis(cliente);

            clienteRepositorio.InserirCliente(cliente.ConverterParaEntidade());
        }

        public void Atualizar(ClienteDto cliente)
        {
            Validate.That(cliente).IsNotNull();
            Validate.That(cliente.Login).IsNotNullOrWhiteSpace(Mensagens.ERRO_LOGIN_NAO_INFORMADO);
            Validate.That(cliente.Email).IsNotNullOrWhiteSpace(Mensagens.ERRO_EMAIL_NAO_INFORMADO);
            Validate.That(cliente.Senha).IsNotNullOrWhiteSpace(Mensagens.ERRO_SENHA_NAO_INFORMADA);
            Validate.That(cliente.Celular).IsNotNullOrWhiteSpace(Mensagens.ERRO_CELULAR_NAO_INFORMADO);
            Validate.That(cliente.Nome).IsNotNullOrWhiteSpace(Mensagens.ERRO_NOME_NAO_INFORMADO);

            var clienteObtido = ObterPorLogin(cliente.Login);
            Validate.That(clienteObtido).IsNotNull(Mensagens.ERRO_LOGIN_NAO_ENCONTRADO);

            clienteRepositorio.AtualizarCliente(cliente.ConverterParaEntidade());
        }

        private void ValidarLoginEmailDisponiveis(ClienteDto cliente)
        {
            var clienteRegistrado = clienteRepositorio.ObterPorLoginOuEmail(cliente.Login, cliente.Email);

            if (clienteRegistrado != null)
            {
                if (!string.IsNullOrWhiteSpace(clienteRegistrado.Login) && clienteRegistrado.Login == cliente.Login)
                    throw new ParametroInvalidoException(Mensagens.ERRO_LOGIN_JA_CADASTRADO);
                else if (!string.IsNullOrWhiteSpace(clienteRegistrado.Email) && clienteRegistrado.Email == cliente.Email)
                    throw new ParametroInvalidoException(Mensagens.ERRO_EMAIL_JA_CADASTRADO);
                else
                    throw new ParametroInvalidoException();
            }
        }
    }
}
