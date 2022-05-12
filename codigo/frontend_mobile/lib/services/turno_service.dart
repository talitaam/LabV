import 'dart:convert';
import 'package:frontend_mobile/models/turno_model.dart';
import 'package:frontend_mobile/shared/utils/variables.dart';
import 'package:http/http.dart';

class TurnoService { //o mobile nao cadastra turno, somente consulta
  Future<List<TurnoModel>> getTurnos() async {
    Response res = await get('$BASE_URL/turno');

    if (res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);
      List<TurnoModel> turnos =
          body.map((dynamic item) => TurnoModel.fromJson(item)).toList();
      return turnos;
    } else {
      throw "Falha ao carregar turnos";
    }
  }
}
