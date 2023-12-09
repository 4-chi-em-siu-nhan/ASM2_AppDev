using ASM2_AppDev.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM2_AppDev.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
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
        }
    }
}
