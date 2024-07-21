using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Raw SQL to create database if it does not exist
            migrationBuilder.Sql(@"CREATE DATABASE IF NOT EXISTS eshopping;
            USE eshopping;");

            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "addresstypes",
                columns: table => new
                {
                    address_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    address_type_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    max_address_per_user = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresstypes", x => x.address_type_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    role_description = table.Column<string>(type: "varchar(1500)", maxLength: 1500, nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    user_email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password_salt = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    password_expiry_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    password_recovery_uid = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    recovery_expiry = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_by = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "useraddresses",
                columns: table => new
                {
                    User_address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    address_type_id = table.Column<int>(type: "int", nullable: false),
                    address_line1 = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    address_line2 = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    city = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    state = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    zip_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    country = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_useraddresses", x => x.User_address_id);
                    table.ForeignKey(
                        name: "FK_useraddresses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userrolejoin",
                columns: table => new
                {
                    user_role_join_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userrolejoin", x => x.user_role_join_id);
                    table.ForeignKey(
                        name: "FK_userrolejoin_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userrolejoin_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_useraddresses_user_id",
                table: "useraddresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_userrolejoin_role_id",
                table: "userrolejoin",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_userrolejoin_user_id",
                table: "userrolejoin",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresstypes");

            migrationBuilder.DropTable(
                name: "useraddresses");

            migrationBuilder.DropTable(
                name: "userrolejoin");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
