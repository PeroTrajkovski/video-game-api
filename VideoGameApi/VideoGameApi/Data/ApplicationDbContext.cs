using Microsoft.EntityFrameworkCore;
using VideoGameApi.Models;

namespace VideoGameApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<VideoGame> VideoGames { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<VideoGame>().HasData(
                new VideoGame
                {
                    Id = 1,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Platform = "Nintendo Switch",
                    Developer = "Nintendo",
                    Publisher = "Nintendo"
                },
                new VideoGame
                {
                    Id = 2,
                    Title = "God of War",
                    Platform = "PS4",
                    Developer = "Santa Monica Studio",
                    Publisher = "Sony Interactive Entertainment"
                },
                new VideoGame
                {
                    Id = 3,
                    Title = "Red Dead Redemption 2",
                    Platform = "Multiple",
                    Developer = "Rockstar Games",
                    Publisher = "Rockstar Games"
                },
                new VideoGame
                {
                    Id = 4,
                    Title = "Spider-Man 2",
                    Platform = "PS5",
                    Developer = "Insomniac Games",
                    Publisher = "Sony Interactive Entertainment",
                 }
            );
        }
    }
}
