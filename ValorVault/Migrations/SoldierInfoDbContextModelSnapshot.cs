﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SoldierInfoContext;

#nullable disable

namespace ValorVault.Migrations
{
    [DbContext(typeof(SoldierInfoDbContext))]
    partial class SoldierInfoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SoldierInfoContext.Administrator", b =>
                {
                    b.Property<int>("admin_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("admin_id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("user_password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("admin_id");

                    b.ToTable("administrators");
                });

            modelBuilder.Entity("SoldierInfoContext.SoldierInfo", b =>
                {
                    b.Property<int>("soldier_info_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("soldier_info_id"));

                    b.Property<int>("admin_ref")
                        .HasColumnType("integer");

                    b.Property<int>("age")
                        .HasColumnType("integer");

                    b.Property<DateTime>("birth_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("birth_place")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("call_sign")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("death_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("death_place")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("missing_date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("missing_place")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("other_info")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("photo")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("profile_status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("rank")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("soldier_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("soldier_status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("source_ref")
                        .HasColumnType("integer");

                    b.Property<int>("user_ref")
                        .HasColumnType("integer");

                    b.HasKey("soldier_info_id");

                    b.HasIndex("admin_ref");

                    b.HasIndex("source_ref");

                    b.HasIndex("user_ref");

                    b.ToTable("soldier_infos");
                });

            modelBuilder.Entity("SoldierInfoContext.Source", b =>
                {
                    b.Property<int>("source_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("source_id"));

                    b.Property<string>("source_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("source_id");

                    b.ToTable("sources");
                });

            modelBuilder.Entity("ValorVault.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("user_id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("user_password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("user_id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("SoldierInfoContext.SoldierInfo", b =>
                {
                    b.HasOne("SoldierInfoContext.Administrator", null)
                        .WithMany()
                        .HasForeignKey("admin_ref")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SoldierInfoContext.Source", null)
                        .WithMany()
                        .HasForeignKey("source_ref")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ValorVault.Models.User", null)
                        .WithMany()
                        .HasForeignKey("user_ref")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
