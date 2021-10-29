using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mango.Services.ProductAPI.Models;

namespace Mango.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = 1,
                Name = "Samosa",
                Price = 15,
                Description = "Samosa",
                ImageUrl = "",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = 2,
                Name = "Samosa 2",
                Price = 16,
                Description = "Samosa 2",
                ImageUrl = "",
                CategoryName = "Appetizer 2"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = 3,
                Name = "Samosa 3",
                Price = 17,
                Description = "Samosa 3",
                ImageUrl = "",
                CategoryName = "Appetizer 3"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = 4,
                Name = "Samosa 4",
                Price = 18,
                Description = "Samosa 4",
                ImageUrl = "",
                CategoryName = "Appetizer 4"
            });

        }
    }
}
