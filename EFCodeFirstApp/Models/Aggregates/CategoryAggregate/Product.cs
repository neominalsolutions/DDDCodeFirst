
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.Aggregates.CategoryAggregate
{
    public class Product
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public short UnitsInStock { get; private set; }
        public bool IsPromotion { get; private set; } = false;

        // parametre

        public Product(string name, bool isPromotion = false)
        {
            Id = Guid.NewGuid().ToString();
            Name = name.Trim();// boşluk alma
            IsPromotion = isPromotion; 
        }

        public Category Category { get; private set; }

        /// <summary>
        /// Seçilen bir categoryId üzerinden işlem yapılacak bir method 
        /// </summary>
        /// <param name="categoryId"></param>
        public void SetCategory(Category category)
        {
            // sistemde olmayan bir kategori product atanamaz
            // category alanı zorunlu alan olup boş geçilemez

            if (category == null)
            {
                throw new Exception("Kategori girmek zorunludur");
            }

            Category = category;

        }

        public void SetUnitPrice(decimal unitPrice)
        {
            if (unitPrice < 0)
            {
                throw new Exception("Ürüne Negatif bir fiyat değeri biçilemez");
            }

            if (!IsPromotion && unitPrice == 0)
            {
                throw new Exception("Promosyon olmayan bir ürünün fiyatı 0 olarak sisteme girilemez");
            }

            UnitPrice = unitPrice;
            
        }

        /// <summary>
        /// Bir ürün oluştırulurken ürün stoğunun nasıl girileceğinin logicini yönettiğimiz method
        /// </summary>
        /// <param name="unitsInStock"></param>
        public void SetUnitsInStock(short unitsInStock)
        {
            if(unitsInStock >= 10 && unitsInStock <= 100)
            {
                UnitsInStock = unitsInStock;
            }
            else
            {
                throw new Exception("Stock değeri min 10 max 100 arasında bir değer seçilebilir");
            }
        }
        

    }
}
