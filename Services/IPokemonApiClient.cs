namespace PokeApi.Services
{
    public interface IPokemonApiClient
    {
        Task<HttpResponseMessage> GetPokemonById(int id);
        Task<HttpResponseMessage> GetPokemonBySpecie(int id);
        Task<HttpResponseMessage> GetPokemonByEvolution(int id);
    }
}
