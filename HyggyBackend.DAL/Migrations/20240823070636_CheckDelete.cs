using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CheckDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Addresses_AddressId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_AddressId",
                table: "Shops");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43fbb869-1829-4439-9691-073103bddb6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0838325-42ed-4472-a7b0-da169e9880a0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f524de0-0c2d-4722-925b-0f6bc58d2f25", null, "User", "USER" },
                    { "83df5277-a7e2-4f13-91cd-8448e7bd1bdd", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_AddressId",
                table: "Shops",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Addresses_AddressId",
                table: "Shops",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Addresses_AddressId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_AddressId",
                table: "Shops");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f524de0-0c2d-4722-925b-0f6bc58d2f25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83df5277-a7e2-4f13-91cd-8448e7bd1bdd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43fbb869-1829-4439-9691-073103bddb6f", null, "Admin", "ADMIN" },
                    { "e0838325-42ed-4472-a7b0-da169e9880a0", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_AddressId",
                table: "Shops",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Addresses_AddressId",
                table: "Shops",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
