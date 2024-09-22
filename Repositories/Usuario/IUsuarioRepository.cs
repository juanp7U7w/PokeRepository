namespace PokeApi.Repositories.Usuario
{
    public interface IUsuarioRepository
    {
        Task<Models.Usuario> GetUsuarioByUsernameAsync(string username);
    }
}
