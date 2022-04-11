// import chatService from "../../services/ChatService";
// import atendimentosService from "../../services/AtendimentosService";
import hub from "../../hubConnection";

class ChatAtendimentoManager {
  
  async conectarChat() {
    return await hub.startConnection();
  }

  async listarMesas() {
    let dataReserva = '2021-05-23';
    let idsTurnos = '1,2';

    return await hub.connection.invoke("ListarMesas", { dataReserva, idsTurnos });
  }

}

export default new ChatAtendimentoManager();