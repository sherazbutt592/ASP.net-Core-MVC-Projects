using Diary_App.Models;
using Microsoft.EntityFrameworkCore;
namespace Diary_App.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<DiaryEntry> DiaryEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DiaryEntry>().HasData(
               new DiaryEntry
               {
                   Id = 1,
                   Title = "Went to Diving",
                   Content = " Went to Diving with joe",
                   Created = new DateTime(2025, 8, 24, 10, 0, 0)
               },
               new DiaryEntry
               {
                   Id = 2,
                   Title = "Went to Diving",
                   Content = " Went to Diving with joe",
                   Created = new DateTime(2025, 8, 24, 10, 0, 0)
               },
               new DiaryEntry
               {
                   Id = 3,
                   Title = "Went to Diving",
                   Content = " Went to Diving with joe",
                   Created = new DateTime(2025, 8, 24, 10, 0, 0)
               }
                );
        }
    }
}
