using EFCodeFirstApp.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.Aggregates.CategoryAggregate
{
    /// <summary>
    /// Eventler bir eylem gerçekleşeceği zaman bu eyle ile ilişkili ne kadar veri varsa içerisinde saklar. Bu veriler üzerinden işlem yapılmak üzere eventhandler'a gönderir. yada üzerinde logic (iş kuralı varsa) domain service gönderir.
    /// </summary>
    public class ProductAdded: IDomainEvent
    {
        

        public ProductAdded(string notifyFrom, string notifyTo, string productName,decimal price, short stock, bool promotion)
        {
            NotifyFrom = notifyFrom;
            NotifyTo = notifyTo;
            ProductName = productName;
            Price = price;
            Stock = stock;
            Promotion = promotion;
        }

        public string NotifyFrom { get; private set; }
        public string NotifyTo { get; private set; }

        public string ProductName { get; private set; }
        public decimal Price { get; private set; }

        public short Stock { get; private set; }

        public bool Promotion { get; private set; }


    }
}
