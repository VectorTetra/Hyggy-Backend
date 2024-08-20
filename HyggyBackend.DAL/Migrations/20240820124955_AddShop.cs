using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12739dea-8f5c-4c05-a5d9-d6b2c76f61e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6f06e4e-948a-4f6f-a585-61446e8cb97a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "212a82e6-f75e-43c9-9604-cb914b33cf17", null, "Admin", "ADMIN" },
                    { "834801fc-ffe4-4fc8-82c2-46fbe41127f2", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "212a82e6-f75e-43c9-9604-cb914b33cf17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "834801fc-ffe4-4fc8-82c2-46fbe41127f2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12739dea-8f5c-4c05-a5d9-d6b2c76f61e6", null, "User", "USER" },
                    { "f6f06e4e-948a-4f6f-a585-61446e8cb97a", null, "Admin", "ADMIN" }
                });
        }
    }
}
