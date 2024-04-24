using Microsoft.EntityFrameworkCore;
using SoldierInfoContext;
using ValorVault.Models;

namespace ValorVault.Services
{
    public interface IDataVerificationService
    {
        void MarkSoldierInfoAsVerified(int soldierInfoId);
        void MarkSoldierInfoAsRejected(int soldierInfoId);
    }

    public class DataVerificationService : IDataVerificationService
    {
        private readonly SoldierInfoDbContext _context;

        public DataVerificationService(SoldierInfoDbContext context)
        {
            _context = context;
        }

        public void MarkSoldierInfoAsVerified(int soldierInfoId)
        {
            var soldierInfo = _context.soldier_infos.Find(soldierInfoId);

            if (soldierInfo != null)
            {
                // Здійсніть необхідні дії для позначення солдата як верифікованого

                _context.SaveChanges(); // Викликайте SaveChanges() після збереження змін
            }
        }

        public void MarkSoldierInfoAsRejected(int soldierInfoId)
        {
            var soldierInfo = _context.soldier_infos.Find(soldierInfoId);

            if (soldierInfo != null)
            {
                _context.soldier_infos.Remove(soldierInfo);
                _context.SaveChanges();
            }
        }
    }
}
