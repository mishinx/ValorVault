using Microsoft.AspNetCore.Mvc;
using SoldierInfoContext;
using ValorVault.Models;
using ValorVault.Services;

namespace ValorVault.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(UserBase userBase)
        {
            var user = _authenticationService.Authenticate(userBase.email, userBase.user_password);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Email або пароль невірні";
                return View("Login");
            }

            // Вхід успішний, редірект або інша логіка
            return RedirectToAction("Index", "Home");
        }
    }
}
