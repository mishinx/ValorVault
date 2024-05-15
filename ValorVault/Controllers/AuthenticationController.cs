using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ValorVault.Models;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;

namespace ValorVault.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        public static string user_email = "";
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(LoginUserDto userDto)
        {
            try
            {
                var role = _userService.SignInUser(userDto).Result;
                user_email = userDto.Email;
                if (role == null)
                {
                    TempData["LoginErrorMessage"] = "Неправильний email або пароль.";
                    ViewBag.ErrorMessage = "Email або пароль невірні";
                    return View("Login");
                }
                else if (role == "Admin")
                {
                    TempData["SuccessMessage"] = "Дія виконана успішно!";
                    return RedirectToAction("Adminpage2", "ProfileView");
                }
                else
                {
                    TempData["SuccessMessage"] = "Дія виконана успішно!";
                    return RedirectToAction("Main_registered", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Login");
            }
        }
    }
}