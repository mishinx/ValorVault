using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;
namespace ValorVault.Services
{
    public class RegistrationService
    {
        private readonly SoldierInfoDbContext _context;

        public RegistrationService(SoldierInfoDbContext context)
        {
            _context = context;
        }

        public User Register(string email, string password, string name)
        {
            if (_context.Users.Any(x => x.email == email))
                return null;

            var user = new User { email = email, password = password, Name = name };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}