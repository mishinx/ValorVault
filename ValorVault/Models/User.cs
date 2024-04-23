using SoldierInfoContext;

namespace ValorVault.Models
{
    public class User : UserBase
    {
        public int user_id { get; set; }
        public string username { get; set; }
    }
}