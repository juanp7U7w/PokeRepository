using System.Text.Json.Serialization;

namespace PokeApi.Response
{
    public class EvolucionResponse
    {
        [JsonPropertyName("chain")]
        public Chain Chain { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class Chain
    {

        [JsonPropertyName("evolves_to")]
        public List<Chain> EvolvesTo { get; set; }

        [JsonPropertyName("is_baby")]
        public bool IsBaby { get; set; }

        [JsonPropertyName("species")]
        public Species Species { get; set; }
    }

    public class Species
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

}
