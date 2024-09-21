using Azure;
using Azure.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PokeApi.Config;
using PokeApi.Helpers;
using PokeApi.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace PokeApi.Services.Pokemon
{
    public class PokeService : PokemonApiClient, IPokeService
    {
        private readonly IPokemonApiClient _pokemonApiClient;

        public PokeService(HttpClient httpClient,
            ILogger<PokemonApiClient> logger,
            IOptions<UrlsConfig> config,
            IHttpContextAccessor httpContext,
            IConfiguration configuration,
             IPokemonApiClient pokemonApiClient) : base(httpClient, logger, config, httpContext, configuration)
        {
            _pokemonApiClient = pokemonApiClient;
        }

        public async Task<List<PokeResponse>> ObtenerPokemons(int id)
        {
            try
            {
                var responseEspecie = await _pokemonApiClient.GetPokemonBySpecie(id);
                if (!responseEspecie.IsSuccessStatusCode)
                {
                    await ManejarError(responseEspecie);
                }

                var responseBodyEspecie = await responseEspecie.Content.ReadAsStringAsync();
                var specieResponse = System.Text.Json.JsonSerializer.Deserialize<EspecieResponse>(responseBodyEspecie);
                var number = ObtenerEvolucionChainId(specieResponse.EvolutionChain.Url);

                var responseEvolucion = await _pokemonApiClient.GetPokemonByEvolution(number);
                if (!responseEvolucion.IsSuccessStatusCode)
                {
                    await ManejarError(responseEvolucion);
                }

                var responseBodyEvolucion = await responseEvolucion.Content.ReadAsStringAsync();
                var pokemonIds = ObtenerPokemonIdsDesdeEvolucion(responseBodyEvolucion);
                pokemonIds.Sort();

                var pokeResponses = new List<PokeResponse>();
                foreach (var idPokemon in pokemonIds)
                {
                    var responsePokemon = await _pokemonApiClient.GetPokemonById(idPokemon);
                    if (!responsePokemon.IsSuccessStatusCode)
                    {
                        await ManejarError(responsePokemon);
                    }

                    var responseBodyPokemon = await responsePokemon.Content.ReadAsStringAsync();
                    var pokemonResponse = System.Text.Json.JsonSerializer.Deserialize<PokemonResponse>(responseBodyPokemon);
                    pokeResponses.Add(new PokeResponse
                    {
                        Id = pokemonResponse.Id,
                        PokeName = pokemonResponse.Name,
                        PokeUrl = pokemonResponse.Sprites.FrontDefault
                    });
                }

                return pokeResponses;
            }
            catch (Exception ex)
            {
                throw new ValidacionException($"Error en la solicitud: {ex.Message}");
            }
        }

        private int ObtenerEvolucionChainId(string especieUrl)
        {
            var segments = especieUrl.Split('/');
            if (segments.Length < 2 || !int.TryParse(segments[segments.Length - 2], out int id))
            {
                throw new ValidacionException("No se encontró cadena de evolución.");
            }
            return id;
        }

        private List<int> ObtenerPokemonIdsDesdeEvolucion(string responseBodyEvolucion)
        {
            var jsonObject = JObject.Parse(responseBodyEvolucion);
            var pokemonIds = new List<int>();
            var currentChain = jsonObject["chain"];

            while (currentChain != null)
            {
                var speciesUrl = currentChain["species"]?["url"]?.ToString();
                if (!string.IsNullOrEmpty(speciesUrl))
                {
                    var segments = speciesUrl.Split('/');
                    if (segments.Length > 1 && int.TryParse(segments[segments.Length - 2], out int id))
                    {
                        pokemonIds.Add(id);
                    }
                }

                currentChain = currentChain["evolves_to"]?.FirstOrDefault();
            }

            return pokemonIds;
        }

        private async Task ManejarError(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseBody);
            var message = jsonResponse?["message"]?.ToString() ?? "Error desconocido";
            throw new ValidacionException($"Error en la solicitud: {message}", message);
        }

    }
}
