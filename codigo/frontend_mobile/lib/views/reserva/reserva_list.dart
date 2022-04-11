import 'package:flutter/material.dart';
import 'package:frontend_mobile/components/reserva_tile.dart';
import 'package:frontend_mobile/models/reserva_model.dart';
import 'package:frontend_mobile/provider/reservaProvider.dart';
import 'package:frontend_mobile/routes/app_routes.dart';
import 'package:frontend_mobile/services/reserva_service.dart';
import 'package:frontend_mobile/views/home.dart';
import 'package:provider/provider.dart';

class ReservaList extends StatefulWidget {
  @override
  _ReservaListState createState() => _ReservaListState();
}

class _ReservaListState extends State<ReservaList> {
  final ReservaService reservaService = ReservaService();
  List<ReservaModel> reservas;

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    var reservasProvider = Provider.of<ReservaProvider>(context);
    reservas = reservasProvider.all;

    if (reservas == null) {
      reservas = [];
    }

    Widget _buildChild() {
      if (reservas.isEmpty || reservas == null) {
        return Text("Não há reservas para serem listadas");
      } else {
        return ListView.builder(
          itemCount: reservas.length,
          itemBuilder: (ctx, i) => ReservaTile(reservas[i]),
        );
      }
    }

    return Scaffold(
      appBar: AppBar(
        leading: IconButton(
          icon: Icon(Icons.arrow_back, color: Colors.black),
          onPressed: () => Navigator.of(context).push(
            MaterialPageRoute(builder: (context) => Home()),
          ),
        ),
        title: Text("Minhas reservas"),
        actions: <Widget>[
          IconButton(
              icon: Icon(Icons.add),
              onPressed: () {
                Navigator.of(context).pushNamed(AppRoutes.RESERVA_FORM);
              })
        ],
        backgroundColor: Colors.orange[700],
      ),
      body: new Container(
        child: new Center(child: _buildChild()),
      ),
    );
  }
}
