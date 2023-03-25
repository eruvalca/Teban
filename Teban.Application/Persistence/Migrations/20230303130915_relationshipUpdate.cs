using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TebanRMS.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class relationshipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cf4ce9a-1a67-4c35-8075-043bca933839");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a803d690-52f8-4448-b3ae-f4c8493073ee");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Contacts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73ec1d39-e1ad-485e-b217-a40716957c4d", null, "General", "GENERAL" },
                    { "e36066ac-54a7-47a5-a542-dc9052b3b1b3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73ec1d39-e1ad-485e-b217-a40716957c4d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e36066ac-54a7-47a5-a542-dc9052b3b1b3");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Contacts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0cf4ce9a-1a67-4c35-8075-043bca933839", null, "Admin", "ADMIN" },
                    { "a803d690-52f8-4448-b3ae-f4c8493073ee", null, "General", "GENERAL" }
                });
        }
    }
}
