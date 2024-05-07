using SoldierInfoContext;
using ValorVault.Models;


namespace ValorVault.Services
{
    public class ProfileService : IProfileService
    {
        private readonly SoldierInfoDbContext _context;

        public ProfileService(SoldierInfoDbContext context)
        {
            _context = context;
        }

        public async Task<SoldierInfo> GetProfile(int id)
        {
            return await _context.soldier_infos.FindAsync(id);
        }
    }
}