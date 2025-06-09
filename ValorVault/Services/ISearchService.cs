using SoldierInfoContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValorVault.Models;

namespace ValorVault.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SoldierInfo>> SearchByNameAsync(string keyword);
    }
}
