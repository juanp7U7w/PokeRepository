using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeApi.Helpers;
using PokeApi.Models;
using PokeApi.Response;
using PokeApi.Services.Pokemon;
using System.Net;

namespace PokeApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class PokeController : ControllerBase
    {
        private readonly IPokeService _pokeService;
        public PokeController(IPokeService pokeService)
        {
            _pokeService = pokeService;
        }
        [Route("Pokemons")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<PokeResponse>>> GetPokemons(int id)
        {
            try
            {
                var response = await _pokeService.ObtenerPokemons(id);

                return Ok(response);
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
