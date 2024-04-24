using Microsoft.AspNetCore.Mvc;
using ValorVault.Services;
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
            if (!InputValidator.IsEmailValid(model.Email))
                return BadRequest(new { message = "Некоректний формат електронної пошти" });

            if (!InputValidator.IsPasswordValid(model.Password))
                return BadRequest(new { message = "Некоректний формат паролю" });

            if (!InputValidator.IsNameValid(model.Username))
                return BadRequest(new { message = "Некоректний формат імені" });

            try
            {
                var registeredUser = await _userService.CreateUser(model);

                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Помилка на сервері" });
            }
        }
    }
}
