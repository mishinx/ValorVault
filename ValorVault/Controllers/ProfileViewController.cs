using Microsoft.AspNetCore.Mvc;
using SoldierInfoContext;
using System.Threading.Tasks;
using ValorVault.Models;
using ValorVault.Services;



namespace ValorVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileViewController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileViewController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SoldierInfo>> GetProfile(int id)
        {
            var profile = await _profileService.GetProfile(id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }
    }
}