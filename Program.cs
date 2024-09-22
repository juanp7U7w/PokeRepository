using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokeApi.Config;
using PokeApi.Models;
using PokeApi.Repositories.Pokemon;
using PokeApi.Repositories.Usuario;
using PokeApi.Services;
using PokeApi.Services.Pokemon;
using PokeApi.Services.Usuario;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpClient<PokemonApiClient>();
builder.Services.AddLogging();
builder.Services.AddTransient<HttpClient>();
builder.Services.AddHttpClient<IPokemonApiClient, PokemonApiClient>();

// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

//AGREGAR EL SERVICIO
builder.Services.AddScoped<IPokeService, PokeService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

//AGREGAR EL REPOSITORIO
builder.Services.AddScoped<IPokeRepository, PokeRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


builder.Services.AddDbContext<PokeApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PokeApiDatabase")));

// Configurar IOptions para UrlsConfig
builder.Services.Configure<UrlsConfig>(builder.Configuration.GetSection("UrlsConfig"));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Configuración de middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAllOrigins");


app.MapControllers();

app.Run();
