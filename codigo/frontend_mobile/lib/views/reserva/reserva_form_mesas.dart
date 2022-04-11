import 'package:flutter/material.dart';
import 'package:frontend_mobile/components/validation_error.dart';
import 'package:frontend_mobile/models/mesa_model.dart';
import 'package:signalr_client/signalr_client.dart';
import 'dart:async';

class ReservaFormMesas extends StatefulWidget {
  @override
  _ReservaFormMesasState createState() => _ReservaFormMesasState();
}

class _ReservaFormMesasState extends State<ReservaFormMesas> {
  final _mesas = {};
  Timer _retryConnect;
  Timer _retryList;

  final hubConnection =
      HubConnectionBuilder().withUrl("http://10.0.2.2:5000/chat").build();

  Map<String, dynamic> _args;
  bool _isSomeMesaChecked;
  bool _conectadoWebSocket = false;
  bool _isLoading = true;

  void startConnection() async {
    try {
      if (_conectadoWebSocket)
        await hubConnection.stop(); // Finaliza a conexão ao servidor

      _conectadoWebSocket = false;
      await hubConnection.start(); // Inicia a conexão ao servidor

      if (_retryConnect != null && _retryConnect.isActive)
        _retryConnect.cancel();
      _conectadoWebSocket = true;
    } on Exception catch (err) {
      print(err);
      _retryConnect = Timer(Duration(seconds: 2), () => startConnection());
    }
  }

  @override
  Future<void> didChangeDependencies() async {
    super.didChangeDependencies();
    _args = ModalRoute.of(context).settings.arguments;

    await startConnection();

    hubConnection.onclose((_) {
      print("Conexão perdida");
    });

    hubConnection.on("mesas", (mensagem) => {tratarMesas(mensagem)});

    hubConnection.on("listarMesasNovamente",
        (mensagem) async => {await _sendMessageListarMesas()});

    if (_conectadoWebSocket) {
      await _sendMessageListarMesas();
    }
  }

  void stopConnection() async {
    print("Encerrando conexão webSocket");
    hubConnection.off("mesas");
    hubConnection.off("listarMesasNovamente");
    await hubConnection.stop();
  }

  @override
  void dispose() {
    stopConnection();
    super.dispose();
  }

  void setStateIfMounted(f) {
    if (this.mounted) setState(f);
  }

  void _sendMessageListarMesas() async {
    setStateIfMounted(() {
      _isLoading = true;
    });
    if (_retryList != null && _retryList.isActive) _retryList.cancel();
    var dataReserva = _args['dataReserva'].substring(0, 10);
    var idsTurnos = _args['idsTurnos'].join(',');

    Map mensagem = {'dataReserva': dataReserva, 'idsTurnos': idsTurnos};

    await hubConnection
        .invoke("ListarMesas", args: <Object>[mensagem]).catchError((err) {
      print(err);
      _retryList = Timer(Duration(seconds: 2), () => _sendMessageListarMesas());
    });
  }

  void tratarMesas(mensagem) {
    print(mensagem);
    if (mensagem != null && mensagem.length > 0) {
      List<dynamic> mesasObtidas = mensagem[0];
      var mesas = mesasObtidas.map((dynamic item) => MesaModel.fromJson(item));
      _loadMesas(mesas, _args['reserva']);
      setStateIfMounted(() {
        _isLoading = false;
      });
    }
  }

  void _loadMesas(mesas, reserva) {
    List mesasAtuais =
        reserva != null ? reserva.mesas.map((e) => e.id).toList() : [];

    if (mesas != null) {
      setStateIfMounted(() {
        _mesas.clear();
      });
      mesas.forEach(
        (element) => setStateIfMounted(
          () {
            _mesas[element.id] = {
              'nomeMesa': element.nomeMesa,
              'quantCadeiras': element.quantCadeiras,
              'checked':
                  mesas.any((_) => mesasAtuais.contains(element.id.toString())),
            };
          },
        ),
      );
    }
  }

  String getContentMesa(key) {
    var mesa = _mesas[key];
    return 'Mesa ${mesa['nomeMesa']}: ${mesa['quantCadeiras']} cadeiras';
  }

  List getListMesasChecked() {
    return _mesas.keys.where((key) => _mesas[key]['checked'] == true).toList();
  }

  _buildCheckboxMesa() {
    return _mesas.keys.map((key) {
      return new CheckboxListTile(
        title: new Text(getContentMesa(key)),
        value: _mesas[key]['checked'],
        checkColor: Colors.white,
        onChanged: (bool value) {
          setStateIfMounted(() {
            _mesas[key]['checked'] = value;
            _isSomeMesaChecked =
                getListMesasChecked().length > 0 ? true : false;
          });
        },
      );
    }).toList();
  }

  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: _args['_isEdit'] == true
            ? Text("Editar Reserva")
            : Text("Criar Reserva"),
        backgroundColor: Colors.orange[700],
        actions: <Widget>[
          IconButton(
            icon: Icon(Icons.save),
            onPressed: () {
              if (getListMesasChecked().length > 0) {
                _args['setMesas'](getListMesasChecked());
                _args['onSave']();
                stopConnection();
              } else {
                setStateIfMounted(() {
                  _isSomeMesaChecked = false;
                });
              }
            },
          )
        ],
      ),
      body: Container(
        child: Column(
          children: [
            Expanded(
              child: ListView(
                children: [
                  Padding(
                    padding: EdgeInsets.fromLTRB(0.0, 18.0, 0.0, 0.0),
                    child: Column(
                      children: [
                        Text(
                          'Mesas: ',
                          style: TextStyle(fontSize: 18, color: Colors.black54),
                        ),
                        ValidationError(_isSomeMesaChecked,
                            'É necessário selecionar pelo menos 1 mesa'),
                        if (_isLoading)
                          CircularProgressIndicator()
                        else if (_mesas.isEmpty == true)
                          Text(
                              "Não há mesas disponíveis para essa data e turno")
                        else
                          ..._buildCheckboxMesa()
                      ],
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
