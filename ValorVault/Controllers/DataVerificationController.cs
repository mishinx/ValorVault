using Microsoft.AspNetCore.Mvc;
using ValorVault.Services;

namespace ValorVault.Controllers
{
    public class DataVerificationController : Controller
    {
        private readonly IDataVerificationService _dataVerificationService;

        public DataVerificationController(IDataVerificationService dataVerificationService)
        {
            _dataVerificationService = dataVerificationService;
        }

        [HttpPost]
        public IActionResult MarkAsVerified(int soldierInfoId)
        {
            _dataVerificationService.MarkSoldierInfoAsVerified(soldierInfoId);
            return Ok("Анкета успішно підтверджена.");
        }

        [HttpPost]
        public IActionResult MarkAsRejected(int soldierInfoId)
        {
            _dataVerificationService.MarkSoldierInfoAsRejected(soldierInfoId);
            return Ok("Анкета успішно відхилена.");
        }
    }
}
