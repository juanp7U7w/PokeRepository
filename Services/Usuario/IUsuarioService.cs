using PokeApi.Request;
using PokeApi.Response;

namespace PokeApi.Services.Usuario
{
    public interface IUsuarioService
    {
        public Task<string> ObtenerToken(UsuarioRequest usuarioRequest);
    }
}
