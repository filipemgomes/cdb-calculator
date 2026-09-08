using Newtonsoft.Json;

namespace Cdb.WebApi.Contracts
{
    public class SimulateResponse
    {
        [JsonProperty("resultadoBruto")]
        public decimal GrossAmount { get; set; }

        [JsonProperty("resultadoLiquido")]
        public decimal NetAmount { get; set; }
    }
}
