using System.Text.Json.Serialization;

namespace RadarGov.Integracoes.RadarHub.DTOs
{
    public class ODataResult<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
