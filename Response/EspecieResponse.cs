using System.Text.Json.Serialization;

namespace PokeApi.Response
{
    public class EspecieResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("is_baby")]
        public bool IsBaby { get; set; }

        [JsonPropertyName("color")]
        public Color Color { get; set; }

        [JsonPropertyName("evolution_chain")]
        public EvolutionChain EvolutionChain { get; set; }
    }

    public class Color
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class EvolutionChain
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
