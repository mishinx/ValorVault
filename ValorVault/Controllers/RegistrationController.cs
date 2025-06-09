using Microsoft.AspNetCore.Mvc;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;

namespace ValorVault.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;

        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto model)
        {
            try
            {
                var registeredUser = await _userService.CreateUser(model);

                return RedirectToAction("Main_registered", "Home");
            }
            catch (ArgumentException ex)
            {
                TempData["RegisterErrorMessage"] = "Некоректний формат електронної пошти або заслабкий пароль";
                return RedirectToAction("Register", "Registration");
            }
            catch (InvalidOperationException ex)
            {
                TempData["RegisterErrorMessage"] = "Некоректний формат електронної пошти або заслабкий пароль";
                return RedirectToAction("Register", "Registration");
            }
            catch (Exception)
            {
                TempData["RegisterErrorMessage"] = "При реєстрації виникла помилка. Будь ласка, спробуйте ще раз.";
                return RedirectToAction("Register", "Registration");
            }
        }
    }
}
