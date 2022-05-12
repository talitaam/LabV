import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';

class SqliteService {
  
  final String databaseName = "reserveaqui.db";

  Future<Database> initializeDB() async {
    String path = await getDatabasesPath();
    //String path = "C:/Users/Talita/Desktop/Eng software PUC/5 Período 1-2022/Lab V/codigo/codigo/frontend_mobile/.dart_tool/chrome-device/Default/databases";
    print('path: $path');
    String dbPath = join(path, databaseName);
    
    var database = await openDatabase(dbPath, version: 1, onCreate: populateDb);

    return database;
  }

  final String tableCliente = "CREATE TABLE IF NOT EXISTS cliente ("
  "id_cliente INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,"
  "login TEXT NOT NULL,"
  "senha TEXT NOT NULL,"
  "nome TEXT NOT NULL,"
  "email TEXT NOT NULL,"
  "celular TEXT NOT NULL,"
  "PRIMARY KEY (id_cliente))";

  final String tableReserva = "CREATE TABLE IF NOT EXISTS reserva ("
  "id_reserva INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,"
  "horario_chegada_esperada TEXT NOT NULL,"
  "data_reserva TEXT NOT NULL,"
  "cliente_id_cliente INTEGER NOT NULL,"
  "avaliacao INTEGER NULL,"
  "PRIMARY KEY (id_reserva, cliente_id_cliente),"
  "INDEX fk_reserva_cliente1_idx (cliente_id_cliente ASC) VISIBLE,"
  "CONSTRAINT fk_reserva_cliente1"
    "FOREIGN KEY (cliente_id_cliente)"
    "REFERENCES reserveaqui.cliente (id_cliente)"
    "ON DELETE NO ACTION"
    "ON UPDATE NO ACTION)";
  
  final String tableMesa = "CREATE TABLE IF NOT EXISTS mesa ("
  "id_mesa INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,"
  "qtd_cadeiras INTEGER NOT NULL,"
  "nome_mesa TEXT NOT NULL,"
  "PRIMARY KEY (id_mesa))";

  final String tableRestaurante = "CREATE TABLE IF NOT EXISTS restaurante ("
  "id_restaurante INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,"
  "login TEXT NOT NULL,"
  "senha TEXT NOT NULL,"
  "minutos_limite_chegada INTEGER NULL,"
  "PRIMARY KEY (id_restaurante))";

  final String tableReservaTurno = "CREATE TABLE IF NOT EXISTS reserva_turno ("
  "reserva_id_reserva INTEGER NOT NULL,"
  "turno_id_turno INTEGER NOT NULL,"
  "PRIMARY KEY (reserva_id_reserva, turno_id_turno),"
  "INDEX fk_turno_has_reserva_reserva1_idx (reserva_id_reserva ASC) VISIBLE,"
  "INDEX fk_turno_has_reserva_turno_idx (turno_id_turno ASC) VISIBLE,"
  "CONSTRAINT fk_turno_has_reserva_turno"
    "FOREIGN KEY (turno_id_turno)"
    "REFERENCES reserveaqui.turno (id_turno)"
    "ON DELETE NO ACTION"
    "ON UPDATE NO ACTION,"
  "CONSTRAINT fk_turno_has_reserva_reserva1"
    "FOREIGN KEY (reserva_id_reserva)"
    "REFERENCES reserveaqui.reserva (id_reserva)"
    "ON DELETE NO ACTION"
    "ON UPDATE NO ACTION)";

  final String tableStatus = "CREATE TABLE IF NOT EXISTS status ("
  "id_status INTEGER NOT NULL,"
  "descricao TEXT NOT NULL,"
  "PRIMARY KEY (id_status))";

  final String tableMesaReserva = "CREATE TABLE IF NOT EXISTS mesa_reserva ("
  "mesa_id_mesa INTEGER NOT NULL,"
  "reserva_id_reserva INTEGER NOT NULL,"
  "id_status INTEGER NOT NULL,"
  "PRIMARY KEY (mesa_id_mesa, reserva_id_reserva),"
  "INDEX fk_mesa_has_reserva_reserva1_idx (reserva_id_reserva ASC) VISIBLE,"
  "INDEX fk_mesa_has_reserva_mesa1_idx (mesa_id_mesa ASC) VISIBLE,"
  "INDEX fk_mesa_reserva_status1_idx (id_status ASC) VISIBLE,"
  "CONSTRAINT fk_mesa_has_reserva_mesa1"
    "FOREIGN KEY (mesa_id_mesa)"
    "REFERENCES reserveaqui.mesa (id_mesa)"
    "ON DELETE NO ACTION"
    "ON UPDATE NO ACTION,"
  "CONSTRAINT fk_mesa_has_reserva_reserva1"
    "FOREIGN KEY (reserva_id_reserva)"
    "REFERENCES reserveaqui.reserva (id_reserva)"
    "ON DELETE NO ACTION"
    "ON UPDATE NO ACTION,"
  "CONSTRAINT fk_mesa_reserva_status1"
    "FOREIGN KEY (id_status)"
    "REFERENCES reserveaqui.status (id_status)"
    "ON DELETE NO ACTION"
    "ON UPDATE NO ACTION)";

