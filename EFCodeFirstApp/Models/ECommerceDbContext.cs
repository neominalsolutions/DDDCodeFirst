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
