using EFCodeFirstApp.Models;
using EFCodeFirstApp.Models.Aggregates.CategoryAggregate;
using EFCodeFirstApp.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //var p = new Product("324324", 2143, 345,true);

            //var repo = new EFCategoryRepository(new ECommerceDbContext(new DbContextOptions<ECommerceDbContext>()));

            //var c = new Category("Category1");
            //var p = new Product("Product1", true);
            //var p2 = new Product("Product2", false);

            //c.AddProduct(p.Name, 11, 100, p.IsPromotion);
            //c.AddProduct(p2.Name, 50, 12, p2.IsPromotion);

            //repo.Add(c);
            //// Kategori ile ürünün birlikte girildiði arayüz
            //// sisteme 3 kayýt gönderdik.


            //// sadece ilgili kategoriye ürün ekleme arayüzü
            //var category = repo.Find(Guid.NewGuid().ToString());
            //var p3 = new Product("Product3", false);
            //category.AddProduct(p3.Name, 50, 12, p3.IsPromotion);
            //repo.Save();

            //// 3 arayüz
            //var category3 = repo.Find(Guid.NewGuid().ToString());
            //category3.SetName("Kategor11");
            //repo.Update(category3);

            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<ECommerceDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddDbContext<ECommerceDbContext>();

            services.AddScoped<ICategoryRepository, EFCategoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
