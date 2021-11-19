using EFCodeFirstApp.Models.Aggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.SeedWork
{
    public class ProductAddDomainService : IProductAddDomainService
    {
        private readonly IProductAddEventHandler _productAddEventHandler;
        private readonly ICategoryRepository _categoryRepository;
        public ProductAddDomainService(IProductAddEventHandler productAddEventHandler, ICategoryRepository categoryRepository)
        {
            _productAddEventHandler = productAddEventHandler;
            _categoryRepository = categoryRepository;
        }

        public void onProcess(IDomainEvent @event)
        {
            
            var productEvent = @event as ProductAdded;

            // log servis ile ürün ilk giriş bilgisini tutabilirdik.
            // aynı zamanda user service ile ürün sisteme girecek olan kullanıcının email bilgisi okuyup, bu kullanıcının operasyon sorumlusuna mail atabilirdik.

            // promosyonsuz için mail atmamız gerekiyor.
            if(!productEvent.Promotion)
            {
                _productAddEventHandler.Handle(@event);
            }

            _categoryRepository.Save();


            // gelen event bilgisine göre ekstra işlem yaparız

        }
    }
}
