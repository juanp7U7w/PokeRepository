using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokeApi.Helpers;
using PokeApi.Models;
using PokeApi.Request;
using PokeApi.Response;
using PokeApi.Services.Usuario;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PokeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] UsuarioRequest request)
        {
            try
            {
                var response = await _usuarioService.ObtenerToken(request);

                if (response!="")
                {
                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (ValidacionException ex)
            {
                if (ex.Codigo == "400")
                    return BadRequest(ex.Detalle.ToString());
                if (ex.Codigo == "401")
                    return Unauthorized(ex.Detalle.ToString());
                if (ex.Codigo == "403")
                    return StatusCode(403, ex.Detalle.ToString());
                return UnprocessableEntity(ex.Detalle.ToString());
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new { error = true, msg = "Error desconocido" });
            }

        }
       
    }

}
