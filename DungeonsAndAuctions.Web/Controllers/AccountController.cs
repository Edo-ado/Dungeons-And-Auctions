using Microsoft.AspNetCore.Mvc;
using DNDA.Web.Models;

namespace DNDA.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //}aqui va la logica de la autenticaicon
            // Por ahora solo redirige al home
            return RedirectToAction("Index", "Home");
        }
    }
}
