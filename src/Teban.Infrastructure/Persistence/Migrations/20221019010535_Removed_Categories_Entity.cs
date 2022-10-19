using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Removed_Categories_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                table: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_AccountId",
                table: "AccountTransactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1efeb0e7-1f87-469a-9e18-36fbfffcf9de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "288658eb-901e-4e84-9692-ce702e57b493");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TransactionEntries");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "TransactionEntries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7ac1c375-9e4d-47a8-9b34-7db527c295ed", "0a076a88-bed7-4b4e-8b84-5082834e9f8a", "General", "GENERAL" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c336155c-a6bd-4a39-89ae-c94962a82cdd", "3e617462-8320-42ae-9dcc-26784b66340d", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_BudgetId",
                table: "AccountTransactions",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Budgets_BudgetId",
                table: "AccountTransactions",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Budgets_BudgetId",
                table: "AccountTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_BudgetId",
                table: "AccountTransactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ac1c375-9e4d-47a8-9b34-7db527c295ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c336155c-a6bd-4a39-89ae-c94962a82cdd");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "TransactionEntries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TransactionEntries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "AccountTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1efeb0e7-1f87-469a-9e18-36fbfffcf9de", "7cfb2e03-b0bd-4f0f-85e4-c65dfe1b55e5", "General", "GENERAL" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "288658eb-901e-4e84-9692-ce702e57b493", "aa494bc9-5ac4-4818-9d74-9804e67d6630", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_AccountId",
                table: "AccountTransactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BudgetId",
                table: "Categories",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                table: "AccountTransactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }
    }
}
