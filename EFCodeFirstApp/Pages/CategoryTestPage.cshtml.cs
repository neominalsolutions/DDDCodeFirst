using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCodeFirstApp.Models.Aggregates.CategoryAggregate;
using EFCodeFirstApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EFCodeFirstApp.Pages
{
    public class CategoryTestPageModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        //private readonly EFCategoryRepository eFCategoryRepository;

        public CategoryTestPageModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            //eFCategoryRepository = new EFCategoryRepository(new Models.ECommerceDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<Models.ECommerceDbContext>()));
        }

        public void OnGet()
        {
            var c = _categoryRepository.Find(Guid.NewGuid().ToString());
        }
    }
}
