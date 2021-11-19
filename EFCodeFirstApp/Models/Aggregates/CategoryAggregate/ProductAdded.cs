using EFCodeFirstApp.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.Aggregates.CategoryAggregate
{
    public class ProductAdded : IDomainEvent<ProductAddedEventArgs>
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IEmailSender _emailSender;

        public ProductAdded(ICategoryRepository categoryRepository, IEmailSender emailSender)
        {
            _categoryRepository = categoryRepository;
            _emailSender = emailSender;
        }

        public void Handle(ProductAddedEventArgs args)
        {
            _categoryRepository.Save();
            _emailSender.SendEmail(args.FromEmailAddress, args.ToEmailAddress, args.Message, args.Subject);
        }
    }
}
