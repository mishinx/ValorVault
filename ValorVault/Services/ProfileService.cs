using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ValorVault.Services
{
    public class ProfileService : IProfileService
    {
        private readonly SoldierInfoDbContext _context;
        private readonly Random _random;

        public ProfileService(SoldierInfoDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task<User> GetRandomUser()
        {
            var users = await _context.Users.ToListAsync();
            var randomIndex = _random.Next(0, users.Count);
            return users[randomIndex];
        }
        public async Task<SoldierInfo> GetRandomProfile()
        {
            var profiles = await _context.soldier_infos.ToListAsync();
            if (profiles == null || !profiles.Any())
            {
                return null;
            }
            var randomIndex = _random.Next(0, profiles.Count);     
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

        public async Task UpdateUserName(User user, string newUsername)
        {
            user.UserName = newUsername;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserEmail(User user, string newEmail)
        {
            user.Email = newEmail;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPassword(User user, string newPassword)
        {
            var passwordHasher = new PasswordHasher<User>();
            var newPasswordHash = passwordHasher.HashPassword(user, newPassword);
            user.PasswordHash = newPasswordHash;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
