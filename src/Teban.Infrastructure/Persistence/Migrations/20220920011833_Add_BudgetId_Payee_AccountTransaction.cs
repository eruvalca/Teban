using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Add_BudgetId_Payee_AccountTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "089920d4-d74e-4139-b875-082fee17dbe6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18be43ec-e872-4344-9de9-2c2f5e572c03");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "AccountTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Payee",
                table: "AccountTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2bc76d68-734e-408d-85b2-b0c5dda478ce", "efa99dc5-a5a6-43fe-99d2-a8f886f6e034", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "773487f7-604e-49ee-a103-f86827e69e14", "2097d75c-782b-4801-ae35-b32d3148df32", "General", "GENERAL" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bc76d68-734e-408d-85b2-b0c5dda478ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "773487f7-604e-49ee-a103-f86827e69e14");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "Payee",
                table: "AccountTransactions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "089920d4-d74e-4139-b875-082fee17dbe6", "2ba98ef6-2e16-4d3f-b1fe-94ba47f45e1e", "General", "GENERAL" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18be43ec-e872-4344-9de9-2c2f5e572c03", "64abe7df-09e5-4f5c-99e3-bf6b0a17d06c", "Admin", "ADMIN" });
        }
    }
}
