using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PokeApi.Models;

public partial class PokeApiContext : DbContext
{
    public PokeApiContext()
    {
    }

    public PokeApiContext(DbContextOptions<PokeApiContext> options)
        : base(options)
    {
    }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<ErrorLog> ErrorLogs { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Evolucion> Evoluciones { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorLog>()
            .HasKey(e => e.LogId); 

        modelBuilder.Entity<Evolucion>()
            .HasOne(e => e.PokemonBase)
            .WithMany()
            .HasForeignKey(e => e.PokemonBaseId);

        base.OnModelCreating(modelBuilder);
    }

}
