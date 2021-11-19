using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.Aggregates.CategoryAggregate
{
    // Arguman Product Added olduğunda Evente göndereceimiz bilgileri taşıyan Event nesnesi oluyor.
    public class ProductAddedEventArgs
    {
        private Product _product;

        public ProductAddedEventArgs(Product product, string from, string to)
        {
            _product = product;

            Message = $"Sisteme {_product.Name} adında bir ürün eklendi. Fiyatı {_product.UnitPrice}'dır";
            Subject = "Yeni ürün girişi!";
            FromEmailAddress = from;
            ToEmailAddress = to;
        }

        public string FromEmailAddress { get; private set; }
        public string ToEmailAddress { get; private set; }

        public string Subject { get; private set; } 

        public string Message { get; private set; }



    }
}
