using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Add_Additional_Nav_Props : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId",
                table: "Categories");

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
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryGroupId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "AccountTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b544ae7c-8b1d-453d-b906-12cfb3b5f6b3", "ea80df69-1c8c-48a7-837b-e2cddf8b5727", "General", "GENERAL" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc6078ab-1b20-434a-a5e5-326402fa1bb3", "7883b4e7-2610-4ed2-a3ff-86bf3713d584", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGroups_BudgetId",
                table: "CategoryGroups",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_CategoryId",
                table: "AccountTransactions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Categories_CategoryId",
                table: "AccountTransactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId",
                table: "Categories",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "CategoryGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGroups_Budgets_BudgetId",
                table: "CategoryGroups",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Categories_CategoryId",
                table: "AccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGroups_Budgets_BudgetId",
                table: "CategoryGroups");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGroups_BudgetId",
                table: "CategoryGroups");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_CategoryId",
                table: "AccountTransactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b544ae7c-8b1d-453d-b906-12cfb3b5f6b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc6078ab-1b20-434a-a5e5-326402fa1bb3");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryGroupId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "AccountTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2bc76d68-734e-408d-85b2-b0c5dda478ce", "efa99dc5-a5a6-43fe-99d2-a8f886f6e034", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "773487f7-604e-49ee-a103-f86827e69e14", "2097d75c-782b-4801-ae35-b32d3148df32", "General", "GENERAL" });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId",
                table: "Categories",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "CategoryGroupId");
        }
    }
}
