import 'package:flutter/cupertino.dart';
import 'package:frontend_mobile/models/reserva.dart';
import 'package:frontend_mobile/models/reserva_model.dart';
import 'package:frontend_mobile/services/reserva_service.dart';

class ReservaProvider extends ChangeNotifier {
  final ReservaService reservaService = ReservaService();
  List<ReservaModel> _reservas;

  ReservaProvider() {
    fetchReservaList();
  }

  setReserva(newValue) {
    _reservas = newValue;
    notifyListeners();
  }

  Future<List<ReservaModel>> fetchReservaList() async {
    Future<List<ReservaModel>> futureReservas = reservaService.getReservas();
    futureReservas.then(
      (reservaList) {
        setReserva(reservaList);
      },
    );

    return futureReservas;
  }

  List<ReservaModel> get all {
    return _reservas;
  }

  void remove(ReservaModel reserva) {
    if (reserva != null && reserva.id != null) {
      reservaService.deleteReserva(reserva.id);
      _reservas.remove(reserva);

      notifyListeners();
    }
  }

  void put(Reserva reserva, context) {
    if (reserva == null) {
      return;
    }

    var containsElement = _reservas.any((element) => element.id == reserva.id);

    if (reserva.id != '' && containsElement) {
      var reservaMapped = new Reserva(
        id: reserva.id,
        horarioChegadaEsperada: reserva.horarioChegadaEsperada,
        dataReserva: reserva.dataReserva,
        cliente: reserva.cliente,
        avaliacao: reserva.avaliacao,
        status: reserva.status,
        mesas: convertListIdsToInt(reserva.mesas),
        turnos: convertListIdsToInt(reserva.turnos),
      );

      reservaService
          .updateReserva(reserva.id, reservaMapped)
          .then((_) => fetchReservaList());
    } else {
      print("Reserva não foi atualizada");
    }
  }

  void create(Reserva reserva, context) {
    if (reserva == null) {
      return;
    }

    var reservaMapped = new Reserva(
      id: reserva.id,
      horarioChegadaEsperada: reserva.horarioChegadaEsperada,
      dataReserva: reserva.dataReserva,
      cliente: reserva.cliente,
      avaliacao: reserva.avaliacao,
      status: reserva.status,
      mesas: convertListIdsToInt(reserva.mesas),
      turnos: convertListIdsToInt(reserva.turnos),
    );

    reservaService.createReserva(reservaMapped).then((_) => fetchReservaList());
  }

  void cancel(ReservaModel reserva) {
    if (reserva == null) {
      return;
    }

    var containsElement = _reservas.any((element) => element.id == reserva.id);

    if (reserva.id != '' && containsElement) {
      reservaService.cancelReserva(reserva.id).then((_) => fetchReservaList());
    } else {
      print("Reserva não foi cancelada");
    }
  }
}

List convertListIdsToInt(listIds) {
  return listIds.map((id) => int.parse(id)).toList();
}

List turnosToList(turnos, turnosProvided) {
  return turnos
      .map((idTurno) =>
          turnosProvided.firstWhere((turno) => turno.id == idTurno))
      .toList();
}
