using Microsoft.AspNetCore.Mvc;
using PokeApi.Response;

namespace PokeApi.Services.Pokemon
{
    public interface IPokeService
    {
        public Task<List<PokeResponse>> ObtenerPokemons(int id);
    }
}
