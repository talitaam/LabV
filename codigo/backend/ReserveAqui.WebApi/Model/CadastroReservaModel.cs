using System.Collections.Generic;
using System;

namespace ReserveAqui.WebApi.Model
{
    public class CadastroReservaModel
    {
        public string HorarioChegadaEsperada { get; set; }
        public DateTime DataReserva { get; set; }
        public int Cliente { get; set; }
        public int? Avaliacao { get; set; }
        public List<int> Mesas { get; set; }
        public List<int> Turnos { get; set; }
    }
}
