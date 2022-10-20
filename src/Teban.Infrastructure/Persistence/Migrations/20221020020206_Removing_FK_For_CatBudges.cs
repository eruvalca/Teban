using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Removing_FK_For_CatBudges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1448922c-517f-4087-bcf1-c1ab47cf5a92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60fa1c01-5e04-49a0-b5d5-d80bb67b3812");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "MonthlyCategoryBudgets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c6c031fb-18df-46be-9bec-1e20600c196f", "4aeece27-4d4a-4f63-9e6a-d3967cc5d602", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0b04032-bdff-4dcc-901f-1f26d5afd03a", "cbb3ae24-2db8-4126-ab32-46bdeb881488", "General", "GENERAL" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6c031fb-18df-46be-9bec-1e20600c196f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0b04032-bdff-4dcc-901f-1f26d5afd03a");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "MonthlyCategoryBudgets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1448922c-517f-4087-bcf1-c1ab47cf5a92", "7dd187a1-5704-4d7c-a2d4-ae10b8074229", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60fa1c01-5e04-49a0-b5d5-d80bb67b3812", "8df3334c-bd8d-4f25-937f-89df4602112f", "General", "GENERAL" });
        }
    }
}
