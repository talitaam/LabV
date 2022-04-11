using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ReserveAqui.WebApi.Model;
using ReserveAqui.WebApi.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveAqui.WebApi.SignalRChat
{
    public class ChatHub : Hub
    {
        private readonly ChatManager chatManager;

        public ChatHub(ChatManager chatManager)
        {
            this.chatManager = chatManager;
        }
        
        public async Task ListarMesas(MensagemMesasModel mensagem)
        {
            var turnos = mensagem.IdsTurnos.Split(",").Select(id => id.Trim()).Select(int.Parse).ToList();

            var cliente = chatManager.ObterClienteConectado(Context.ConnectionId);

            await chatManager.ListarMesasDisponiveis(Context.ConnectionId, mensagem.DataReserva, turnos);
        }

        public override async Task OnConnectedAsync()
        {
            var usuarioLogado = AutenticacaoUtil.ObterUsuario(Context.User);
            await base.OnConnectedAsync();
            chatManager.RegistrarUsuarioAtivo(usuarioLogado, Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            chatManager.RemoverUsuarioAtivo(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
