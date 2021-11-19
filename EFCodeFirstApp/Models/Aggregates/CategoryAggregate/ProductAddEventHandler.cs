using EFCodeFirstApp.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.Aggregates.CategoryAggregate
{
    /// <summary>
    /// Product Sisteme eklenirken uygulanacak logici Domain eventi dinleyerek yapar.
    /// </summary>
    public class ProductAddEventHandler : IProductAddEventHandler 
    {
        
        private readonly IEmailSender _emailSender;

        public ProductAddEventHandler(IEmailSender emailSender)
        {
         
            _emailSender = emailSender;
        }

        public void Handle(IDomainEvent @event)
        {
            var productAddedEvent = @event as ProductAdded;

            string message = $"{productAddedEvent.ProductName} ürünü sisteme başarılı bir şekilde eklendi!";
            _emailSender.SendEmail(from: productAddedEvent.NotifyFrom, to: productAddedEvent.NotifyTo, message, nameof(ProductAdded));

        }

        
    }
}
