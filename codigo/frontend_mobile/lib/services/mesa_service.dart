import 'dart:convert';
import 'package:frontend_mobile/models/mesa_model.dart';
import 'package:frontend_mobile/shared/utils/variables.dart';
import 'package:http/http.dart';

class MesaService { //mobile nao castrada mesa, somente consulta
  Future<List<MesaModel>> getMesas(String dataReserva, List idsTurnos) async {
    // TODO: trocar para mesa/disponiveis
    //http://localhost:5000/api/v1/mesa/disponiveis?dataReserva=2021-06-10&idsTurnos=1,2,3
    
    var dataReservaFormatted = dataReserva.substring(0, 10);
    var idsTurnosFormatted = idsTurnos.join(',');

    var URL =
        '$BASE_URL/mesa/disponiveis/?dataReserva=$dataReservaFormatted&idsTurnos=$idsTurnosFormatted';
    Response res = await get(URL);

    if (res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);
      List<MesaModel> mesas =
          body.map((dynamic item) => MesaModel.fromJson(item)).toList();
      return mesas;
    } else {
      throw "Falha ao carregar mesas disponíveis";
    }
  }
}
