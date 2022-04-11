import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

const url = "http://localhost:5000/chat";

class Hub {
  constructor() {
    this.connection = null;
    this._timeoutId = null;
  }

  _buildConnection() {


    this.connection = new HubConnectionBuilder()
        .withUrl(`${url}`)
    .build();


    this.connection.on("mesas", mensagem => {
      console.log(mensagem)
    });

  }

  async startConnection() {
    try {
      this._buildConnection();
      await this.connection.start();

      if (this._timeoutId)
          clearTimeout(this._timeoutId);

      console.log("connected " + this.connection.connectionId);

    } catch (err) {
      console.log(err);
      this._timeoutId = setTimeout(() => this.startConnection(), 2000);
    }
  };
}

export default new Hub();