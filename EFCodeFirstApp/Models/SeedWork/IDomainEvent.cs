using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.SeedWork
{
    public interface IDomainEvent<T>
    {
        void Handle(T args);
    }
}
