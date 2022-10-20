using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Adding_Monthly_Category_Budgets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ac1c375-9e4d-47a8-9b34-7db527c295ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c336155c-a6bd-4a39-89ae-c94962a82cdd");

            migrationBuilder.CreateTable(
                name: "MonthlyCategoryBudgets",
                columns: table => new
                {
                    MonthlyCategoryBudgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyCategoryBudgets", x => x.MonthlyCategoryBudgetId);
                    table.ForeignKey(
                        name: "FK_MonthlyCategoryBudgets_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1448922c-517f-4087-bcf1-c1ab47cf5a92", "7dd187a1-5704-4d7c-a2d4-ae10b8074229", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60fa1c01-5e04-49a0-b5d5-d80bb67b3812", "8df3334c-bd8d-4f25-937f-89df4602112f", "General", "GENERAL" });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyCategoryBudgets_AccountId",
                table: "MonthlyCategoryBudgets",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyCategoryBudgets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1448922c-517f-4087-bcf1-c1ab47cf5a92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60fa1c01-5e04-49a0-b5d5-d80bb67b3812");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7ac1c375-9e4d-47a8-9b34-7db527c295ed", "0a076a88-bed7-4b4e-8b84-5082834e9f8a", "General", "GENERAL" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c336155c-a6bd-4a39-89ae-c94962a82cdd", "3e617462-8320-42ae-9dcc-26784b66340d", "Admin", "ADMIN" });
        }
    }
}
