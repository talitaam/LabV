using System;

namespace ReserveAqui.Infra.Data.Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFim { get; set; }
    }
}
