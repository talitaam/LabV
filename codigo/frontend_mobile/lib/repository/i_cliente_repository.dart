import 'package:frontend_mobile/models/cliente_model.dart';

abstract class IClienteRepository {
  Future<List<ClienteModel>> findAllClientes();
}
