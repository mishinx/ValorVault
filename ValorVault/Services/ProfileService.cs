using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ValorVault.UserDtos;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;
using ValorVault.Services.UserService;

namespace ValorVault.Services
{
    public class ProfileService : IProfileService
    {
        private readonly SoldierInfoDbContext _context;
        private readonly UserManager<User> _userManager;

        public ProfileService(SoldierInfoDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<User> GetUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var result = await _userManager.GetUserIdAsync(user);
            return user; // Додайте цей рядок, щоб повернути користувача
        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<List<SoldierInfo>> GetAllProfiles()
        {
            return await _context.soldier_infos.ToListAsync();
        }


        public async Task<bool> UpdateEmailAsync(int userId, string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null)
                    return false;

                user.Email = email;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Обробка помилки, якщо щось пішло не так
                return false;
            }
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    return false;

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                return result.Succeeded;
            }
            catch (Exception)
            {
                // Обробка помилки, якщо щось пішло не так
                return false;
            }
        }

        public async Task<bool> UpdateUsernameAsync(int userId, string username)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(User => User.UserId == userId);
                if (user == null)
                    return false;

                user.username = username;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Обробка помилки, якщо щось пішло не так
                return false;
            }
        }

        public async Task DeleteUser(int UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
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
    }
}
