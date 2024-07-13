using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserAndAuthTicketInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4864e995-22ea-4ff6-b9cc-eb025b35385d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fcddf8bf-007a-443f-b214-b671f631a2a1"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "BlockReason",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "AuthTickets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BlockReason", "Created", "Email", "Name", "Note", "Patronymic", "Phone", "RefreshToken", "RefreshTokenExpires", "Surname" },
                values: new object[,]
                {
                    { new Guid("315d30eb-ed04-4a1a-861e-fa6ac470f64d"), null, new DateTime(2024, 7, 6, 8, 26, 25, 542, DateTimeKind.Utc).AddTicks(4751), "guest@example.com", "Джон", "Создано автоматически", null, "79887893311", null, null, "Уик" },
                    { new Guid("9c849860-e5e5-478a-a397-4215b35741ba"), null, new DateTime(2024, 7, 6, 8, 26, 25, 542, DateTimeKind.Utc).AddTicks(4730), "17moron@bk.ru", "Джон", "Создано автоматически", null, "79887897788", null, null, "Уик" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("315d30eb-ed04-4a1a-861e-fa6ac470f64d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9c849860-e5e5-478a-a397-4215b35741ba"));

            migrationBuilder.DropColumn(
                name: "BlockReason",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "AuthTickets");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "Email", "Name", "Note", "Patronymic", "Phone", "RefreshToken", "RefreshTokenExpires", "Role", "Surname" },
                values: new object[,]
                {
                    { new Guid("4864e995-22ea-4ff6-b9cc-eb025b35385d"), new DateTime(2024, 2, 4, 15, 30, 18, 404, DateTimeKind.Utc).AddTicks(970), "guest@example.com", "Джон", "Создано автоматически", null, "79887893311", null, null, 0, "Уик" },
                    { new Guid("fcddf8bf-007a-443f-b214-b671f631a2a1"), new DateTime(2024, 2, 4, 15, 30, 18, 404, DateTimeKind.Utc).AddTicks(950), "17moron@bk.ru", "Джон", "Создано автоматически", null, "79887897788", null, null, 0, "Уик" }
                });
        }
    }
}
