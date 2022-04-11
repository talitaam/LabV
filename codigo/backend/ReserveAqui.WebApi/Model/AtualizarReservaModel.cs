using System.Collections.Generic;
using System;

namespace ReserveAqui.WebApi.Model
{
    public class AtualizarReservaModel
    {

        public int Id { get; set; }
        public string HorarioChegadaEsperada { get; set; }
        public DateTime DataReserva { get; set; }
        public int? Avaliacao { get; set; }
        public List<int> Mesas { get; set; }
        public List<int> Turnos { get; set; }
    }
}
