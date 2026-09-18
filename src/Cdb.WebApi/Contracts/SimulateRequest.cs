using System.Text.Json.Serialization;

namespace Cdb.WebApi.Contracts
{
    public class SimulateRequest
    {
        [JsonPropertyName("valorInicial")]
        public decimal? InitialValue { get; set; }

        [JsonPropertyName("prazoMeses")]
        public int? TermInMonths { get; set; }
    }
}
