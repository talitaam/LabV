import 'package:flutter/material.dart';
import 'package:frontend_mobile/models/reserva_model.dart';
import 'package:frontend_mobile/provider/reservaProvider.dart';
import 'package:frontend_mobile/routes/app_routes.dart';
import 'package:frontend_mobile/shared/utils/formatters.dart';
import 'package:provider/provider.dart';

class ReservaTile extends StatelessWidget {
  final ReservaModel reserva;

  const ReservaTile(this.reserva);

  bool showActions() {
    return reserva.status.descricao == 'Reservado';
  }

  @override
  Widget build(BuildContext context) {
    Iterable<String> mesas = reserva.mesas?.map((mesa) => mesa?.nomeMesa);
    Iterable<String> turnos = reserva.turnos?.map((turno) => turno?.descricao);
    String turnosFormatados = turnos?.join(', ');
    String mesasFormatadas = mesas?.join(', ');

    return Card(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Padding(
            padding: EdgeInsets.only(
                left: 10, top: 5, bottom: showActions() == true ? 0 : 5),
            child: Text(
              'Data: ${stringToDate(reserva.dataReserva)}, às ${reserva.horarioChegadaEsperada}',
              style: TextStyle(fontWeight: FontWeight.w400, fontSize: 16),
            ),
          ),
          Padding(
            padding: EdgeInsets.only(
                left: 10, top: 5, bottom: showActions() == true ? 0 : 5),
            child: Text(
              'Turnos: $turnosFormatados',
              style: TextStyle(fontWeight: FontWeight.w400, fontSize: 16),
            ),
          ),
          Padding(
            padding: EdgeInsets.only(
                left: 10, top: 5, bottom: showActions() == true ? 0 : 5),
            child: Text(
              'Mesas: $mesasFormatadas',
              style: TextStyle(fontWeight: FontWeight.w400, fontSize: 16),
            ),
          ),
          Padding(
            padding: EdgeInsets.only(
                left: 10, top: 5, bottom: showActions() == true ? 0 : 5),
            child: Text(
              'Status: ${reserva.status?.descricao}',
              style: TextStyle(fontWeight: FontWeight.w400, fontSize: 16),
            ),
          ),
          if (showActions() == true)
            ButtonBar(
              children: <Widget>[
                IconButton(
                  icon: Icon(Icons.edit),
                  color: Colors.orange,
                  onPressed: () {
                    Navigator.of(context)
                        .pushNamed(AppRoutes.RESERVA_FORM, arguments: reserva);
                  },
                ),
                IconButton(
                  icon: Icon(Icons.close),
                  color: Colors.black54,
                  onPressed: () {
                    showDialog(
                      context: context,
                      builder: (ctx) => AlertDialog(
                        title: Text("Cancelar reserva: "),
                        content: Text("Tem certeza?"),
                        actions: <Widget>[
                          TextButton(
                              onPressed: () {
                                Navigator.of(context).pop(false);
                              },
                              child: Text('Não')),
                          TextButton(
                              onPressed: () {
                                Navigator.of(context).pop(true); // fecha dialog
                              },
                              child: Text('Sim'))
                        ],
                      ),
                    ).then(
                      (confirmado) {
                        if (confirmado) {
                          Provider.of<ReservaProvider>(context, listen: false)
                              .cancel(reserva);
                        }
                      },
                    );
                  },
                ),
              ],
            ),
        ],
      ),
    );
  }
}
