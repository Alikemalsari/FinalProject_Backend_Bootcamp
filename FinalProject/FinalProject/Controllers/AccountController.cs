using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        FinalProjectDBContext context = new FinalProjectDBContext();
        public IActionResult Account()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Account(Login login)
        {
            var control = context.Logins.FirstOrDefault(x => x.EPosta == login.EPosta && x.Sifre == login.Sifre);
            if (control != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Geçersiz Kullanıcı Adı Veya Şifre !!!";
                return View();
            }

        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
                
            }
            Login lgn = new Login();

            lgn.AdSoyad = model.UsernameSurname;
            lgn.EPosta = model.Eposta;
            lgn.Sifre = model.Password;
            context.Logins.Add(lgn);
            context.SaveChanges();

            return RedirectToAction("Account");
        }
    }
}
