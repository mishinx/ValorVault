using Microsoft.AspNetCore.Identity;
using ValorVault.UserDtos;
using ValorVault.Models;
using System.Security.Claims;
using Serilog;

namespace ValorVault.Services.UserService
{
    public interface IUserService
    {
        Task<User> CreateUser(RegisterUserDto user);
        Task<bool> SignInUser(LoginUserDto user);
        Task LogOut();
        Task DeleteUser(Guid userId);
        Task<UserDto> GetUser(Guid id);
        Task<User> UpdateUser(Guid id, User user);
    }

    public class UserService : IUserService
    {
        //private readonly Serilog.ILogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)//, Serilog.ILogger logger)
        {
            //_logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> CreateUser(RegisterUserDto user)
        {
            if (user.Password != user.PasswordRepeat)
            {
                //_logger.Error($"'Password' and 'Repeat Password' fields must be the same!");
                throw new Exception("Паролі не співпадають!");
            }

            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                //_logger.Error($"User with Email {user.Email} already exists!");
                throw new InvalidOperationException($"Користувач з такою поштою вже існує!");
            }
            var existingUserByUsername = await _userManager.FindByNameAsync(user.Username);
            if (existingUserByUsername != null)
            {
                //_logger.Error($"User with Username {user.Username} already exists!");
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
                //_logger.Error($"Error occurred while creating user: {errorMessage}");
                throw new Exception($"При створенні користувача виникла помилка: {errorMessage}");
            }

            return newUser;
        }

        public async Task<bool> SignInUser(LoginUserDto user)
        {
            var foundUser = await _userManager.FindByEmailAsync(user.Email);

            if (foundUser == null)
            {
                //_logger.Error($"User not found!");
                return false;
            }
            foundUser.Id = foundUser.UserId;


            var result_of_upd = await _userManager.UpdateAsync(foundUser);
            if (!result_of_upd.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user.");
            }
            var result = await _signInManager.PasswordSignInAsync(foundUser.UserName, user.Password, true, false);

            if (!result.Succeeded)
            {
                //_logger.Error($"User not found!");
                return false;
            }
            await _signInManager.SignInAsync(foundUser, true);
            return true;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                //_logger.Error($"User not found!");
                throw new InvalidOperationException("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                //_logger.Error($"Failed to delete user.");
                throw new InvalidOperationException("Failed to delete user.");
            }
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                if (user == null)
                {
                    //_logger.Error($"User with ID {id} not found.");
                    throw new Exception("Користувача не знайдено");
                }

                return new UserDto(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> UpdateUser(Guid id, User updatedUser)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            existingUser.username = updatedUser.username;
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