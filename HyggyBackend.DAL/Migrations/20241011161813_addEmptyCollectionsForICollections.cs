using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addEmptyCollectionsForICollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eff3950-60e8-423f-94a9-526a38529a10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7257eb1a-220a-421d-b0d4-b43af00e0866");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32095b31-6890-4d15-a22c-b188e1806133", null, "Admin", "ADMIN" },
                    { "34f2d014-2941-43b1-9bc0-937d1bd6ab96", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32095b31-6890-4d15-a22c-b188e1806133");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34f2d014-2941-43b1-9bc0-937d1bd6ab96");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0eff3950-60e8-423f-94a9-526a38529a10", null, "Admin", "ADMIN" },
                    { "7257eb1a-220a-421d-b0d4-b43af00e0866", null, "User", "USER" }
                });
        }
    }
}
