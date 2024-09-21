using PokeApi.Models;

namespace PokeApi.Repositories.Pokemon
{
    public interface IPokeRepository
    {
        Task AddPokemonAsync(PokeApi.Models.Pokemon pokemon);
        Task AddEvolucionAsync(PokeApi.Models.Evolucion evolucion);
        Task<Models.Pokemon> GetPokemonByIdPokemonAsync(int idPokemon);
        Task<Evolucion> GetEvolucionByIdPokemonAsync(int idPokemon);
        Task<List<Evolucion>> GetEvolucionesByPokemonBaseIdAsync(int pokemonBaseId);
    }
}
