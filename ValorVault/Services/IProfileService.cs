using System.Collections.Generic;
using System.Threading.Tasks;
using ValorVault.Models;
using ValorVault.UserDtos;

public interface IProfileService
{
    Task<User> GetUser(int id);
    Task<List<User>> GetAllUsers();
    Task<List<SoldierInfo>> GetAllProfiles();
    Task<User> GetRandomUser();
    Task<SoldierInfo> GetProfile(int id);
    Task<SoldierInfo> GetRandomProfile();
    Task<User> UpdateUser(Guid id, RegisterUserDto updatedUserDto)
    Task DeleteUser(User user);
}
