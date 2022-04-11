import 'package:flutter/cupertino.dart';
import 'package:frontend_mobile/models/cliente_model.dart';
import 'package:frontend_mobile/services/clients_service.dart';

class ClienteProvider extends ChangeNotifier {
  final ClientService clientService = ClientService();

  Future<int> realizaLogin(String login, String senha) async {
    return clientService.authentication(login, senha);
  }

  Future<int> criarCliente(String nome, String login, String email,
      String celular, String senha) async {
    return clientService.createClient(nome, login, email, celular, senha);
  }
}
