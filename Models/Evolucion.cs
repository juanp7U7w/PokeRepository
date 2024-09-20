namespace PokeApi.Models
{
    public class Evolucion
    {
        public int Id { get; set; }

        public int PokemonBaseId { get; set; }

        public string EvolvesTo { get; set; }

        public string ImaUrl { get; set; }

        public Pokemon PokemonBase { get; set; } 
    }
}
