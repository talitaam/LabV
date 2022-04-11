using Newtonsoft.Json;

namespace ReserveAqui.Infra.Data.Entities
{
    public class DadosAutenticacao
    {
        [JsonProperty("ValidaResult")]
        public Resultado ValidaResultado { get; set; }

        public class Resultado
        {
            [JsonProperty("CodigoLC")]
            public int CodigoLaboratorio { get; set; }
            public string SiglaUsuario { get; set; }
            public string CpfUsuario { get; set; }
            public string DataExpiracao { get; set; }
        }
    }
}
