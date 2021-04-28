using Microsoft.EntityFrameworkCore;
using OOP_L_4_L_5.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_L_4_L_5.Storage
{
    public class RestShopDataContext : DbContext
    {
        public RestShopDataContext(DbContextOptions<RestShopDataContext> options) : base(options)
        {

        }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Figure> Figures { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentInfo> PaymentInfos{ get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(p => p.PaymentInfos);

            modelBuilder.Entity<OrderProduct>()
            .HasKey(t => new { t.OrderId, t.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(sc => sc.Order)
                .WithMany(s => s.OrderProducts)
                .HasForeignKey(sc => sc.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(sc => sc.Product)
                .WithMany(c => c.OrderProducts)
                .HasForeignKey(sc => sc.ProductId);
        }

    }
}
