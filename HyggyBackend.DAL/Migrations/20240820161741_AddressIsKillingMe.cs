using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddressIsKillingMe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Shops_ShopId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ShopId",
                table: "Address");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0196a4eb-a483-4ad7-acc9-bfb84ca924cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3faca307-a2c7-40b1-8e0f-824daf9f3f8b");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Address");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "539bcbb8-9f6b-4977-b1dc-55ce7c78ade2", null, "Admin", "ADMIN" },
                    { "dbac026f-d2a4-45e0-b455-b4b804235817", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "539bcbb8-9f6b-4977-b1dc-55ce7c78ade2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbac026f-d2a4-45e0-b455-b4b804235817");

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
                    { "0196a4eb-a483-4ad7-acc9-bfb84ca924cd", null, "User", "USER" },
                    { "3faca307-a2c7-40b1-8e0f-824daf9f3f8b", null, "Admin", "ADMIN" }
                });

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
    }
}
