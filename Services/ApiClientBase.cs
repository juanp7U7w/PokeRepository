using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using PokeApi.Config;

namespace PokeApi.Services
{
    public class ApiClientBase
    {

        protected readonly ILogger _logger;
        protected readonly UrlsConfig _urls;
        protected readonly IHttpContextAccessor _httpContext;
        protected readonly JsonSerializerOptions _serializerOptions;
        protected readonly HttpClient _apiClient;
        protected readonly IPokemonApiClient _pokemonApiClient;
        protected readonly string _pokeUrl;
        public ApiClientBase(HttpClient apiClient,
            ILogger logger,
            IOptions<UrlsConfig> config,
            IHttpContextAccessor httpContext,
            IConfiguration configuration,
            IPokemonApiClient pokemonApiClient)
        {
            _pokeUrl = configuration.GetValue<string>("urls:PokemonUrl");
            _urls = config.Value;
            _apiClient = apiClient;
            _logger = logger;
            _httpContext = httpContext;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            _pokemonApiClient = pokemonApiClient;
        }
    }
}
