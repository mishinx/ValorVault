using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ValorVault.UserDtos;
using ValorVault.Services.UserService;

namespace ValorVault.Services
{
    public class ProfileService : IProfileService
    {
        private readonly SoldierInfoDbContext _context;
        private readonly IUserService _userService;

        public ProfileService(SoldierInfoDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<User> GetRandomUser()
        {
            var users = await _context.Users.ToListAsync();
            var randomIndex = new Random().Next(0, users.Count);
            return users[randomIndex];
        }

        public async Task<SoldierInfo> GetRandomProfile()
        {
            var profiles = await _context.soldier_infos.ToListAsync();
            if (profiles == null || !profiles.Any())
            {
                return null;
            }

            var randomIndex = new Random().Next(0, profiles.Count);
            return profiles[randomIndex];
        }

        public async Task<SoldierInfo> GetProfile(int id)
        {
            return await _context.soldier_infos.FindAsync(id);
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<SoldierInfo>> GetAllProfiles()
        {
            return await _context.soldier_infos.ToListAsync();
        }
        public async Task<User> UpdateUser(Guid id, RegisterUserDto updatedUserDto)
        {
            var existingUser = await _userService.FindByIdAsync(id.ToString());
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            existingUser.UserName = updatedUserDto.Username;
            existingUser.Email = updatedUserDto.Email;

            var result = await _userService.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user.");
            }

            if (!string.IsNullOrEmpty(updatedUserDto.Password))
            {
                var newPassword = updatedUserDto.Password;
                var token = await _userService.GeneratePasswordResetTokenAsync(existingUser);
                result = await _userService.ResetPasswordAsync(existingUser, token, newPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Failed to update password.");
                }
            }

            return existingUser;
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
