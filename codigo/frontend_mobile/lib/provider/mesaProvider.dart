import 'package:flutter/cupertino.dart';
import 'package:frontend_mobile/models/mesa_model.dart';
import 'package:frontend_mobile/services/mesa_service.dart';

class MesaProvider extends ChangeNotifier {
  final MesaService mesaService = MesaService();
  List<MesaModel> _mesas;

  setMesa(newValue) {
    _mesas = newValue;
    notifyListeners();
  }

  Future<List<MesaModel>> fetchMesaList(dataReserva, idsTurnos) async {
    Future<List<MesaModel>> futureMesas =
        mesaService.getMesas(dataReserva, idsTurnos);
    futureMesas.then(
      (mesaList) {
        setMesa(mesaList);
      },
    );

    return futureMesas;
  }

  Future<List<MesaModel>> all(String dataReserva, List idsTurnos) async {
    await fetchMesaList(dataReserva, idsTurnos);

    return _mesas;
  }
}
