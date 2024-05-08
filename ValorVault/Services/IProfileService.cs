using System.Collections.Generic;
using System.Threading.Tasks;
using ValorVault.Models;
using ValorVault.UserDtos;
using ValorVault.UserDtos;

public interface IProfileService
{
    Task<User> GetUser(int userId);
    Task<List<User>> GetAllUsers();
    Task<List<SoldierInfo>> GetAllProfiles();
    Task<User> GetRandomUser();
    Task<SoldierInfo> GetProfile(int id);
    Task<SoldierInfo> GetRandomProfile();
    Task<bool> UpdateEmailAsync(int userId, string newEmail);
    Task<bool> UpdatePasswordAsync(int userId, string newPassword);
    Task<bool> UpdateUsernameAsync(int userId, string newUsername);
    Task DeleteUser(int userId);
}
