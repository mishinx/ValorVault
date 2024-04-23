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
                name: "administrators",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    user_password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrators", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "sources",
                columns: table => new
                {
                    source_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "text", nullable: false),
                    source_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sources", x => x.source_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    user_password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "soldier_infos",
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
                    table.PrimaryKey("PK_soldier_infos", x => x.soldier_info_id);
                    table.ForeignKey(
                        name: "FK_soldier_infos_administrators_admin_ref",
                        column: x => x.admin_ref,
                        principalTable: "administrators",
                        principalColumn: "admin_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_soldier_infos_sources_source_ref",
                        column: x => x.source_ref,
                        principalTable: "sources",
                        principalColumn: "source_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_soldier_infos_users_user_ref",
                        column: x => x.user_ref,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_soldier_infos_admin_ref",
                table: "soldier_infos",
                column: "admin_ref");

            migrationBuilder.CreateIndex(
                name: "IX_soldier_infos_source_ref",
                table: "soldier_infos",
                column: "source_ref");

            migrationBuilder.CreateIndex(
                name: "IX_soldier_infos_user_ref",
                table: "soldier_infos",
                column: "user_ref");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "soldier_infos");

            migrationBuilder.DropTable(
                name: "administrators");

            migrationBuilder.DropTable(
                name: "sources");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
