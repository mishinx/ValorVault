using System.ComponentModel.DataAnnotations;

namespace ValorVault.Models
{
    public partial class SoldierInfo
    {
        public SoldierInfo()
        {
            profile_status = "Unverified";
        }
        [Key]
        public int soldier_info_id { get; set; }
        public string soldier_name { get; set; }
        public string call_sign { get; set; }
        public byte[] photo { get; set; }
        public int age { get; set; }
        public DateTime birth_date { get; set; }
        public DateTime? death_date { get; set; }
        public DateTime? missing_date { get; set; }
        public string birth_place { get; set; }
        public string rank { get; set; }
        public string missing_place { get; set; }
        public string death_place { get; set; }
        public string profile_status { get; private set; }
        public string soldier_status { get; set; }
        public string other_info { get; set; }
        public int user_ref { get; set; }
        public int admin_ref { get; set; }
        public int source_ref { get; set; }
    }
}
