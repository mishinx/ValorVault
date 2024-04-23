namespace ValorVault.Models
{
    public partial class SoldierInfo
    {
        public int soldier_info_id { get; set; }
        public string soldier_name { get; set; }
        public string call_sign { get; set; } // Позивний
        public byte[] photo { get; set; }
        public int age { get; set; }
        public DateTime birth_date { get; set; }
        public DateTime? death_date { get; set; } // Може бути null
        public DateTime? missing_date { get; set; } // Може бути null
        public string birth_place { get; set; }
        public string rank { get; set; } // Посада
        public string missing_place { get; set; } // Може бути null
        public string death_place { get; set; } // Може бути null
        public string profile_status { get; set; } // Статус профайла (Апрувнутий, Не апрувнутий, В процесі)
        public string soldier_status { get; set; } // Статус військового (Живий, Загинув, Зник безвісти, В полоні)
        public string other_info { get; set; }
        public int user_ref { get; set; }
        public int admin_ref { get; set; }
        public int source_ref { get; set; }
    }
}
