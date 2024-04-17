using Microsoft.EntityFrameworkCore;
using ValorVault.Models;

namespace SoldierInfoContext
{
    public class SoldierInfoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<ValorVault.Models.SoldierInfo> SoldierInfos { get; set; }
        public string DbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=btserver1.postgres.database.azure.com;Database=postgres;Username=BT_Admin;Password=Admpassword123!",
                npgsqlOptionsAction: options =>
                {
                    options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.user_id);
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.password).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasKey(e => e.source_id);
                entity.Property(e => e.url).IsRequired();
                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.admin_id);
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.password).IsRequired();
            });

            modelBuilder.Entity<SoldierInfo>(entity =>
            {
                entity.HasKey(e => e.soldier_info_id);
                entity.Property(e => e.soldier_name).IsRequired();
                entity.Property(e => e.call_sign).IsRequired(); // Позивний
                entity.Property(e => e.photo).IsRequired();
                entity.Property(e => e.age).IsRequired();
                entity.Property(e => e.birth_date).IsRequired();
                entity.Property(e => e.death_date); // Може бути null
                entity.Property(e => e.missing_date); // Може бути null
                entity.Property(e => e.birth_place).IsRequired();
                entity.Property(e => e.rank).IsRequired(); // Посада
                entity.Property(e => e.missing_place); // Може бути null
                entity.Property(e => e.death_place); // Може бути null
                entity.Property(e => e.profile_status).IsRequired(); // Статус профайла
                entity.Property(e => e.soldier_status).IsRequired(); // Статус військового
                entity.Property(e => e.other_info).IsRequired();
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.user_ref)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Administrator>()
                    .WithMany()
                    .HasForeignKey(e => e.admin_ref)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Source>()
                    .WithMany()
                    .HasForeignKey(e => e.source_ref)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public class UserBase
        {
            public string email { get; set; }
            public string password { get; set; }
        }


        public class User : UserBase
        {
            public int user_id { get; set; }
            public string Name { get; set; }
        }

        public partial class Administrator : UserBase
        {
            public int admin_id { get; set; }
        }

        public partial class Source
        {
            public int source_id { get; set; }
            public string url { get; set; }
            public string name { get; set; }
        }
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
}