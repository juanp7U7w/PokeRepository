using System.Text.Json.Serialization;
using System;

namespace PokeApi.Response
{
    public class PokemonResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }
    }
    public class Sprites
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }
    }
}
