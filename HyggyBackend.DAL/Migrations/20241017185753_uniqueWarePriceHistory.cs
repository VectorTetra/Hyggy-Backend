using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class uniqueWarePriceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarePriceHistories_WareId",
                table: "WarePriceHistories");

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
                    { "3bb5488a-d065-4b1e-96dc-980f8fa511b4", null, "Admin", "ADMIN" },
                    { "c1616847-211b-49cc-b9b4-1b4dc28b45cc", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarePriceHistories_WareId_Price_EffectiveDate",
                table: "WarePriceHistories",
                columns: new[] { "WareId", "Price", "EffectiveDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarePriceHistories_WareId_Price_EffectiveDate",
                table: "WarePriceHistories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bb5488a-d065-4b1e-96dc-980f8fa511b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1616847-211b-49cc-b9b4-1b4dc28b45cc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cf45492-398d-4869-b494-e8c71d7e9a27", null, "Admin", "ADMIN" },
                    { "b867ee1a-84db-41bd-b4eb-b1a242aba192", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarePriceHistories_WareId",
                table: "WarePriceHistories",
                column: "WareId");
        }
    }
}
