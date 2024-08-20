using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addressagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shops_AddressId",
                table: "Shops");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb96b694-4855-4a25-afbb-c11b15ef14a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f11b8599-1129-494a-bc16-2e7097b456a2");

            migrationBuilder.AddColumn<long>(
                name: "ShopId",
                table: "Address",
                type: "bigint",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c971a92-a0ac-476c-a871-a884ed63b8d2", null, "User", "USER" },
                    { "9ebb93a1-5fb2-4152-b2b9-79fd4175d1d6", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_AddressId",
                table: "Shops",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ShopId",
                table: "Address",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Shops_ShopId",
                table: "Address",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Shops_ShopId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Shops_AddressId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Address_ShopId",
                table: "Address");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c971a92-a0ac-476c-a871-a884ed63b8d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ebb93a1-5fb2-4152-b2b9-79fd4175d1d6");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Address");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "eb96b694-4855-4a25-afbb-c11b15ef14a7", null, "User", "USER" },
                    { "f11b8599-1129-494a-bc16-2e7097b456a2", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_AddressId",
                table: "Shops",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");
        }
    }
}
