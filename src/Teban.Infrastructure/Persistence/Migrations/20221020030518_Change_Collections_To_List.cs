using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teban.Infrastructure.Persistence.Migrations
{
    public partial class Change_Collections_To_List : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6c031fb-18df-46be-9bec-1e20600c196f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0b04032-bdff-4dcc-901f-1f26d5afd03a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "15d239b1-5079-4f48-b2e4-d095f087d193", "f86797a7-4098-4996-bc5d-92dd82fb889b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "facc3663-8096-42e9-8853-693de015ee74", "713c8f0a-ed96-4a02-91fd-77c48e463acb", "General", "GENERAL" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15d239b1-5079-4f48-b2e4-d095f087d193");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "facc3663-8096-42e9-8853-693de015ee74");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c6c031fb-18df-46be-9bec-1e20600c196f", "4aeece27-4d4a-4f63-9e6a-d3967cc5d602", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0b04032-bdff-4dcc-901f-1f26d5afd03a", "cbb3ae24-2db8-4126-ab32-46bdeb881488", "General", "GENERAL" });
        }
    }
}
