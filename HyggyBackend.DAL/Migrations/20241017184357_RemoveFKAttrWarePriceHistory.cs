using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFKAttrWarePriceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "756375f3-cc60-4061-af14-357cc688cca8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cad22e5e-0232-4b19-8345-c49e68729593");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cf45492-398d-4869-b494-e8c71d7e9a27", null, "Admin", "ADMIN" },
                    { "b867ee1a-84db-41bd-b4eb-b1a242aba192", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cf45492-398d-4869-b494-e8c71d7e9a27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b867ee1a-84db-41bd-b4eb-b1a242aba192");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "756375f3-cc60-4061-af14-357cc688cca8", null, "User", "USER" },
                    { "cad22e5e-0232-4b19-8345-c49e68729593", null, "Admin", "ADMIN" }
                });
        }
    }
}
