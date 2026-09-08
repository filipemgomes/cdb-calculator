using Newtonsoft.Json;

namespace Cdb.WebApi.Contracts
{
    public class SimulateRequest
    {
        [JsonProperty("valorInicial")]
        public decimal InitialValue { get; set; }

        [JsonProperty("prazoMeses")]
        public int TermInMonths { get; set; }
    }
}
