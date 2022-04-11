import 'dart:convert';
import 'package:frontend_mobile/models/reserva.dart';
import 'package:frontend_mobile/models/reserva_model.dart';
import 'package:frontend_mobile/shared/utils/variables.dart';
import 'package:http/http.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ReservaService {
  Future<List<ReservaModel>> getReservas() async {
    final prefs = await SharedPreferences.getInstance();

    Response res = await get(
      '$BASE_URL/reserva/${prefs.getString("login")}',
      headers: <String, String>{
        'Accept': 'application/json; charset=UTF-8',
        'Content-Type': 'application/json; charset=UTF-8',
      },
    );

    if (res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);
      List<ReservaModel> reservas =
          body.map((dynamic item) => ReservaModel.fromJson(item)).toList();
      return reservas;
    } else {
      throw "Falha ao carregar a lista de reservas";
    }
  }

  Future<ReservaModel> getReservaById(int id) async {
    final response = await get('$BASE_URL/reserva/$id');

    if (response.statusCode == 200) {
      return ReservaModel.fromJson(json.decode(response.body));
    } else {
      throw Exception('Falha ao carregar um reserva');
    }
  }

  Future<void> deleteReserva(String id) async {
    Response res = await delete('$BASE_URL/reserva/${int.parse(id)}');

    if (res.statusCode == 200) {
      print("Reserva deletada!");
    } else {
      throw "Falha ao deletar reserva.";
    }
  }

  Future<void> cancelReserva(String id) async {
    Map data = {'idReserva': int.parse(id), 'idStatus': 4};
    Response res = await put(
      '$BASE_URL/reserva/status',
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(data),
    );

    if (res.statusCode == 200) {
      print("Reserva cancelada!");
    } else {
      throw "Falha ao cancelar reserva.";
    }
  }

  Future updateReserva(String id, Reserva reserva) async {
    Map data = {
      'id': int.parse(reserva.id),
      'horarioChegadaEsperada': reserva.horarioChegadaEsperada,
      'dataReserva': reserva.dataReserva,
      'turnos': reserva.turnos,
      'mesas': reserva.mesas,
      'avaliacao': 0,
    };

    final Response response = await put(
      '$BASE_URL/reserva',
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(data),
    );
    if (response.statusCode != 200) {
      throw Exception('Falha ao atualizar reserva');
    }
  }

  Future createReserva(Reserva reserva) async {
    final prefs = await SharedPreferences.getInstance();

    Map data = {
      'horarioChegadaEsperada': reserva.horarioChegadaEsperada,
      'dataReserva': reserva.dataReserva,
      "cliente": prefs.getInt("idCliente"),
      "avaliacao": 0, // Hard-coded
      'turnos': reserva.turnos,
      "mesas": reserva.mesas,
    };
    final Response response = await post(
      '$BASE_URL/reserva',
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(data),
    );

    if (response.statusCode != 200 && response.statusCode != 201) {
      throw Exception('Falha ao criar reserva');
    }
  }
}
