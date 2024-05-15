using Microsoft.AspNetCore.Mvc;
using ValorVault.Services;

namespace ValorVault.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserModerationService _userModerationService;

        public AdminController(IUserModerationService userModerationService)
        {
            _userModerationService = userModerationService;
        }

        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            _userModerationService.DeleteUser(userId);
            return Ok("Користувач успішно видалений.");
        }
    }
}
