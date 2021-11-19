using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.SeedWork
{
    // Domain service ise bir domain eventi işler.
    // burada bu service ile alakalı tüm alt bileşenleri ve iş kurallarını yazarız.

    /// <summary>
    /// Eventleri işlemek için bu interface kullanılır
    /// Eventlerden gelen bilgilere göre nasıl bir işlem yapılacağını belirler
    /// </summary>
    public interface IDomainEventHandler 
    {
        void Handle(IDomainEvent @event);
    }
}
