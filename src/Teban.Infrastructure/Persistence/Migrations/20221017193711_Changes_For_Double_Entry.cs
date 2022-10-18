using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Changes_For_Double_Entry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                table: "AccountTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Categories_CategoryId",
                table: "AccountTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AccountTransactions_CategoryId",
                table: "AccountTransactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45ceec61-ac97-49dd-b854-59cf8fd11cee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e99baff-fbd2-486b-bbe7-233a5b0cd1e4");

            migrationBuilder.DropColumn(
                name: "CreditAmount",
                table: "TransactionEntries");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AccountTransactions");

            migrationBuilder.RenameColumn(
                name: "DebitAmount",
                table: "TransactionEntries",
                newName: "Amount");

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

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "AccountTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsTransfer",
                table: "AccountTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "120112d6-2e8d-497f-87b4-4694cd0b5a9e", "dd1ed4fc-42ac-4a8e-a5ea-f3d15a18240f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a71b8a5-4446-452a-bd9d-9caec4e82749", "f526a0d4-2eee-45ac-aadf-460f49c2b2d3", "General", "GENERAL" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                table: "AccountTransactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                table: "AccountTransactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "120112d6-2e8d-497f-87b4-4694cd0b5a9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a71b8a5-4446-452a-bd9d-9caec4e82749");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TransactionEntries");

            migrationBuilder.DropColumn(
                name: "IsTransfer",
                table: "AccountTransactions");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "TransactionEntries",
                newName: "DebitAmount");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "TransactionEntries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmount",
                table: "TransactionEntries",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "AccountTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "AccountTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45ceec61-ac97-49dd-b854-59cf8fd11cee", "e8138b98-156f-46c8-b4d7-b68c6bf15629", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e99baff-fbd2-486b-bbe7-233a5b0cd1e4", "85193520-b3da-4756-b21c-c591e6e6fd8a", "General", "GENERAL" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_CategoryId",
                table: "AccountTransactions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Accounts_AccountId",
                table: "AccountTransactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransactions_Categories_CategoryId",
                table: "AccountTransactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
