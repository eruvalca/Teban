using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Teban.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class frequencyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b96931c-20d8-44c7-ac48-249bda435180");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0dcec0b-06c3-4b03-a7b4-a0d10a12c567");

            migrationBuilder.AlterColumn<string>(
                name: "Frequency",
                table: "CommunicationSchedules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d1aa516-7343-4ec7-8e95-73dd447c3117", null, "General", "GENERAL" },
                    { "56f413d8-8a3f-45e8-8f1f-5c24f5cd65a2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d1aa516-7343-4ec7-8e95-73dd447c3117");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56f413d8-8a3f-45e8-8f1f-5c24f5cd65a2");

            migrationBuilder.AlterColumn<int>(
                name: "Frequency",
                table: "CommunicationSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2b96931c-20d8-44c7-ac48-249bda435180", null, "Admin", "ADMIN" },
                    { "a0dcec0b-06c3-4b03-a7b4-a0d10a12c567", null, "General", "GENERAL" }
                });
        }
    }
}
