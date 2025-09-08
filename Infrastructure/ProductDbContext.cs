using Microsoft.EntityFrameworkCore;
using ProductsControl.Models;
using System;

namespace ProductsControl.Infrastructure
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTime)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Order>()
                .Property(o => o.DeliveryTime)
                .HasColumnType("timestamp without time zone");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
        }
    }
}
