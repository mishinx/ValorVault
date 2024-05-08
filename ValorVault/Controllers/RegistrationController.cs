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
                return BadRequest(new { message = "Некоректний формат електронної пошти або заслабкий пароль" });
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
