using EcommerceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { 
        
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category
                {
                    Id = 1, Name = "Plabon", Order = 1
                },
                new Category
                {
                    Id = 2, Name = "Plabon", Order = 1
                },
                new Category
                {
                    Id = 3, Name = "Plabon", Order = 1
                },
                new Category
                {
                    Id = 4, Name = "Plabon", Order = 1
                }
            ) ; 

        }
    }
}
