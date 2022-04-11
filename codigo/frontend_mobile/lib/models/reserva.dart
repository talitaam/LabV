class Reserva {
  final String id;
  final String horarioChegadaEsperada;
  final String dataReserva;
  final int cliente;
  final int avaliacao;
  final String status;
  final List mesas;
  final List turnos;

  const Reserva({
    this.id,
    this.horarioChegadaEsperada,
    this.dataReserva,
    this.cliente,
    this.avaliacao,
    this.status,
    this.mesas,
    this.turnos,
  });
}
