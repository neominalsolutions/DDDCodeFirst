using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.SeedWork
{
    /// <summary>
    /// sadece IAggregateRoot olan sınıflar üzerinden repository verilere ulaşılabilir
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T: IAggregateRoot
    {
        T Find(string Id);
        List<T> List();
        void Add(T model);
        void Delete(string Id);
        void Update(T model);
        void Save();

        bool SaveResult();
    }
}
