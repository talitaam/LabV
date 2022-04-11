import 'package:flutter/material.dart';
import 'package:frontend_mobile/models/cliente_model.dart';
import 'package:frontend_mobile/provider/mesaProvider.dart';
import 'package:frontend_mobile/provider/reservaProvider.dart';
import 'package:frontend_mobile/provider/turnoProvider.dart';
import 'package:frontend_mobile/routes/app_routes.dart';
import 'package:frontend_mobile/views/login_screen.dart';

import 'package:frontend_mobile/views/reserva/reserva_form.dart';
import 'package:frontend_mobile/views/reserva/reserva_form_mesas.dart';
import 'package:provider/provider.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatefulWidget {
  @override
  _MyAppState createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider<ReservaProvider>.value(
          value: ReservaProvider(),
        ),
        ChangeNotifierProvider<TurnoProvider>.value(
          value: TurnoProvider(),
        ),
        ChangeNotifierProvider<MesaProvider>.value(
          value: MesaProvider(),
        )
      ],
      child: MaterialApp(
        title: 'LoginPage',
        theme: ThemeData(
          primaryColor: Colors.orange[700],
        ),
        home: MyHomePage(title: 'LoginPage'),
        routes: {
          AppRoutes.RESERVA_FORM: (ctx) => ReservaForm(),
          AppRoutes.RESERVA_FORM_MESAS: (ctx) => ReservaFormMesas(),
          AppRoutes.CLIENTE_FORM: (ctx) => LoginPage()
        },
      ),
    );
  }
}

class ClienteForm {}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key, this.title}) : super(key: key);

  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  // final ClienteService clienteService = ClienteService();
  List<ClienteModel> clientesApi;

  @override
  Widget build(BuildContext context) {
    if (clientesApi == null) {
      clientesApi = <ClienteModel>[];
      //List<ClienteModel>.empty(growable: true)
      //List<ClienteModel>()
    }

    return Scaffold(
      body: LoginPage(),
    );
  }
}
