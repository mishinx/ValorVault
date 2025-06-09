using Microsoft.AspNetCore.Identity;

namespace ValorVault.Models
{
    public partial class Administrator : IdentityUser<int>
    {
        public int AdminId { get; set; }
        public string email { get; set; }
        public string user_password { get; set; }
    }
}
