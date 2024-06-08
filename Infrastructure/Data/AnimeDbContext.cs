using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class AnimeDbContext : DbContext
{
    public AnimeDbContext(DbContextOptions<AnimeDbContext> options) : base(options) { }
    public DbSet<Anime> Animes { get; set; }
    public DbSet<PalavraChave> PalavrasChave { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var eTypes = modelBuilder.Model.GetEntityTypes();
        foreach (var type in eTypes)
        {
            var foreignKeys = type.GetForeignKeys();
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        modelBuilder.Entity<Anime>()
            .HasMany(e => e.PalavrasChave)
            .WithMany(e => e.Animes)
            .UsingEntity(j => j
                .ToTable("AnimePalavraChave"));

        modelBuilder.Entity<PalavraChave>()
            .HasKey(e => e.Id);
        
        modelBuilder.Entity<Anime>()
            .HasKey(e => e.Id);
    }
}
