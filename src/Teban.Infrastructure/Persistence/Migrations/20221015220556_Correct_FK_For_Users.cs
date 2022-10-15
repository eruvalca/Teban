using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Correct_FK_For_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AspNetUsers_TebanUserId",
                table: "Budgets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b544ae7c-8b1d-453d-b906-12cfb3b5f6b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc6078ab-1b20-434a-a5e5-326402fa1bb3");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "TebanUserId",
                table: "Budgets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45ceec61-ac97-49dd-b854-59cf8fd11cee", "e8138b98-156f-46c8-b4d7-b68c6bf15629", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e99baff-fbd2-486b-bbe7-233a5b0cd1e4", "85193520-b3da-4756-b21c-c591e6e6fd8a", "General", "GENERAL" });

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AspNetUsers_TebanUserId",
                table: "Budgets",
                column: "TebanUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AspNetUsers_TebanUserId",
                table: "Budgets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45ceec61-ac97-49dd-b854-59cf8fd11cee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e99baff-fbd2-486b-bbe7-233a5b0cd1e4");

            migrationBuilder.AlterColumn<string>(
                name: "TebanUserId",
                table: "Budgets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Budgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b544ae7c-8b1d-453d-b906-12cfb3b5f6b3", "ea80df69-1c8c-48a7-837b-e2cddf8b5727", "General", "GENERAL" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc6078ab-1b20-434a-a5e5-326402fa1bb3", "7883b4e7-2610-4ed2-a3ff-86bf3713d584", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AspNetUsers_TebanUserId",
                table: "Budgets",
                column: "TebanUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
