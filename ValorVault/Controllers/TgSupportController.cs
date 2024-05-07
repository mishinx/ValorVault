using Microsoft.AspNetCore.Mvc;
using ValorVault.Services;

namespace ValorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : Controller
    {
        private readonly ITelegramService _telegramService;

        public TelegramController(ITelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        [HttpGet]
        public IActionResult ContactAdministrator()
        {
            var telegramUrl = _telegramService.GetTelegramAccountUrl();
            return Redirect(telegramUrl);
        }
    }
}
