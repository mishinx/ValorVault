using Microsoft.AspNetCore.Identity;
using ValorVault.UserDtos;
using ValorVault.Models;
using ValorVault.Controllers;
using NuGet.Common;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ValorVault.Services.UserService
{
    public interface IUserService
    {
        Task<User> CreateUser(RegisterUserDto user);
        Task<string> SignInUser(LoginUserDto user);
        Task LogOut();
        Task DeleteUser(int userId);
        Task<UserDto> GetUser(int id);
        Task<int> GetIdByEmail(string email);
        Task<User> UpdateUser(int id, User user);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
           _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> CreateUser(RegisterUserDto user)
        {
            if (user.Password != user.PasswordRepeat)
            {
                throw new Exception("Паролі не співпадають!");
            }

            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Користувач з такою поштою вже існує!");
            }
            var existingUserByUsername = await _userManager.FindByNameAsync(user.Username);
            if (existingUserByUsername != null)
            {
                throw new InvalidOperationException($"Користувач з таким іменем вже існує!");
            }

            var newUser = new User
            {
                username = user.Username,
                UserName = user.Username,
                Email = user.Email,
                email = user.Email,
                user_password = user.Password
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"При створенні користувача виникла помилка: {errorMessage}");
            }
            try
            {
                newUser.Id = newUser.UserId;
                await _userManager.AddToRoleAsync(newUser, "User");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return null;
            }
            AuthenticationController.user_email = newUser.Email;
            return newUser;
        }

        public async Task<string> SignInUser(LoginUserDto user)
        {
            var foundUser = await _userManager.FindByEmailAsync(user.Email);

            if (foundUser == null)
            {
                return null;
            }
            foundUser.Id = foundUser.UserId;


            var result_of_upd = await _userManager.UpdateAsync(foundUser);
            if (!result_of_upd.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user.");
            }
            var result = await _signInManager.PasswordSignInAsync(foundUser.UserName, user.Password, true, false);

            AuthenticationController.user_email = foundUser.Email;
            if (!result.Succeeded)
            {
                return null;
            }

            await _signInManager.SignInAsync(foundUser, true);
            var isAdmin = await _userManager.IsInRoleAsync(foundUser, "Administrator");
            if (isAdmin) 
            {
                return "Admin"; 
            }
            else 
            {
                return "User";
            }
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to delete user.");
            }

        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new Exception("Користувача не знайдено");
            }

            return new UserDto(user);
        }

        public async Task<int> GetIdByEmail(string email)
        {
            var foundUser = await _userManager.FindByEmailAsync(email);

            if (foundUser == null)
            {
                throw new Exception("Користувача не знайдено");
            }

            return foundUser.UserId;
        }

        public async Task<User> UpdateUser(int id, User updatedUser)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if(updatedUser.Email != null)
            {
                AuthenticationController.user_email = updatedUser.Email;
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(existingUser, existingUser.user_password, updatedUser.user_password);
            if (!changePasswordResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to change password.");
            }

            existingUser.username = updatedUser.username;
            existingUser.UserName = updatedUser.username;
            existingUser.email = updatedUser.Email;
            existingUser.Email = updatedUser.Email;
            existingUser.user_password = updatedUser.user_password;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user.");
            }

            return existingUser;
        }
    }
}