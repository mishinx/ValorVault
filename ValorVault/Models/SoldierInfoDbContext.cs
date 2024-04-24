using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ValorVault.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Configuration;

namespace SoldierInfoContext
{
    public class SoldierInfoDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Source> sources { get; set; }
        public virtual DbSet<SoldierInfo> soldier_infos { get; set; }
        public string DbPath { get; }

        public SoldierInfoDbContext(DbContextOptions<SoldierInfoDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
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

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "User", NormalizedName = "USER" },
                new IdentityRole<int> { Id = 2, Name = "Administrator", NormalizedName = "ADMINISTRATOR" }
            );

            modelBuilder.Entity<SoldierInfo>(entity =>
            {
                entity.HasKey(e => e.soldier_info_id);
                entity.Property(e => e.soldier_name).IsRequired();
                entity.Property(e => e.call_sign).IsRequired();
                entity.Property(e => e.photo).IsRequired();
                entity.Property(e => e.age).IsRequired();
                entity.Property(e => e.birth_date).IsRequired();
                entity.Property(e => e.death_date);
                entity.Property(e => e.missing_date);
                entity.Property(e => e.birth_place);
                entity.Property(e => e.rank);
                entity.Property(e => e.missing_place);
                entity.Property(e => e.death_place);
                entity.Property(e => e.profile_status).IsRequired();
                entity.Property(e => e.soldier_status).IsRequired();
                entity.Property(e => e.other_info);
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.user_ref)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<IdentityRole<int>>()
                    .WithMany()
                    .HasForeignKey(e => e.admin_ref)
                    .HasPrincipalKey(role => role.Id)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Source>()
                    .WithMany()
                    .HasForeignKey(e => e.source_ref)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    } 
}