using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ValorVault.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    source_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.source_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "SoldierInfos",
                columns: table => new
                {
                    soldier_info_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    soldier_name = table.Column<string>(type: "text", nullable: false),
                    call_sign = table.Column<string>(type: "text", nullable: false),
                    photo = table.Column<byte[]>(type: "bytea", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    death_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    missing_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    birth_place = table.Column<string>(type: "text", nullable: false),
                    rank = table.Column<string>(type: "text", nullable: false),
                    missing_place = table.Column<string>(type: "text", nullable: false),
                    death_place = table.Column<string>(type: "text", nullable: false),
                    profile_status = table.Column<string>(type: "text", nullable: false),
                    soldier_status = table.Column<string>(type: "text", nullable: false),
                    other_info = table.Column<string>(type: "text", nullable: false),
                    user_ref = table.Column<int>(type: "integer", nullable: false),
                    admin_ref = table.Column<int>(type: "integer", nullable: false),
                    source_ref = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldierInfos", x => x.soldier_info_id);
                    table.ForeignKey(
                        name: "FK_SoldierInfos_Administrators_admin_ref",
                        column: x => x.admin_ref,
                        principalTable: "Administrators",
                        principalColumn: "admin_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoldierInfos_Sources_source_ref",
                        column: x => x.source_ref,
                        principalTable: "Sources",
                        principalColumn: "source_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SoldierInfos_Users_user_ref",
                        column: x => x.user_ref,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoldierInfos_admin_ref",
                table: "SoldierInfos",
                column: "admin_ref");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierInfos_source_ref",
                table: "SoldierInfos",
                column: "source_ref");

            migrationBuilder.CreateIndex(
                name: "IX_SoldierInfos_user_ref",
                table: "SoldierInfos",
                column: "user_ref");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldierInfos");

            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
