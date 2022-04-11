import 'package:flutter/cupertino.dart';
import 'package:frontend_mobile/models/turno_model.dart';
import 'package:frontend_mobile/services/turno_service.dart';

class TurnoProvider extends ChangeNotifier {
  final TurnoService turnoService = TurnoService();
  List<TurnoModel> _turnos;

  TurnoProvider() {
    fetchTurnoList();
  }

  setTurno(newValue) {
    _turnos = newValue;
    notifyListeners();
  }

  Future<List<TurnoModel>> fetchTurnoList() async {
    Future<List<TurnoModel>> futureTurnos = turnoService.getTurnos();
    futureTurnos.then(
      (turnoList) {
        setTurno(turnoList);
      },
    );

    return futureTurnos;
  }

  List<TurnoModel> get all {
    return _turnos;
  }
}
