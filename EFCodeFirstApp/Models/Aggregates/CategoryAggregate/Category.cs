using EFCodeFirstApp.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.Aggregates.CategoryAggregate
{
    public class Category: IAggregateRoot
    {
        /// <summary>
        /// Kategorinin birden fazla ürünü olucaktır.
        /// </summary>
        /// <param name="name"></param>
        /// 

        
       
        public Category(string name)
        {
    

            Id = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Category Ismi boş geçilemez");
            }

            Name = name;

        }

        public string Id { get; private set; }
        public string Name { get; private set; }

        private HashSet<Product> products = new HashSet<Product>();

        /// <summary>
        /// Bir kategoride birden fazla ürün bulunabilir.
        /// </summary>
        public IReadOnlySet<Product> Products => products;

        /// <summary>
        /// Ürün eklerken kullanacağınacağımız algoritma
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(string name, short stock, decimal price, bool promotion, IProductAddDomainService domainService)
        {

            bool sameNameExists = products.Any(x => x.Name == name.Trim());

            if (sameNameExists)
            {
                throw new Exception("Aynı isimde ürün sistemde mevcut");
            }

            var product = new Product(name: name, isPromotion: promotion);
            product.SetUnitPrice(price);
            product.SetUnitsInStock(stock);
            product.SetCategory(this);

            // aynı isme sahip ürün girilmesin
            // ürünün min stok miktarı 10 dan büyük
            // ürünün maksimum stok miktarı 100 olsun
            // promosyon ürünlerin fiyatı 0 olarak ayarlanabilir olsun
            // promosyon olmayan ürünlerin fiyatı 0 olamaz.

            products.Add(product);

            var @events = new ProductAdded(
                notifyFrom: "nbuy.oglen@gmail.com",
                notifyTo: "mert.alptekin@neominal.com",
                productName: product.Name,
                price: product.UnitPrice,
                stock: product.UnitsInStock,
                promotion:product.IsPromotion
                );

            // eventi işler
            domainService.onProcess(@events);

            //var @args = new ProductAddedEventArgs(product,"mert.alptekin@neominal.com", "nbuy_oglen@gmail.com");
            //var @events = new ProductAdded(categoryRepository, emailSender);
            //@events.Handle(@args);


        }


        /// <summary>
        /// Kategorinin Name alanının değiştirmek için kullanırız.
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Kategori ismi girmeniz gerekir");
            }

            Name = name.Trim();
        }


        

    }
}
