using Microsoft.EntityFrameworkCore;
using ValorVault.Models;

namespace SoldierInfoContext
{
    public class SoldierInfoDbContext : DbContext
    {
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Source> sources { get; set; }
        public virtual DbSet<Administrator> administrators { get; set; }
        public virtual DbSet<SoldierInfo> soldier_infos { get; set; }
        public string DbPath { get; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Ensure call to base to preserve any other configuration setup outside
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.user_id);
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.user_password).IsRequired();
                entity.Property(e => e.username).IsRequired();
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasKey(e => e.source_id);
                entity.Property(e => e.url).IsRequired();
                entity.Property(e => e.source_name).IsRequired();
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.admin_id);
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.user_password).IsRequired();
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
    }
}