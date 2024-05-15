    using ValorVault.Models;

namespace ValorVault.UserDtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        
        public UserDto(User user)
        {
            Email = user.email;
            Name = user.username;
        }
    }
}