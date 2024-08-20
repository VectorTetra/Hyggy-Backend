using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Address_DeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Address_AddressId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Address_AddressId",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Storages_AddressId",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "539bcbb8-9f6b-4977-b1dc-55ce7c78ade2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbac026f-d2a4-45e0-b455-b4b804235817");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09ec0d64-c9e6-4e54-bcc4-c1fba343fdf3", null, "Admin", "ADMIN" },
                    { "17dc3d84-544d-48d3-af92-1ce0264d6cfe", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storages_AddressId",
                table: "Storages",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Addresses_AddressId",
                table: "Shops",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Addresses_AddressId",
                table: "Storages",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_DeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Addresses_AddressId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Addresses_AddressId",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Storages_AddressId",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09ec0d64-c9e6-4e54-bcc4-c1fba343fdf3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17dc3d84-544d-48d3-af92-1ce0264d6cfe");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "539bcbb8-9f6b-4977-b1dc-55ce7c78ade2", null, "Admin", "ADMIN" },
                    { "dbac026f-d2a4-45e0-b455-b4b804235817", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storages_AddressId",
                table: "Storages",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Address_DeliveryAddressId",
                table: "Orders",
                column: "DeliveryAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Address_AddressId",
                table: "Shops",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Address_AddressId",
                table: "Storages",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
