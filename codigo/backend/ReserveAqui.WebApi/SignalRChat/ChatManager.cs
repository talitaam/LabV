using Microsoft.AspNetCore.SignalR;
using ReserveAqui.AppService.Interfaces;
using ReserveAqui.Domain.DTO.Autenticacao;
using ReserveAqui.Domain.DTO.Mensagem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ReserveAqui.WebApi.SignalRChat.ChatCache;

namespace ReserveAqui.WebApi.SignalRChat
{
    public class ChatManager
    {
        private readonly IHubContext<ChatHub> hubContext;
        private readonly IListarMesasDisponiveisAppService listarMesasDisponiveisAppService;
        private readonly ChatCache cache;

        public ChatManager(IHubContext<ChatHub> hubContext, IListarMesasDisponiveisAppService listarMesasDisponiveisAppService)
        {
            this.hubContext = hubContext;
            this.listarMesasDisponiveisAppService = listarMesasDisponiveisAppService;
            cache = new ChatCache();
        }

        public void RegistrarUsuarioAtivo(UsuarioLogado usuario, string idUsuarioChat)
        {
            if(usuario != null)
                cache.RegistrarClienteAtivo(usuario.Id, idUsuarioChat);
        }

        public void RemoverUsuarioAtivo(string connectionId)
        {
            cache.RemoverClienteAtivo(connectionId);
        }

        public Cliente ObterClienteConectado(string connectionId)
        {
            return cache.ObterClienteAtivoPorConexao(connectionId);
        }

        public async Task ReceberMensagemCliente(MensagensRemetenteDto mensagem)
        {
            throw new NotImplementedException();
        }

        public async Task ListarMesasDisponiveis(string connectionId, DateTime dataReserva, List<int> idsTurnos)
        {
            var mesasDisponiveis = listarMesasDisponiveisAppService.ListarDisponiveis(dataReserva, idsTurnos);
            await hubContext.Clients.Clients(connectionId).SendAsync("mesas", mesasDisponiveis);
        }
    }
}
