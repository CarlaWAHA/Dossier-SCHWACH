using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Rating> Ratings => Set<Rating>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation Many-to-Many explicite entre Movies et Actors
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany()
                .UsingEntity(j => j.ToTable("ActorMovie"));

            // Optionnel : configurer les clés primaires ou restrictions si besoin
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Actor>().HasKey(a => a.Id);
            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Rating>().HasKey(r => r.Id);
        }
    }
}
