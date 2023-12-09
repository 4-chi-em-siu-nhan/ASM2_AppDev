using ASM2_AppDev.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM2_AppDev.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Horror", Description = "So scary"},
                new Category { Id = 2, Name = "Action", Description = "So cool"},
                new Category { Id = 3, Name = "Romance", Description = "So romance"},
                new Category { Id = 4, Name = "Science", Description = "So difficult"}
            );
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "C# Programming",
					Author = "Anne",
					Description = "Hello",
                    Publisher = "Kim Dong",
                    Price = 10,
                    Quantity = 10,
                    CategoryId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "Java Programming",
					Author = "Jane",
					Description = "Hello",
                    Publisher = "Kim Dong",
                    Price = 15,
                    Quantity = 15,
                    CategoryId = 3
                },
                new Book
                {
                    Id = 3,
                    Title = "Python Programming",
					Author = "Billy",
					Description = "Hello",
                    Publisher = "Kim Dong",
                    Price = 20,
                    Quantity = 20,
                    CategoryId = 2
                },
                new Book
                {
                    Id = 4,
                    Title = "C Programming",
					Author = "Jessica",
					Description = "Hello",
                    Publisher = "Kim Dong",
                    Price = 15,
                    Quantity = 15,
                    CategoryId = 4
                }
            );
        }
    }
}
