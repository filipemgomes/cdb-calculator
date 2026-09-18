using System.Text.Json.Serialization;

namespace Cdb.WebApi.Contracts
{
    public class SimulateResponse
    {
        [JsonPropertyName("resultadoBruto")]
        public decimal GrossAmount { get; set; }

        [JsonPropertyName("resultadoLiquido")]
        public decimal NetAmount { get; set; }
    }
}
