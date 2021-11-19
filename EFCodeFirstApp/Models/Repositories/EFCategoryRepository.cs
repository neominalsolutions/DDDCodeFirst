using EFCodeFirstApp.Models.Aggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.Repositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly ECommerceDbContext _db;

        public EFCategoryRepository(ECommerceDbContext db)
        {
            _db = db;
        }

        public void Add(Category model)
        {
            _db.Categories.Add(model);
            _db.SaveChanges(); // kayı işlemini uygula
        }

        public void Delete(string Id)
        {
            // Find Methodu Idsine göre kaydı bulur.
            var category = _db.Categories.Find(Id);
            // kategori bulunamadıysa
            if(category == null)
            {
                throw new Exception("Silincek kategori sistemde mevcut değildir");
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
        }

        public Category Find(string Id)
        {
            // Include ile Category nesnesi ile birlikte kategoriye ait Ürünlerin listesinde aldık. Çünkü o kategoriye ait ürünleri bilmek zorundayız. Aggregate yapısı kullanıldığında root yani category ile birlikte root altındaki nesnelerin program tarafına yönetim için çekilmesi gerekir.
            return _db.Categories.Include(x => x.Products).FirstOrDefault(x => x.Id == Id);
        }

        public List<Category> List()
        {
            return _db.Categories.Include(x => x.Products).ToList();
        }

        /// <summary>
        /// Sistem üzerinde tek başına save alınabilsin.
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }


        public bool SaveResult()
        {
            // saveChanges() kayıt sayısı döner
            return _db.SaveChanges() > 0 ? true : false;
        }

        public void Update(Category model)
        {
            _db.Categories.Update(model);
            _db.SaveChanges();
        }
    }
}
