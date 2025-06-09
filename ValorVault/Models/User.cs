using Microsoft.AspNetCore.Identity;

namespace ValorVault.Models
{
    public class User : IdentityUser<int>
    {
        public int UserId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string user_password { get; set; }
    }
}