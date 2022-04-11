import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:frontend_mobile/models/cliente_model.dart';
import 'package:frontend_mobile/provider/clients_provider.dart';
import 'package:frontend_mobile/views/login_screen.dart';

class SignupPage extends StatefulWidget {
  @override
  _SignupPageState createState() => _SignupPageState();
}

class _SignupPageState extends State<SignupPage> {
  ClienteProvider clienteProvider = ClienteProvider();
  TextEditingController nameController = TextEditingController();
  TextEditingController loginController = TextEditingController();
  TextEditingController emailController = TextEditingController();
  TextEditingController celularController = TextEditingController();
  TextEditingController senhaController = TextEditingController();

  @override
  void initState() {
    SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual, overlays: []);
    //SystemChrome.setEnabledSystemUIOverlays([]); //antigo
    // SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual, overlays: [SystemUiOverlay.bottom, SystemUiOverlay.top]);
    //static Future<void> setEnabledSystemUIOverlays(List<SystemUiOverlay> overlays) async {
      //await setEnabledSystemUIMode(SystemUiMode.manual, overlays: overlays);
    //}
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        child: ListView(
          children: <Widget>[
            Container(
              width: MediaQuery.of(context).size.width,
              height: MediaQuery.of(context).size.height / 3.5,
              decoration: BoxDecoration(
                  gradient: LinearGradient(
                    begin: Alignment.topCenter,
                    end: Alignment.bottomCenter,
                    colors: [Colors.orange, Colors.orangeAccent],
                  ),
                  borderRadius:
                      BorderRadius.only(bottomLeft: Radius.circular(90))),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Spacer(),
                  Align(
                    alignment: Alignment.center,
                    child: Icon(
                      Icons.person,
                      size: 90,
                      color: Colors.black,
                    ),
                  ),
                  Spacer(),
                  Align(
                    alignment: Alignment.bottomRight,
                    child: Padding(
                      padding: const EdgeInsets.only(bottom: 32, right: 32),
                      child: Text(
                        'Cadastre-se',
                        style: TextStyle(color: Colors.black, fontSize: 18),
                      ),
                    ),
                  ),
                ],
              ),
            ),
            Container(
              height: MediaQuery.of(context).size.height / 2,
              width: MediaQuery.of(context).size.width,
              padding: EdgeInsets.only(top: 62),
              child: Column(
                children: <Widget>[
                  Container(
                    width: MediaQuery.of(context).size.width / 1.2,
                    height: 45,
                    padding:
                        EdgeInsets.only(top: 4, left: 16, right: 16, bottom: 4),
                    decoration: BoxDecoration(
                        borderRadius: BorderRadius.all(Radius.circular(50)),
                        color: Colors.white,
                        boxShadow: [
                          BoxShadow(color: Colors.black12, blurRadius: 5)
                        ]),
                    child: TextField(
                      controller: nameController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: 'Nome Completo',
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 5,
                  ),
                  Container(
                    width: MediaQuery.of(context).size.width / 1.2,
                    height: 45,
                    padding:
                        EdgeInsets.only(top: 4, left: 16, right: 16, bottom: 4),
                    decoration: BoxDecoration(
                        borderRadius: BorderRadius.all(Radius.circular(50)),
                        color: Colors.white,
                        boxShadow: [
                          BoxShadow(color: Colors.black12, blurRadius: 5)
                        ]),
                    child: TextField(
                      controller: loginController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: 'Login',
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 5,
                  ),
                  Container(
                    width: MediaQuery.of(context).size.width / 1.2,
                    height: 45,
                    padding:
                        EdgeInsets.only(top: 4, left: 16, right: 16, bottom: 4),
                    decoration: BoxDecoration(
                        borderRadius: BorderRadius.all(Radius.circular(50)),
                        color: Colors.white,
                        boxShadow: [
                          BoxShadow(color: Colors.black12, blurRadius: 5)
                        ]),
                    child: TextField(
                      controller: emailController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: 'E-mail',
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 5,
                  ),
                  SizedBox(
                    height: 5,
                  ),
                  Container(
                    width: MediaQuery.of(context).size.width / 1.2,
                    height: 45,
                    padding:
                        EdgeInsets.only(top: 4, left: 16, right: 16, bottom: 4),
                    decoration: BoxDecoration(
                        borderRadius: BorderRadius.all(Radius.circular(50)),
                        color: Colors.white,
                        boxShadow: [
                          BoxShadow(color: Colors.black12, blurRadius: 5)
                        ]),
                    child: TextField(
                      controller: celularController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: 'Celular',
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 5,
                  ),
                  Container(
                    width: MediaQuery.of(context).size.width / 1.2,
                    height: 45,
                    padding:
                        EdgeInsets.only(top: 4, left: 16, right: 16, bottom: 4),
                    decoration: BoxDecoration(
                        borderRadius: BorderRadius.all(Radius.circular(50)),
                        color: Colors.white,
                        boxShadow: [
                          BoxShadow(color: Colors.black12, blurRadius: 5)
                        ]),
                    child: TextField(
                      obscureText: true,
                      controller: senhaController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: 'Senha',
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 15,
                  ),
                  InkWell(
                    onTap: () async {
                      final responseCode = await clienteProvider.criarCliente(
                          nameController.text,
                          loginController.text,
                          emailController.text,
                          celularController.text,
                          senhaController.text);

                      if (responseCode == 200) {
                        ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                          content: Text("Cadastro feito com sucesso!"),
                        ));
                        sleep(const Duration(seconds: 2));
                        Navigator.of(context).push(MaterialPageRoute(
                            builder: (context) => LoginPage()));
                      } else {
                        ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                          content: Text("Erro no cadastro"),
                        ));
                        nameController.clear();
                        loginController.clear();
                        emailController.clear();
                        celularController.clear();
                        senhaController.clear();
                      }
                    },
                    child: Container(
                      height: 45,
                      width: MediaQuery.of(context).size.width / 1.2,
                      decoration: BoxDecoration(
                          gradient: LinearGradient(
                            colors: [Colors.orange, Colors.orangeAccent],
                          ),
                          borderRadius: BorderRadius.all(Radius.circular(50))),
                      child: Center(
                        child: Text(
                          'Cadastre-se'.toUpperCase(),
                          style: TextStyle(
                              color: Colors.black, fontWeight: FontWeight.bold),
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
            InkWell(
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Text("Ja tem uma conta?"),
                  Text(
                    "Login",
                    style: TextStyle(color: Color.fromRGBO(255, 69, 0, 0)),
                  ),
                ],
              ),
              onTap: () {
                Navigator.pop(context);
              },
            ),
          ],
        ),
      ),
    );
  }
}
