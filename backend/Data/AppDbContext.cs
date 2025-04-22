using Microsoft.EntityFrameworkCore;
using backend.Models; // si tes modèles sont dans un dossier Models

namespace backend.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Rating> Ratings => Set<Rating>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Déclaration explicite de la relation Many-to-Many
    modelBuilder.Entity<Movie>()
        .HasMany(m => m.Actors)
        .WithMany();
    
}

}
