using ETickets.Data.Enums;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {   
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Cinemas
            modelBuilder.Entity<Cinema>().HasData(
                new Cinema { Id = 1, Name = "Cinema One", Description = "Main city cinema", Logo = "cinema1.jpg" },
                new Cinema { Id = 2, Name = "Cinema Two", Description = "Suburb cinema", Logo = "cinema2.jpg" },
                new Cinema { Id = 3, Name = "Cinema Three", Description = "New cinema", Logo = "cinema3.jpg" }
            );

            // Seed Producers
            modelBuilder.Entity<Producer>().HasData(
                new Producer { Id = 1, FullName = "John Producer", Bio = "Award-winning producer", ProfilePicture = "producer1.jpg" },
                new Producer { Id = 2, FullName = "Jane Producer", Bio = "Indie film expert", ProfilePicture = "producer2.jpg" },
                new Producer { Id = 3, FullName = "Joy Producer", Bio = "CGI film expert", ProfilePicture = "producer3.jpg" }
            );

            // Seed Actors
            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, FullName = "Tom", Bio = "Action star", ProfilePicture = "actor1.jpg" },
                new Actor { Id = 2, FullName = "Jack", Bio = "Comedy Star", ProfilePicture = "actor2.jpg" },
                new Actor { Id = 3, FullName = "John", Bio = "Mystery Start", ProfilePicture = "actor3.jpg" }
            );

            // Seed Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Name = "Action Movie",
                    Description = "An exciting action movie.",
                    StartDate = new DateTime(2025, 10, 1),
                    EndDate = new DateTime(2025, 12, 1),
                    Price = 12.99,
                    Image = "movie1.jpg",
                    MovieCategory = MovieCategory.Action,
                    CinemaId = 1,
                    ProducerId = 1
                },
                new Movie
                {
                    Id = 2,
                    Name = "Comedy Movie",
                    Description = "A hilarious comedy.",
                    StartDate = new DateTime(2025, 11, 1),
                    EndDate = new DateTime(2026, 1, 1),
                    Price = 10.99,
                    Image = "movie2.jpg",
                    MovieCategory = MovieCategory.Comedy,
                    CinemaId = 2,
                    ProducerId = 2
                },
                new Movie
                {
                    Id = 3,
                    Name = "Mystery Movie",
                    Description = "A Masterpiece Mystery.",
                    StartDate = new DateTime(2025, 11, 1),
                    EndDate = new DateTime(2026, 1, 1),
                    Price = 10.99,
                    Image = "movie3.jpg",
                    MovieCategory = MovieCategory.Documentary,
                    CinemaId = 3,
                    ProducerId = 3
                }
            );

            // Seed Movie-Actor relationships (for many-to-many)
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity(j => j.HasData(
                    new { MoviesId = 1, ActorsId = 1 },
                    new { MoviesId = 2, ActorsId = 2 },
                    new { MoviesId = 3, ActorsId = 3 }
                ));
        }
    }
}
