using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokeApi.Helpers;
using PokeApi.Repositories.Usuario;
using PokeApi.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokeApi.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }
        public async Task<string> ObtenerToken(UsuarioRequest usuarioRequest)
        {
            var user = await _usuarioRepository.GetUsuarioByUsernameAsync(usuarioRequest.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(usuarioRequest.Password, user.PasswordHash))
            {
                return "";
            }

            var token = GenerateJwtToken(user.Username, user.Role);
            return token;
        }

        private string GenerateJwtToken(string username, string role)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
               new Claim(JwtRegisteredClaimNames.Sub, username),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {

                throw new ValidacionException($"Error en la solicitud: {ex}");
            }

        }


    }
}
