import 'package:frontend_mobile/models/cliente_model.dart';
import 'package:frontend_mobile/models/mesa_model.dart';
import 'package:frontend_mobile/models/status_model.dart';
import 'package:frontend_mobile/models/turno_model.dart';

class ReservaModel {
  final String id;
  final String horarioChegadaEsperada;
  final String dataReserva;
  final ClienteModel cliente;
  final int avaliacao;
  final StatusModel status;
  final List<MesaModel> mesas;
  final List<TurnoModel> turnos;

  const ReservaModel({
    this.id,
    this.horarioChegadaEsperada,
    this.dataReserva,
    this.cliente,
    this.avaliacao,
    this.status,
    this.mesas,
    this.turnos,
  });

  factory ReservaModel.fromJson(Map<String, dynamic> json) => ReservaModel(
        id: json["id"].toString(),
        horarioChegadaEsperada: json["horarioChegadaEsperada"],
        dataReserva: json["dataReserva"],
        cliente: convertCliente(json['cliente']),
        avaliacao: json["avaliacao"],
        status: convertStatus(json["status"]),
        mesas: convertMesas(json["mesas"]),
        turnos: convertTurnos(json["turnos"]),
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "horarioChegadaEsperada": horarioChegadaEsperada,
        "dataReserva": dataReserva,
        "cliente": cliente,
        "avaliacao": avaliacao,
        "status": status,
        "mesas": mesas,
        "turnos": turnos,
      };
}

ClienteModel convertCliente(jsonCliente) {
  ClienteModel cliente;
  if (jsonCliente != null) cliente = ClienteModel.fromJson(jsonCliente);
  return cliente;
}

StatusModel convertStatus(jsonStatus) {
  StatusModel status;
  if (jsonStatus != null) status = StatusModel.fromJson(jsonStatus);
  return status;
}

List<MesaModel> convertMesas(jsonMesas) {
  List<MesaModel> mesas = [];
  if (jsonMesas != null && jsonMesas.isEmpty == false) {
    jsonMesas.forEach((element) => mesas.add(MesaModel.fromJson(element)));
  }
  return mesas;
}

List<TurnoModel> convertTurnos(jsonTurnos) {
  List<TurnoModel> turnos = [];
  if (jsonTurnos != null && jsonTurnos.isEmpty == false)
    jsonTurnos.forEach((element) => turnos.add(TurnoModel.fromJson(element)));
  return turnos;
}
