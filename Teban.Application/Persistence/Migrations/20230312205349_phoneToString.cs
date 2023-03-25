using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TebanRMS.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class phoneToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "810aa48d-494e-4621-90cc-88c54e95214d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d68b7754-b7df-45fd-8792-6de992332f9e");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d97fb29-be27-4810-8078-693d84c817df", null, "Admin", "ADMIN" },
                    { "d6bb7b65-1a3f-4c78-bfb7-9c7c11b71e8c", null, "General", "GENERAL" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d97fb29-be27-4810-8078-693d84c817df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6bb7b65-1a3f-4c78-bfb7-9c7c11b71e8c");

            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Contacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "810aa48d-494e-4621-90cc-88c54e95214d", null, "General", "GENERAL" },
                    { "d68b7754-b7df-45fd-8792-6de992332f9e", null, "Admin", "ADMIN" }
                });
        }
    }
}
