import 'dart:io';

import 'package:flutter/material.dart';
import 'package:frontend_mobile/provider/clients_provider.dart';
import 'package:frontend_mobile/provider/reservaProvider.dart';
import 'package:frontend_mobile/views/home.dart';
import 'package:provider/provider.dart';

import 'cadastro_cliente.dart';

class LoginPage extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => new _State();
}

class _State extends State<LoginPage> {
  TextEditingController nameController = TextEditingController();
  TextEditingController passwordController = TextEditingController();
  ClienteProvider clienteProvider = ClienteProvider();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text('Reserve Aqui'),
        ),
        body: Padding(
            padding: EdgeInsets.all(10),
            child: ListView(
              children: <Widget>[
                Container(
                    alignment: Alignment.center,
                    padding: EdgeInsets.all(10),
                    child: Text(
                      'Login',
                      style: TextStyle(
                          color: Colors.orange,
                          fontWeight: FontWeight.w500,
                          fontSize: 30),
                    )),
                Container(
                  padding: EdgeInsets.all(10),
                  child: TextField(
                    controller: nameController,
                    decoration: InputDecoration(
                      border: OutlineInputBorder(),
                      labelText: 'Login',
                    ),
                  ),
                ),
                Container(
                  padding: EdgeInsets.fromLTRB(10, 10, 10, 0),
                  child: TextField(
                    obscureText: true,
                    controller: passwordController,
                    decoration: InputDecoration(
                      border: OutlineInputBorder(),
                      labelText: 'Senha',
                    ),
                  ),
                ),
                Container(
                    height: 50,
                    padding: EdgeInsets.fromLTRB(10, 0, 10, 0),
                    child: RaisedButton(
                      textColor: Colors.black,
                      color: Colors.orange,
                      child: Text('Login'),
                      onPressed: () async {
                        print(nameController.text);
                        print(passwordController.text);

                        final responseCode = await clienteProvider.realizaLogin(
                            nameController.text, passwordController.text);
                        print(responseCode);
                        if (responseCode == 200 || responseCode == 201) {
                          ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                            content: Text("Login feito com sucesso!"),
                          ));
                          sleep(const Duration(seconds: 2));
                          Provider.of<ReservaProvider>(context, listen: false)
                              .fetchReservaList();
                          Navigator.of(context).push(
                            MaterialPageRoute(builder: (context) => Home()),
                          );
                        } else {
                          ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                            content: Text("Login incorreto"),
                          ));
                          nameController.clear();
                          passwordController.clear();
                        }
                      },
                    )),
                Container(
                    child: Row(
                  children: <Widget>[
                    Text('Não tem uma conta? Crie aqui!'),
                    FlatButton(
                      textColor: Colors.black,
                      child: Text(
                        'Cadastre-se',
                        style: TextStyle(fontSize: 20),
                      ),
                      onPressed: () {
                        Navigator.of(context).push(MaterialPageRoute(
                            builder: (context) => SignupPage()));
                      },
                    )
                  ],
                  mainAxisAlignment: MainAxisAlignment.center,
                ))
              ],
            )));
  }
}
