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
    // bu s�n�f domain s�n�f� gibi behaviour (davran��) �zellik g�stermez sadece i�erisinde formdan gelen verileri tutar bu sebep ile genelde i�erisinde herhangi bir method kullan�m� yap�lmaz.
    public class CategoryCreateInputModel
    {
        [Required(ErrorMessage = "Kategori ad�n� bo� ge�meyiniz")]
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
        // bu s�n�f i�erisinde ekran g�r�necek olan default de�erleri initial ilk de�erleri de verebiliriz.
        // Bu s�n�flar i�erisinde formdan yanl�� veri gelmemesi i�in validasyon de�er do�rulma kurallar� olabilir.
        [Required(ErrorMessage = "Name alan� bo� ge�ilemez")]
        [StringLength(20, ErrorMessage = "�r�n ismi en fazla 20 karakter girilebilir")]
        public string Name { get; set; }

        [Min(0, ErrorMessage = "Min 0 de�eri girebilirsiniz")]
        [Required(ErrorMessage = "Fiyat bo� ge�ilemez")]
        public decimal Price { get; set; } = 0;

        [Max(300, ErrorMessage = "En fazla 300 adet stok giri�i yapabilirsiniz")]
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
            // custom model error olu�turmak i�in kulland�k
            // ModelState.AddModelError("error","Hata");
            // ModelState form bilgilerinin valid do�ruland���n� kontrol eder.



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

                    // event oldu�u i�in category aggregate i�erisindeki AddProduct methodu ile category �zerinden �r�n� sisteme eklemi� olduk
                    //_categoryRepository.Save();

                    var result = _categoryRepository.Find(c.Id);


                    if (result != null)
                    {
                        ViewData["Message"] = "Kay�t Ba�ar�l�d�r";
                    }
                    else
                    {
                        ViewData["Message"] = "Tekrar deneyiniz";
                    }


                    //bool isSucceded =  _categoryRepository.SaveResult();

                    // if (isSucceded)
                    // {
                    //     ViewData["Message"] = "Kay�t Ba�ar�l�d�r";
                    // }
                    // else
                    // {
                    //     ViewData["Message"] = "Tekrar deneyiniz";
                    // }



                }
                catch (Exception ex)
                {
                    // s�n�flar �zerinden gelen exceptionmlar� modelstate ekleyip g�stermemiz gerekirkir. program break moduna d��mesin.
                    ModelState.AddModelError("Hata", ex.Message);
                }




                // Database git.
            }

            // repository git i�lemleri yap.
        }

        // OnPost �n tak�s�n� koymadan �al��m�yor
        // OnPost dan sonras� html yaz�l�r.
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
