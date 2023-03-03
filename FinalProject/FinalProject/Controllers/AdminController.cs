using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinalProject.Controllers
{
    public class AdminController : Controller
    {
        FinalProjectDBContext context = new FinalProjectDBContext();

        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Admin(Login login)
        {
            var control = context.Logins.FirstOrDefault(x => x.EPosta == "admin@gmail.com" && x.Sifre == "AdminAdmin.123");
            if (control != null)
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Kullanıcı Adı Veya Şifre !!!";
                return View();
            }

        }

        public IActionResult AdminPanel()
        {
            return View();
        }

        public IActionResult ListCategories()
        {
            List<Category> categories = context.Categories.ToList();
            return View(categories);
        }

        public IActionResult AddNewCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(CategoryViewModel model)
        {
            Category category = new Category()
            {
                CategryName = model.CategoryName
            };
            context.Categories.Add(category);
            context.SaveChanges();

            return RedirectToAction("ListCategories");
        }
        public IActionResult DeleteCategory(int id)
        {
            var query = context.Categories.FirstOrDefault(x => x.CategoryId == id);
            context.Remove(query);
            context.SaveChanges();

            return RedirectToAction("ListCategories");
        }

        public IActionResult ModifyCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(x => x.CategoryId == id);
            CategoryViewModel model = new CategoryViewModel
            {
                CategoryName = category.CategryName,
                id = category.CategoryId

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ModifyCategory(CategoryViewModel category)
        {
            var query = context.Categories.FirstOrDefault(x => x.CategoryId == category.id);
            if(query == null)
            {
                return View("Error");
            }
            query.CategryName = category.CategoryName;
            context.SaveChanges();

            return RedirectToAction("ListCategories");
        }

        public IActionResult ListProducts()
        {
            List<Product> products = context.Products.ToList();
            return View(products);
        }

        public IActionResult DeleteProduct(int id)
        {
            var query = context.Products.FirstOrDefault(x => x.ProductId == id);
            if (query == null)
            {
                return View("Error");
            }
            context.Remove(query);
            context.SaveChanges();
            return RedirectToAction("ListProducts");
        }

        public IActionResult EditProduct(int id)
        {
            var a = context.Products.FirstOrDefault(x => x.ProductId == id);
            ProductViewModel model = new ProductViewModel()
            {
                Name = a.ProductName,
                productUrl = a.ProductUrl,
                Id = a.ProductId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel model)
        {
            var a = context.Products.FirstOrDefault(a => a.ProductId == model.Id);
            a.ProductName = model.Name;
            a.ProductUrl = model.productUrl;
            context.SaveChanges();

            return RedirectToAction("ListProducts");
        }

        public IActionResult AddNewProduct()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in context.Categories.Select(a => new { a.CategoryId, a.CategryName }))
            {
                selectList.Add(new SelectListItem() { Text = item.CategryName, Value = item.CategoryId.ToString() });
            }

            ViewBag.Employees = selectList;

            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(ProductViewModel model)
        {
            Product product = new Product();
            product.ProductName = model.Name;
            product.ProductUrl = model.productUrl;
            product.CategoryId = model.CategoryId;

            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("ListProducts");
        }

        public IActionResult ListMembers()
        {
            List<Login> members = context.Logins.ToList();
            return View(members);
        }

        public IActionResult DeleteMember(int id)
        {
            var query = context.Logins.FirstOrDefault(a => a.Id == id);
            if(query == null)
            {
                return View("Error");
            }
            context.Remove(query);
            context.SaveChanges();
            return RedirectToAction("ListMembers");
        
        }

        public IActionResult EditMember(int id)
        {
            var query = context.Logins.FirstOrDefault(x => x.Id == id);
            LoginViewModel model = new LoginViewModel()
            {
                AdSoyad=query.AdSoyad,
                Eposta=query.EPosta,
                sifre=query.Sifre,
                id=query.Id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditMember(LoginViewModel model)
        {
            var lgn = context.Logins.FirstOrDefault(a => a.Id == model.id);
            lgn.AdSoyad=model.AdSoyad;
            lgn.EPosta = model.Eposta;
            lgn.Sifre = model.sifre;
            
            context.SaveChanges();

            return RedirectToAction("ListMembers");
        }

    }



}
