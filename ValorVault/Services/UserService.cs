﻿using Microsoft.AspNetCore.Identity;
using ValorVault.UserDtos;
using ValorVault.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ValorVault.Services.UserService
{
    public interface IUserService
    {
        Task<User> CreateUser(RegisterUserDto user);
        Task<bool> SignInUser(LoginUserDto user);
        Task LogOut();
        Task DeleteUser(Guid userId);
        Task<UserDto> GetUser(Guid id);
        Task<User> UpdateUser(Guid id, UserDto user);
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
                throw new ArgumentException("Passwords do not match.");
            }

            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }
            var existingUserByUsername = await _userManager.FindByNameAsync(user.Username);
            if (existingUserByUsername != null)
            {
                throw new InvalidOperationException("User with this username already exists.");
            }

            var newUser = new User
            {
                UserName = user.Username,
                Email = user.Email,
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to create user.");
            }

            return newUser;
        }

        public async Task<bool> SignInUser(LoginUserDto user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
            return result.Succeeded;
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
                throw new InvalidOperationException("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to delete user.");
            }
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return new UserDto(user);
        }

        public async Task<User> UpdateUser(Guid id, UserDto user)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Update user properties based on the DTO
            existingUser.Email = user.Email;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user.");
            }

            return existingUser;
        }
    }
}
