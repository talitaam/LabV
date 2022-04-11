import 'package:flutter/material.dart';
import 'package:frontend_mobile/components/floating_button.dart';
import 'package:frontend_mobile/components/validation_error.dart';
import 'package:frontend_mobile/models/reserva.dart';
import 'package:frontend_mobile/models/reserva_model.dart';
import 'package:frontend_mobile/models/turno_model.dart';
import 'package:frontend_mobile/provider/reservaProvider.dart';
import 'package:frontend_mobile/provider/turnoProvider.dart';
import 'package:frontend_mobile/routes/app_routes.dart';
import 'package:frontend_mobile/shared/utils/formatters.dart';
import 'package:frontend_mobile/views/reserva/reserva_list.dart';

import 'package:provider/provider.dart';

class ReservaForm extends StatefulWidget {
  @override
  _ReservaFormState createState() => _ReservaFormState();
}

class _ReservaFormState extends State<ReservaForm> {
  final _form = GlobalKey<FormState>();
  final Map<String, String> _formData = {};
  final _turnos = {};
  List _mesasChecked = [];
  ReservaModel reserva;

  var _isEdit = false;
  bool _isSomeTurnoChecked;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();

    reserva = ModalRoute.of(context).settings.arguments;
    final List<TurnoModel> turnos =
        Provider.of<TurnoProvider>(context, listen: false).all;

    _isEdit = reserva != null;

    _loadFormData(reserva);
    _loadTurnos(turnos, reserva);
  }

  void _loadFormData(ReservaModel reserva) {
    if (reserva != null) {
      _formData["id"] = reserva.id;
      _formData["horarioChegadaEsperada"] = reserva.horarioChegadaEsperada;
      _formData["dataReserva"] = reserva.dataReserva;
    }
  }

  void _loadTurnos(turnos, reserva) {
    List turnosAtuais =
        reserva != null ? reserva.turnos.map((e) => e.id).toList() : [];

    turnos.forEach((element) => _turnos[element.id] = {
          'descricao': element.descricao,
          'horarioInicio': element.horarioInicio,
          'horarioFim': element.horarioFim,
          'checked':
              turnos.any((_) => turnosAtuais.contains(element.id.toString())),
        });
  }

  String getContentTurno(key) {
    var turno = _turnos[key];
    return '${turno['descricao']}: de ${turno['horarioInicio']} às ${turno['horarioFim']}';
  }

  List getListTurnosChecked() {
    return _turnos.keys
        .where((key) => _turnos[key]['checked'] == true)
        .toList();
  }

  List _buildCheckboxTurno() {
    return _turnos.keys.map((key) {
      return new CheckboxListTile(
        title: new Text(getContentTurno(key)),
        value: _turnos[key]['checked'],
        checkColor: Colors.white,
        onChanged: (bool value) {
          setState(() {
            _turnos[key]['checked'] = value;
            _isSomeTurnoChecked =
                getListTurnosChecked().length > 0 ? true : false;
          });
        },
      );
    }).toList();
  }

  onSave() {
    if (_isEdit) {
      Provider.of<ReservaProvider>(context, listen: false).put(
        Reserva(
          id: _formData['id'],
          horarioChegadaEsperada: _formData['horarioChegadaEsperada'],
          dataReserva: _formData['dataReserva'],
          turnos: getListTurnosChecked(),
          mesas: _mesasChecked,
        ),
        context,
      );
    } else {
      Provider.of<ReservaProvider>(context, listen: false).create(
        Reserva(
          horarioChegadaEsperada: _formData['horarioChegadaEsperada'],
          dataReserva: _formData['dataReserva'],
          turnos: getListTurnosChecked(),
          mesas: _mesasChecked,
        ),
        context,
      );
    }
    Navigator.of(context).push(
        MaterialPageRoute(builder: (BuildContext context) => ReservaList()));
  }

  bool isValidForm() {
    var isValid = _form.currentState.validate();

    setState(() {
      _isSomeTurnoChecked = getListTurnosChecked().length > 0 ? true : false;
    });

    if (isValid && _isSomeTurnoChecked == true) {
      _form.currentState.save();
      return true;
    }

    return false;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: _isEdit == true ? Text("Editar Reserva") : Text("Criar Reserva"),
        backgroundColor: Colors.orange[700],
      ),
      body: Padding(
        padding: EdgeInsets.all(15),
        child: Form(
          key: _form,
          child: Column(
            children: <Widget>[
              TextFormField(
                initialValue: _formData['horarioChegadaEsperada'],
                decoration:
                    InputDecoration(labelText: 'Horario esperado de chegada:'),
                validator: (value) {
                  // TODO: validar horas
                  if (value == null || value == '') {
                    return 'Horário esperado de chegada não informado';
                  }
                  return null;
                },
                inputFormatters: [TIME_MASK_FORMATTER],
                onSaved: (value) => _formData['horarioChegadaEsperada'] = value,
              ),
              TextFormField(
                initialValue: stringToDate(_formData['dataReserva']),
                validator: (value) {
                  if (value == null || value == '') {
                    return 'Data da reserva não informada.';
                  }

                  DateTime date = DateTime.parse(stringToDateString(value));
                  DateTime today = DateTime.now();
                  var valid = date.difference(today).inMilliseconds;

                  if (valid < 0) {
                    return "A data da reserva deve ser maior que a de hoje.";
                  }

                  return null;
                },
                // TODO: validar data inexistente
                decoration: InputDecoration(labelText: 'Data:'),
                inputFormatters: [DATE_MASK_FORMATTER],
                onSaved: (value) =>
                    _formData['dataReserva'] = stringToDateString(value),
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.start,
                children: [
                  Expanded(
                    child: Padding(
                      padding: EdgeInsets.fromLTRB(0.0, 18.0, 0.0, 0.0),
                      child: Column(children: [
                        Text(
                          'Turnos: ',
                          style: TextStyle(fontSize: 16, color: Colors.black54),
                        ),
                        ListView(
                          shrinkWrap: true,
                          children: _buildCheckboxTurno(),
                        ),
                      ]),
                    ),
                  ),
                ],
              ),
              Row(
                children: [
                  ValidationError(_isSomeTurnoChecked,
                      'É necessário selecionar pelo menos 1 turno'),
                ],
              ),
              FloatingButton(
                'Próximo',
                () => {
                  if (isValidForm())
                    {
                      Navigator.of(context)
                          .pushNamed(AppRoutes.RESERVA_FORM_MESAS, arguments: {
                        'isEdit': _isEdit,
                        'setMesas': (mesas) => {
                              setState(() {
                                _mesasChecked = mesas;
                              })
                            },
                        'onSave': onSave,
                        'reserva': reserva,
                        'dataReserva': _formData['dataReserva'],
                        'idsTurnos': getListTurnosChecked(),
                      })
                    }
                },
              )
            ],
          ),
        ),
      ),
    );
  }
}
