using Microsoft.EntityFrameworkCore;
using PokeApi.Models;

namespace PokeApi.Repositories.Usuario
{
    public class UsuarioRepository: RepositoryBase, IUsuarioRepository
    {
        public UsuarioRepository(PokeApiContext context) : base(context)
        {
        }

        public async Task<Models.Usuario> GetUsuarioByUsernameAsync(string username)
        {
            return await Context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
