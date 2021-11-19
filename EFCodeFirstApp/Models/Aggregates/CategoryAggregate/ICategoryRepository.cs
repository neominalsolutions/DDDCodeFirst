
using EFCodeFirstApp.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.Aggregates.CategoryAggregate
{
    /// <summary>
    /// Category ve Product bilgisi sağlar. amacı entitylerin ihtiyaç duyacağı verilere ulaşmamızı veya entityler üzerinde kayıt işlemleri yapmamızı sağlamak
    /// </summary>
    public interface ICategoryRepository: IRepository<Category>
    {

    }
}
