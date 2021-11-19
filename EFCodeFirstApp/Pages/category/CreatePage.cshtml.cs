using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataAnnotationsExtensions;
using EFCodeFirstApp.Models.Aggregates.CategoryAggregate;
using EFCodeFirstApp.Models.SeedWork;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EFCodeFirstApp.Pages.category
{
    // bu sýnýf domain sýnýfý gibi behaviour (davranýþ) özellik göstermez sadece içerisinde formdan gelen verileri tutar bu sebep ile genelde içerisinde herhangi bir method kullanýmý yapýlmaz.
    public class CategoryCreateInputModel
    {
        [Required(ErrorMessage = "Kategori adýný boþ geçmeyiniz")]
        public string Name { get; set; }

        public List<ProductCreateInputModel> Products { get; set; }
        public CategoryCreateInputModel()
        {
            Products = new List<ProductCreateInputModel>();
            //Products.Add(default(ProductCreateInputModel));
            //Products.Add(default(ProductCreateInputModel));

        }
    }

    public class ProductCreateInputModel
    {
        // bu sýnýf içerisinde ekran görünecek olan default deðerleri initial ilk deðerleri de verebiliriz.
        // Bu sýnýflar içerisinde formdan yanlýþ veri gelmemesi için validasyon deðer doðrulma kurallarý olabilir.
        [Required(ErrorMessage = "Name alaný boþ geçilemez")]
        [StringLength(20, ErrorMessage = "Ürün ismi en fazla 20 karakter girilebilir")]
        public string Name { get; set; }

        [Min(0, ErrorMessage = "Min 0 deðeri girebilirsiniz")]
        [Required(ErrorMessage = "Fiyat boþ geçilemez")]
        public decimal Price { get; set; } = 0;

        [Max(300, ErrorMessage = "En fazla 300 adet stok giriþi yapabilirsiniz")]
        public short Stock { get; set; } = 10;

        public bool IsPromotion { get; set; } = false;
    }

    public class CreatePageModel : PageModel
    {
        [BindProperty]
        public CategoryCreateInputModel InputModel { get; set; }
        private IProductAddDomainService _productAddService;
        private ICategoryRepository _categoryRepository;
       


        public CreatePageModel(IProductAddDomainService productAddDomainService, ICategoryRepository categoryRepository)
        {
            InputModel = new CategoryCreateInputModel();
            _productAddService = productAddDomainService;
            _categoryRepository = categoryRepository;
        }

        public void OnGet()
        {
            ViewData["ProductCount"] = 0;
        }

        public void OnPostSave()
        {
            // custom model error oluþturmak için kullandýk
            // ModelState.AddModelError("error","Hata");
            // ModelState form bilgilerinin valid doðrulandýðýný kontrol eder.



            if (ModelState.IsValid)
            {

                try
                {
                    var c = new Category(name: InputModel.Name);
                    _categoryRepository.Add(c);

                    foreach (var item in InputModel.Products)
                    {

                        c.AddProduct(item.Name, item.Stock, item.Price, item.IsPromotion, _productAddService);
                    }

                    // event olduðu için category aggregate içerisindeki AddProduct methodu ile category üzerinden ürünü sisteme eklemiþ olduk
                    //_categoryRepository.Save();

                    var result = _categoryRepository.Find(c.Id);


                    if (result != null)
                    {
                        ViewData["Message"] = "Kayýt Baþarýlýdýr";
                    }
                    else
                    {
                        ViewData["Message"] = "Tekrar deneyiniz";
                    }


                    //bool isSucceded =  _categoryRepository.SaveResult();

                    // if (isSucceded)
                    // {
                    //     ViewData["Message"] = "Kayýt Baþarýlýdýr";
                    // }
                    // else
                    // {
                    //     ViewData["Message"] = "Tekrar deneyiniz";
                    // }



                }
                catch (Exception ex)
                {
                    // sýnýflar üzerinden gelen exceptionmlarý modelstate ekleyip göstermemiz gerekirkir. program break moduna düþmesin.
                    ModelState.AddModelError("Hata", ex.Message);
                }




                // Database git.
            }

            // repository git iþlemleri yap.
        }

        // OnPost ön takýsýný koymadan çalýþmýyor
        // OnPost dan sonrasý html yazýlýr.
        public void OnPostAddProduct()
        {
            InputModel.Products.Add(new ProductCreateInputModel());
            ViewData["ProductCount"] = InputModel.Products.Count;
        }

        public void OnPostRemoveProduct()
        {
            InputModel.Products.RemoveAt(InputModel.Products.Count - 1);
            ViewData["ProductCount"] = InputModel.Products.Count;
        }
    }
}
