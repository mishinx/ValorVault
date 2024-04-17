using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;

namespace ValorVault.Services
{
    public class AuthenticationService
    {
        private readonly SoldierInfoDbContext _context;

        public AuthenticationService(SoldierInfoDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string email, string password)
        {
            var user = _context.users.SingleOrDefault(x => x.email == email && x.user_password == password);

            if (user == null)
                return null;

            return user;
        }
    }
}
