using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ValorVault.Models;
using ValorVault.Services;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;

namespace ValorVault.Controllers
{
    public class ProfileViewController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly IUserService _userService;

        public ProfileViewController(IProfileService profileService, IUserService userService)
        public ProfileViewController(IProfileService profileService, IUserService userService)
        {
            _profileService = profileService;
            _userService = userService;
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

        public async Task<IActionResult> ProfileSettings(int id)
        {
            var user = await _profileService.GetUser(id);

            return View(user);
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

        [HttpPut("{id}/UpdateEmail")]
        public async Task<IActionResult> UpdateEmail(int id, [FromBody] string newEmail)
        {
            try
            {
                bool success = await _profileService.UpdateEmailAsync(id, newEmail);
                if (success)
                {
                    return Ok("Email updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update email.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}/UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] string newPassword)
        {
            try
            {
                bool success = await _profileService.UpdatePasswordAsync(id, newPassword);
                if (success)
                {
                    return Ok("Password updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update password.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}/UpdateUsername")]
        public async Task<IActionResult> UpdateUsername(int id, [FromBody] string newUsername)
        {
            try
            {
                bool success = await _profileService.UpdateUsernameAsync(id, newUsername);
                if (success)
                {
                    return Ok("Username updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update username.");
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
                await _profileService.DeleteUser(userId);
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
