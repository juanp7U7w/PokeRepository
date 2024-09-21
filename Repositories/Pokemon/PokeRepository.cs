using Microsoft.EntityFrameworkCore;
using PokeApi.Models;

namespace PokeApi.Repositories.Pokemon
{
    public class PokeRepository : RepositoryBase, IPokeRepository
    {
        public PokeRepository(PokeApiContext context) : base(context)
        {
        }

        public async Task AddPokemonAsync(PokeApi.Models.Pokemon pokemon)
        {
            await Context.Pokemons.AddAsync(pokemon);
            await Context.SaveChangesAsync();
        }

        public async Task AddEvolucionAsync(PokeApi.Models.Evolucion evolucion)
        {
            await Context.Evoluciones.AddAsync(evolucion);
            await Context.SaveChangesAsync();
        }
        public async Task<Models.Pokemon> GetPokemonByIdPokemonAsync(int idPokemon)
        {
            return await Context.Pokemons.FirstOrDefaultAsync(p => p.IdPokemon == idPokemon);
        }

        public async Task<Evolucion> GetEvolucionByIdPokemonAsync(int idPokemon)
        {
            return await Context.Evoluciones.FirstOrDefaultAsync(e => e.EvolvesTo == idPokemon.ToString());
        }
        public async Task<List<Evolucion>> GetEvolucionesByPokemonBaseIdAsync(int pokemonBaseId)
        {
            return await Context.Evoluciones.Where(e => e.PokemonBaseId == pokemonBaseId).ToListAsync();
        }
    }
}
