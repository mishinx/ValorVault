using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;

namespace ValorVault.Services
{
    public interface IUserModerationService
    {
        void DeleteUser(int userId);
    }

    public class UserModerationService : IUserModerationService
    {
        private readonly SoldierInfoDbContext _context;

        public UserModerationService(SoldierInfoDbContext context)
        {
            _context = context;
        }

        public void DeleteUser(int userId)
        {
            var user = _context.users.Find(userId);

            if (user != null)
            {
                _context.users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
