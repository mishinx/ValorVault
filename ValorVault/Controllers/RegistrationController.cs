using Microsoft.AspNetCore.Mvc;
using SoldierInfoContext;
using ValorVault.Services;
using ValorVault.Models;

namespace ValorVault.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly RegistrationService _registrationService;

        public RegistrationController(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (!InputValidator.IsEmailValid(user.email))
                return BadRequest(new { message = "Некоректний формат електронної пошти" });

            if (!InputValidator.IsPasswordValid(user.user_password))
                return BadRequest(new { message = "Некоректний формат паролю" });

            if (!InputValidator.IsNameValid(user.username))
                return BadRequest(new { message = "Некоректний формат імені" });

            var registeredUser = _registrationService.Register(user.email, user.user_password, user.username);

            if (registeredUser == null)
                return BadRequest(new { message = "Користувач з таким email вже існує" });

            return Ok(registeredUser);
        }
    }
}