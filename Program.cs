using Microsoft.EntityFrameworkCore;
using PokeApi.Config;
using PokeApi.Models;
using PokeApi.Services;
using PokeApi.Services.Pokemon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<PokemonApiClient>();
builder.Services.AddLogging();
builder.Services.AddTransient<HttpClient>();
builder.Services.AddHttpClient<IPokemonApiClient, PokemonApiClient>();

// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

//AGREGAR EL SERVICIO
builder.Services.AddScoped<IPokeService, PokeService>();

builder.Services.AddDbContext<PokeApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PokeApiDatabase")));

// Configurar IOptions para UrlsConfig
builder.Services.Configure<UrlsConfig>(builder.Configuration.GetSection("UrlsConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
