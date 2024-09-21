using PokeApi.Models;

namespace PokeApi.Repositories
{
    public class RepositoryBase
    {
        public PokeApiContext Context;

        public RepositoryBase(PokeApiContext context)
        {
            this.Context = context;
        }
    }
}
