using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("315d30eb-ed04-4a1a-861e-fa6ac470f64d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9c849860-e5e5-478a-a397-4215b35741ba"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Policies = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Policies" },
                values: new object[,]
                {
                    { new Guid("2336b391-1752-4bea-b062-437e39944521"), "Житель", new[] { "AuthPolicy", "UsersPolicy", "RequestPolicy", "NewsPolicy" } },
                    { new Guid("d3b03688-bf78-4dac-b552-03357f5dda7f"), "Администратор", new[] { "AuthPolicy", "UsersPolicy", "AdminPolicy", "NewsPolicy", "RequestPolicy", "AssetsPolicy" } }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BlockReason", "Created", "Email", "Name", "Note", "Patronymic", "Phone", "RefreshToken", "RefreshTokenExpires", "RoleId", "Surname" },
                values: new object[,]
                {
                    { new Guid("0c5bf282-a89d-430e-a680-eff6580d365e"), null, new DateTime(2024, 7, 9, 16, 7, 43, 772, DateTimeKind.Utc).AddTicks(2079), "guest@example.com", "Житель", "Создано автоматически", null, "79887893311", null, null, new Guid("2336b391-1752-4bea-b062-437e39944521"), "Тестовый" },
                    { new Guid("8d36efd1-31fe-4936-bb98-1bc8810877c9"), null, new DateTime(2024, 7, 9, 16, 7, 43, 772, DateTimeKind.Utc).AddTicks(2063), "17moron@bk.ru", "Администратор", "Создано автоматически", null, "79887897788", null, null, new Guid("d3b03688-bf78-4dac-b552-03357f5dda7f"), "Тестовый" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0c5bf282-a89d-430e-a680-eff6580d365e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8d36efd1-31fe-4936-bb98-1bc8810877c9"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BlockReason", "Created", "Email", "Name", "Note", "Patronymic", "Phone", "RefreshToken", "RefreshTokenExpires", "Surname" },
                values: new object[,]
                {
                    { new Guid("315d30eb-ed04-4a1a-861e-fa6ac470f64d"), null, new DateTime(2024, 7, 6, 8, 26, 25, 542, DateTimeKind.Utc).AddTicks(4751), "guest@example.com", "Джон", "Создано автоматически", null, "79887893311", null, null, "Уик" },
                    { new Guid("9c849860-e5e5-478a-a397-4215b35741ba"), null, new DateTime(2024, 7, 6, 8, 26, 25, 542, DateTimeKind.Utc).AddTicks(4730), "17moron@bk.ru", "Джон", "Создано автоматически", null, "79887897788", null, null, "Уик" }
                });
        }
    }
}
