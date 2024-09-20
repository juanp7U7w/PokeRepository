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


//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=PokeApi;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorLog>()
            .HasKey(e => e.LogId);  // Define LogId como clave primaria

        modelBuilder.Entity<Evolucion>()
            .HasOne(e => e.PokemonBase)
            .WithMany()
            .HasForeignKey(e => e.PokemonBaseId);

        base.OnModelCreating(modelBuilder);
    }

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
