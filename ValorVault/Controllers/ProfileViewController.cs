using Microsoft.AspNetCore.Mvc;
using ValorVault.Models;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;

namespace ValorVault.Controllers
{
    public class ProfileViewController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;

        public ProfileViewController(IProfileService profileService, IUserService userService)
        {
            _profileService = profileService;
            _userService = userService;
        }

        public async Task<IActionResult> RandomUser()
        {
            var randomUser = await _profileService.GetRandomUser();
            return View(randomUser);
        }

        public async Task<IActionResult> ProfileView(int id)
        {
            var profile = await _profileService.GetProfile(id);

            return View(profile);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileSettings()
        {
            var user_id = await _userService.GetIdByEmail(AuthenticationController.user_email);
            var user = await _userService.GetUser(user_id);
            User user_for_action = new User
            {
                Email = user.Email,
                UserName = user.Name,
            };
            return View(user_for_action);
        }

        public async Task<IActionResult> RandomProfile()
        {
            var randomProfile = await _profileService.GetRandomProfile();
            return View(randomProfile);
        }

        public async Task<IActionResult> Adminpage2()
        {
            var users = await _profileService.GetAllUsers();
            return View(users);
        }

        [HttpPost("Update")]
        public IActionResult Update(User updated_user)
        {
            try
            {
                int user_id = _userService.GetIdByEmail(AuthenticationController.user_email).Result;
                User new_user  = _userService.UpdateUser(user_id, updated_user).Result;
                if (new_user != null)
                {
                    return View("Main_registered");
                }
                else
                {
                    return BadRequest("Failed to update data.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return Ok(new { message = "User deleted successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Failed to delete user" });
            }
        }
    }
}