  final String tableDiasFuncionamento = "CREATE TABLE IF NOT EXISTS dias_funcionamento ("
  "id_dia INTEGER NOT NULL,"
  "restaurante_id_restaurante INTEGER NOT NULL,"
  "dia_descricao TEXT NOT NULL,"
  "ativo TINYINT(1) NOT NULL,"
  "PRIMARY KEY (id_dia, restaurante_id_restaurante),"
  "INDEX fk_dias_funcionamento_restaurante1_idx (restaurante_id_restaurante ASC) VISIBLE,"
  "CONSTRAINT fk_dias_funcionamento_restaurante1"
    "FOREIGN KEY (restaurante_id_restaurante)"
    "REFERENCES reserveaqui.restaurante (id_restaurante)"
    "ON DELETE NO ACTION"
    "ON UPDATE NO ACTION)";


  void populateDb(Database database, int version) async {
    await database.execute(tableCliente);
    await database.execute(tableReserva);
    await database.execute(tableMesa);
    await database.execute(tableRestaurante);
    await database.execute(tableReservaTurno);
    await database.execute(tableStatus);
    await database.execute(tableMesaReserva);
    await database.execute(tableDiasFuncionamento);
    await database.transaction((txn) async {
      await txn.rawInsert(
        "INSERT INTO restaurante (login, senha, minutos_limite_chegada) values ('admin', '654321', 30)");
      await txn.rawInsert(
        "INSERT INTO dias_funcionamento (id_dia, restaurante_id_restaurante, dia_descricao, ativo) values (0, 1, 'Domingo', 1)");
      await txn.rawInsert(
        "INSERT INTO dias_funcionamento (id_dia, restaurante_id_restaurante, dia_descricao, ativo) values (1, 1, 'Segunda-feira', 1)");
      await txn.rawInsert(
        "INSERT INTO dias_funcionamento (id_dia, restaurante_id_restaurante, dia_descricao, ativo) values (2, 1, 'Terça-feira', 1)");
      await txn.rawInsert(
        "INSERT INTO dias_funcionamento (id_dia, restaurante_id_restaurante, dia_descricao, ativo) values (3, 1, 'Quarta-feira', 1)");
      await txn.rawInsert(
        "INSERT INTO dias_funcionamento (id_dia, restaurante_id_restaurante, dia_descricao, ativo) values (4, 1, 'Quinta-feira', 1)");    
      await txn.rawInsert(
        "INSERT INTO dias_funcionamento (id_dia, restaurante_id_restaurante, dia_descricao, ativo) values (5, 1, 'Sexta-feira', 1)");
      await txn.rawInsert(
        "INSERT INTO dias_funcionamento (id_dia, restaurante_id_restaurante, dia_descricao, ativo) values (6, 1, 'Sábado', 1)");      
      await txn.rawInsert(
        "INSERT INTO cliente (id_cliente, login, senha, nome, email, celular) VALUES (1, 'talita.melo', '123', 'Talita Melo', 'talita@email.com', '31999999998')");
      await txn.rawInsert(
        "INSERT INTO status (id_status, descricao) VALUES (1, 'Reservado')");        
      await txn.rawInsert(
        "INSERT INTO status (id_status, descricao) VALUES (2, 'Em uso')");
      await txn.rawInsert(
        "INSERT INTO status (id_status, descricao) VALUES (3, 'Finalizado')");
      await txn.rawInsert(
        "INSERT INTO status (id_status, descricao) VALUES (4, 'Cancelado')");
      await txn.rawInsert(
        "INSERT INTO mesa (id_mesa, qtd_cadeiras, nome_mesa) VALUES (1, 5, 'Mesa A')");
      await txn.rawInsert(
        "INSERT INTO mesa (id_mesa, qtd_cadeiras, nome_mesa) VALUES (2, 2, 'Mesa B')");
      await txn.rawInsert(
        "INSERT INTO mesa (id_mesa, qtd_cadeiras, nome_mesa) VALUES (3, 4, 'Mesa C')");
      await txn.rawInsert(
        "INSERT INTO turno (id_turno, descricao, horario_inicio, horario_fim) VALUES (1, 'Manhã', '09:00:00', '12:00:00')");
      await txn.rawInsert(
        "INSERT INTO turno (id_turno, descricao, horario_inicio, horario_fim) VALUES (2, 'Tarde', '13:00:00', '17:00:00')");
      await txn.rawInsert(
        "INSERT INTO turno (id_turno, descricao, horario_inicio, horario_fim) VALUES (3, 'Noite', '18:00:00', '23:00:00')");
      await txn.rawInsert(
        "INSERT INTO reserva (id_reserva, horario_chegada_esperada, data_reserva, cliente_id_cliente) VALUES (1, '10:30:00', '2022-06-22', 1)");
      await txn.rawInsert(
        "INSERT INTO mesa_reserva (mesa_id_mesa, reserva_id_reserva, id_status) VALUES (1, 1, 1);");
      await txn.rawInsert(
        "INSERT INTO reserva_turno (reserva_id_reserva, turno_id_turno) VALUES (1, 1)");
      await txn.rawInsert(
        "");

    });
  
  }

}