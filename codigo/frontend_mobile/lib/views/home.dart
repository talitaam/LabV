import 'package:flutter/material.dart';

import 'menu.dart';

class Home extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => new _State();
}

class _State extends State<Home> {
  TextEditingController nameController = TextEditingController();
  TextEditingController passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: const [
            Padding(
              padding: EdgeInsets.fromLTRB(0.0, 0.0, 8.0, 0.0),
              child: Icon(Icons.restaurant, size: 24.0),
            ),
            Text('Reserve Aqui'),
          ],
        ),
        backgroundColor: Colors.orange[700],
      ),
      drawer: Menu(),
      body: Center(
        child: new Container(
          child: new Center(
            child: new Text(
              "Bem-vindo ao ReserveAqui",
              style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 20,
                  color: Colors.orange[600]),
            ),
          ),
        ),
      ),
    );
  }
}
