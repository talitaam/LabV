// import 'dart:convert';

class ClienteModel {
  final String idCliente;
  final String nome;
  final String login;
  final String senha;
  final String email;
  final String celular;

  const ClienteModel({
    this.idCliente,
    this.nome,
    this.login,
    this.senha,
    this.email,
    this.celular,
  });

  factory ClienteModel.fromJson(Map<String, dynamic> json) => ClienteModel(
        // idCliente: json["idCliente"] as String,
        idCliente: json["idCliente"].toString(),
        nome: json["nome"],
        login: json["login"],
        senha: json["senha"],
        email: json["email"],
        celular: json["celular"],
      );

  Map<String, dynamic> toJson() => {
        "idCliente": int.parse(idCliente),
        "nome": nome,
        "login": login,
        "senha": senha,
        "email": email,
        "celular": celular,
      };
}
