using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineShop.DB
{
    public class DatabaseContext : DbContext
    {
        private readonly string _dataFilePath = Path.Combine(Environment.CurrentDirectory, "Services/mock_data.json");
        public DbSet<Product> Products { get; set; }
        public DbSet<UserCart> UserCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryContact> DeliveryContacts { get; set; }
        public DbSet<UserFavoriteProduct> UserFavoriteProducts { get; set; }
        public DbSet<UserComparableProduct> UserComparableProducts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Cost).HasColumnType("DECIMAL(10, 0)");
            modelBuilder.Entity<ProductImage>().HasOne(p => p.Product).WithMany(p => p.ProductImages).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);

            if (File.Exists(_dataFilePath))
            {
                IEnumerable<Product> products;
                using (var reader = new StreamReader(_dataFilePath))
                {
                    var serializedString = reader.ReadToEnd();
                    products = JsonConvert.DeserializeObject<List<Product>>(serializedString);
                }

                modelBuilder.Entity<Product>().HasData(products);
            }
        }
    }
}
