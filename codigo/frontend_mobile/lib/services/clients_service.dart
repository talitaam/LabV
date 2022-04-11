import 'dart:convert' show json, jsonDecode, jsonEncode;
// ignore: avoid_web_libraries_in_flutter
import 'package:frontend_mobile/models/cliente_model.dart';
import 'package:frontend_mobile/shared/utils/variables.dart';
import 'package:http/http.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ClientService {
  Future<List<ClienteModel>> getClientes() async {
    Response res = await get('$BASE_URL/');

    if (res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);
      List<ClienteModel> clientes =
          body.map((dynamic item) => ClienteModel.fromJson(item)).toList();
      return clientes;
    } else {
      throw "Falha ao carregar clientes";
    }
  }

  Future<ClienteModel> getClienteById(int id) async {
    final response = await get('$BASE_URL/reserva/$id');

    if (response.statusCode == 200) {
      return ClienteModel.fromJson(json.decode(response.body));
    } else {
      throw Exception('Falha ao carregar um cliente');
    }
  }

  Future<int> authentication(String login, String senha) async {
    //Future<int> authentication(String login, String senha) async {
    Map data = {'login': login, 'senha': senha};
    final Response response = await post(
      '$BASE_URL/autenticacao',
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(data),
    );

    if (response.statusCode == 200 || response.statusCode == 201) {
      Map<String, dynamic> responseJson = json.decode(response.body);
      print(responseJson["tokenJwt"]);
      var token = responseJson["tokenJwt"];
      var idCliente = responseJson["usuario"]["idCliente"];
      print("Token " + token);
      final prefs = await SharedPreferences.getInstance();
      prefs.setString("token", token);
      prefs.setString("login", login);
      prefs.setInt("idCliente", idCliente);
      return int.parse("200");
    }

    //if (response.statusCode != 200 && response.statusCode != 201) {
    else{
      print(response.statusCode);
      return 400;
    }
  }

  Future<int> createClient(String nome, String login, String email,
      String celular, String senha) async {
    Map data = {
      "login": login,
      "senha": senha,
      "nome": nome,
      "email": email,
      "celular": celular
    };
    final Response response = await post(
      '$BASE_URL/cliente',
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(data),
    );
    print(jsonEncode(data));
    if (response.statusCode == 200 || response.statusCode == 201) {
      return int.parse("200");
    } else {
      return int.parse("400");
    }
  }
}
