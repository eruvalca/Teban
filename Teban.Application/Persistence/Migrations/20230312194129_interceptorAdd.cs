using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TebanRMS.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class interceptorAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73ec1d39-e1ad-485e-b217-a40716957c4d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e36066ac-54a7-47a5-a542-dc9052b3b1b3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "810aa48d-494e-4621-90cc-88c54e95214d", null, "General", "GENERAL" },
                    { "d68b7754-b7df-45fd-8792-6de992332f9e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "810aa48d-494e-4621-90cc-88c54e95214d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d68b7754-b7df-45fd-8792-6de992332f9e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73ec1d39-e1ad-485e-b217-a40716957c4d", null, "General", "GENERAL" },
                    { "e36066ac-54a7-47a5-a542-dc9052b3b1b3", null, "Admin", "ADMIN" }
                });
        }
    }
}
