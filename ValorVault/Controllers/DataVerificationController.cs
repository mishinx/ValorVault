using Microsoft.AspNetCore.Mvc;
using ValorVault.Services;

namespace ValorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataVerificationController : ControllerBase
    {
        private readonly IDataVerificationService _dataVerificationService;

        public DataVerificationController(IDataVerificationService dataVerificationService)
        {
            _dataVerificationService = dataVerificationService;
        }

        [HttpPost("MarkAsVerified/{soldierInfoId}")]
        public IActionResult MarkAsVerified(int soldierInfoId)
        {
            _dataVerificationService.MarkSoldierInfoAsVerified(soldierInfoId);
            return Ok("Анкета успішно підтверджена.");
        }

        [HttpPost("MarkAsRejected/{soldierInfoId}")]
        public IActionResult MarkAsRejected(int soldierInfoId)
        {
            _dataVerificationService.MarkSoldierInfoAsRejected(soldierInfoId);
            return Ok("Анкета успішно відхилена.");
        }
    }
}
