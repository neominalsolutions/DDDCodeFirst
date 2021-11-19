using EFCodeFirstApp.Models.Aggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models
{
    public class ECommerceDbContext: DbContext
    {

        //public ECommerceDbContext()
        //{

        //}

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
            // DbContextOptions<ECommerceDbContext>  hangi veri tabanı teknolojisine hangi queryString ile bağlanacağım bilgisi olucak.
            // bu sınıfın nasıl çalışacağını constructor vasıtası ile söylüyoruz.

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=ECommerceDB;Pooling=true;");
            base.OnConfiguring(optionsBuilder);
        }

        // Databasedeki tablolarımız
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // entity configürasyon işlemleri yapıyoruz.
            base.OnModelCreating(modelBuilder);
        }
    }
}
