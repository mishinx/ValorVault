using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ValorVault.Models;
using ValorVault.Services;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] RegisterUserDto updatedUserDto)
        {
            try
            {
                var userToUpdate = await _profileService.GetUser(id);
                if (userToUpdate == null)
                {
                    return NotFound($"User with id {id} not found.");
                }

                // Оновлення властивостей користувача
                userToUpdate.Username = updatedUserDto.Username;
                userToUpdate.Email = updatedUserDto.Email;
                userToUpdate.Password = updatedUserDto.Password;

                var updatedUser = await _userService.UpdateUser(userToUpdate);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
