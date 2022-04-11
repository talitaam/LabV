using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO.Autenticacao;
using ReserveAqui.Domain.Interfaces;
using ReserveAqui.Shared;
using ReserveAqui.Shared.Validacao;
using System;

namespace ReserveAqui.AppService
{
    public class AutenticarUsuarioPorLoginSenhaAppService : IAutenticarUsuarioPorLoginSenhaAppService
    {
        private readonly IClienteService clienteService;

        public AutenticarUsuarioPorLoginSenhaAppService(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        public UsuarioAutenticadoDto Autenticar(string login, string senha)
        {
            var cliente = clienteService.ObterPorLoginSenha(login, senha);
            Validate.That(cliente).IsNotNull(Mensagens.ERRO_CLIENTE_NAO_ENCONTRADO);

            DateTime dataAtual = DateTime.Now;

            return new UsuarioAutenticadoDto()
            {
                DataAgora = dataAtual,
                DataExpiracao = dataAtual.AddHours(3),
                Usuario = cliente
            };
        }
    }
}