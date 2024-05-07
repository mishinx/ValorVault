using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Linq;
using System.Threading.Tasks;
using ValorVault.Models;
using ValorVault.Services;

namespace ValorVault.Controllers
{
    public class ProfileViewController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileViewController(IProfileService profileService)
        {
            _profileService = profileService;
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
        [HttpPost]
        public async Task<IActionResult> UpdateUsername(int id, string username)
        {
            var user = await _profileService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = username;
            await _profileService.UpdateUserName(user, username);

            return Ok(new { UserId = user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmail(int id, string email)
        {
            var user = await _profileService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = email;
            await _profileService.UpdateUserEmail(user, email);

            return Ok(new { UserId = user.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(int id, string user_password)
        {
            var user = await _profileService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            var passwordHasher = new PasswordHasher<User>();
            var newPasswordHash = passwordHasher.HashPassword(user, user_password);
            user.PasswordHash = newPasswordHash;

            await _profileService.UpdateUserPassword(user, user_password);

            return Ok(new { UserId = user.Id });
        }



    }
}
