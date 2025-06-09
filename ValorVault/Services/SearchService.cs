using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValorVault.Models;
using Microsoft.Extensions.DependencyInjection;
using SoldierInfoContext;

namespace ValorVault.Services
{
    public class SearchService : ISearchService
    {
        private readonly SoldierInfoDbContext _context;

        public SearchService(SoldierInfoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SoldierInfo>> SearchByNameAsync(string keyword)
        {
            return await _context.soldier_infos
                .Where(s => s.soldier_name.Contains(keyword))
                .ToListAsync();
        }
    }
}
