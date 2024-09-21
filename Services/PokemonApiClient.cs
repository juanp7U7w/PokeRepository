using Microsoft.Extensions.Options;
using PokeApi.Config;

namespace PokeApi.Services
{
    public class PokemonApiClient : ApiClientBase, IPokemonApiClient
    {
        public PokemonApiClient(HttpClient httpClient,
    ILogger<PokemonApiClient> logger,
    IOptions<UrlsConfig> config,
    IHttpContextAccessor httpContext,
    IConfiguration configuration
    ) : base(httpClient, logger, config, httpContext, configuration,null)
        {

        }


        public async Task<HttpResponseMessage> GetPokemonByEvolution(int id)
        {
            var apiUrl = _pokeUrl + UrlsConfig.Operations.GetPokemonByEvolution(id);
            _apiClient.DefaultRequestHeaders.Add("accept", "application/json");
            return await _apiClient.GetAsync(apiUrl);
        }

        public async Task<HttpResponseMessage> GetPokemonById(int id)
        {
            var apiUrl = _pokeUrl + UrlsConfig.Operations.GetPokemonById(id);
            _apiClient.DefaultRequestHeaders.Add("accept", "application/json");
            return await _apiClient.GetAsync(apiUrl);

        }

        public async Task<HttpResponseMessage> GetPokemonBySpecie(int id)
        {
            var apiUrl = _pokeUrl + UrlsConfig.Operations.GetPokemonBySpecie(id);
            _apiClient.DefaultRequestHeaders.Add("accept", "application/json");
            return await _apiClient.GetAsync(apiUrl);
        }
    }
}
