using System;
using System.Collections.Generic;

namespace ReserveAqui.Infra.Data.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public TimeSpan HorarioChegadaEsperada { get; set; }
        public DateTime DataReserva { get; set; }
        public int IdCliente { get; set; }
        public int? Avaliacao { get; set; }
        public int IdStatus { get; set; }
        public List<int> IdMesas { get; set; }
        public List<int> IdTurnos { get; set; }
    }
}
