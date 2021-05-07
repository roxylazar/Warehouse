using Microsoft.EntityFrameworkCore;
using System;
using WarehouseData.Models;

namespace WarehouseData
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext() { }
        public WarehouseContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Satay Chicken Nuddeln" },
                new Product { Id = 2, Name = "Cannelloni al Formaggio" },
                new Product { Id = 3, Name = "Dumpling Soup" },
                new Product { Id = 4, Name = "The Salad Bowl" },
                new Product { Id = 5, Name = "Lasagne con Pomodoro" },
                new Product { Id = 6, Name = "Spinach Salad" },
                new Product { Id = 7, Name = "Muesli with Raspberry" },
                new Product { Id = 8, Name = "Nut Butter Balls" },
                new Product { Id = 9, Name = "Popcorn with Raspberry" },
                new Product { Id = 10, Name = "Yogurt Parfait" }
            );

            modelBuilder.Entity<Batch>().HasData(
                new
                {
                    Id = 1,
                    ExpirationDate = new DateTime(2021, 4, 20),
                    Quantity = 10,
                    ProductId = 1
                },
                new
                {
                    Id = 2,
                    ExpirationDate = new DateTime(2021, 5, 17),
                    Quantity = 50,
                    ProductId = 9
                },
                new
                {
                    Id = 3,
                    ExpirationDate = new DateTime(2021, 3, 17),
                    Quantity = 3,
                    ProductId = 10
                },
                 new
                 {
                     Id = 4,
                     ExpirationDate = new DateTime(2021, 5, 7),
                     Quantity = 25,
                     ProductId = 6
                 }
                );
        }
    }
}