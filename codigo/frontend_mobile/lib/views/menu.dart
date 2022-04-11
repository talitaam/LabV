import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:frontend_mobile/views/reserva/reserva_list.dart';

final Color backgroundColor = Color(0xFFFF7043);

class Menu extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: <Widget>[
          UserAccountsDrawerHeader(
            accountName: Text("Camila Campos"),
            accountEmail: Text("camila@gmail.com"),
            decoration: BoxDecoration(color: Colors.orange[700]),
            currentAccountPicture: CircleAvatar(
              backgroundColor: Colors.white,
              child: Text(
                "CC",
                style: TextStyle(fontSize: 40.0, color: Colors.black38),
              ),
            ),
          ),
          ListTile(
            title: Text("Minhas reservas"),
            trailing: Icon(Icons.arrow_forward),
            onTap: () {
              Navigator.of(context).pop();
              Navigator.of(context).push(MaterialPageRoute(
                  builder: (BuildContext context) => ReservaList()));
            },
          ),
          ListTile(
            title: Text("Meus dados"),
            trailing: Icon(Icons.arrow_forward),
          ),
        ],
      ),
    );
  }
}
