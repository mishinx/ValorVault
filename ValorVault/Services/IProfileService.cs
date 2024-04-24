using SoldierInfoContext;
using System.Threading.Tasks;
using ValorVault.Models;


namespace ValorVault.Services
{
    public interface IProfileService
    {
        Task<SoldierInfo> GetProfile(int id);
    }
}
