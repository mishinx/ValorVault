using SoldierInfoContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValorVault.Models;

namespace ValorVault.Services
{
    public interface IProfileService
    {
        Task<User> GetUser(int id);
        Task<List<User>> GetAllUsers();
        Task<List<SoldierInfo>> GetAllProfiles();
        Task<User> GetRandomUser();
        Task<SoldierInfo> GetProfile(int id);
        Task<SoldierInfo> GetRandomProfile();
        Task UpdateUserName(User user, string newUsername);
        Task UpdateUserEmail(User user, string newEmail);
        Task UpdateUserPassword(User user, string newPassword);
    }
}