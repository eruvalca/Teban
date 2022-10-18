using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Changes_To_Transactions_Structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryGroups");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "120112d6-2e8d-497f-87b4-4694cd0b5a9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a71b8a5-4446-452a-bd9d-9caec4e82749");

            migrationBuilder.RenameColumn(
                name: "CategoryGroupId",
                table: "Categories",
                newName: "BudgetId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryGroupId",
                table: "Categories",
                newName: "IX_Categories_BudgetId");

            migrationBuilder.AddColumn<bool>(
                name: "IsInflow",
                table: "AccountTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "AccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1efeb0e7-1f87-469a-9e18-36fbfffcf9de", "7cfb2e03-b0bd-4f0f-85e4-c65dfe1b55e5", "General", "GENERAL" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "288658eb-901e-4e84-9692-ce702e57b493", "aa494bc9-5ac4-4818-9d74-9804e67d6630", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Budgets_BudgetId",
                table: "Categories",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Budgets_BudgetId",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1efeb0e7-1f87-469a-9e18-36fbfffcf9de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "288658eb-901e-4e84-9692-ce702e57b493");

            migrationBuilder.DropColumn(
                name: "IsInflow",
                table: "AccountTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "AccountTransactions");

            migrationBuilder.RenameColumn(
                name: "BudgetId",
                table: "Categories",
                newName: "CategoryGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_BudgetId",
                table: "Categories",
                newName: "IX_Categories_CategoryGroupId");

            migrationBuilder.CreateTable(
                name: "CategoryGroups",
                columns: table => new
                {
                    CategoryGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGroups", x => x.CategoryGroupId);
                    table.ForeignKey(
                        name: "FK_CategoryGroups_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "120112d6-2e8d-497f-87b4-4694cd0b5a9e", "dd1ed4fc-42ac-4a8e-a5ea-f3d15a18240f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a71b8a5-4446-452a-bd9d-9caec4e82749", "f526a0d4-2eee-45ac-aadf-460f49c2b2d3", "General", "GENERAL" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGroups_BudgetId",
                table: "CategoryGroups",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId",
                table: "Categories",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "CategoryGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
