using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShopId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85f1665a-29f3-42fa-8b1d-bb03d08d7315");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0271b63-552f-4b81-b4e9-21581b930a2d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c80913b-41ad-43b4-9bd8-f68f27b847b9", null, "Admin", "ADMIN" },
                    { "4825d574-a7a8-4b98-b0a7-c9434b7e0a35", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c80913b-41ad-43b4-9bd8-f68f27b847b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4825d574-a7a8-4b98-b0a7-c9434b7e0a35");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85f1665a-29f3-42fa-8b1d-bb03d08d7315", null, "Admin", "ADMIN" },
                    { "d0271b63-552f-4b81-b4e9-21581b930a2d", null, "User", "USER" }
                });
        }
    }
}
