namespace PokeApi.Config
{
    public class UrlsConfig
    {
        public string PokemonUrl { get; set; }

        public class Operations
        {
            public static string GetPokemonById(int id) => $"/api/v2/pokemon/{id}";

            public static string GetPokemonBySpecie(int id) => $"/api/v2/pokemon-species/{id}";
            public static string GetPokemonByEvolution(int id) => $"/api/v2/evolution-chain/{id}";

        }
    }
}
