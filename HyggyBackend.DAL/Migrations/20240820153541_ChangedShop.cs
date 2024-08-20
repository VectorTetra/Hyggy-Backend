using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangedShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Address_AddressId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Storages_ShopId",
                table: "Storages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "212a82e6-f75e-43c9-9604-cb914b33cf17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "834801fc-ffe4-4fc8-82c2-46fbe41127f2");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Shops",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "StorageId",
                table: "Shops",
                type: "bigint",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85f1665a-29f3-42fa-8b1d-bb03d08d7315", null, "Admin", "ADMIN" },
                    { "d0271b63-552f-4b81-b4e9-21581b930a2d", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storages_ShopId",
                table: "Storages",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_StorageId",
                table: "Shops",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Address_AddressId",
                table: "Shops",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Storages_StorageId",
                table: "Shops",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Address_AddressId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Storages_StorageId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Storages_ShopId",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Shops_StorageId",
                table: "Shops");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85f1665a-29f3-42fa-8b1d-bb03d08d7315");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0271b63-552f-4b81-b4e9-21581b930a2d");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Shops");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Shops",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "212a82e6-f75e-43c9-9604-cb914b33cf17", null, "Admin", "ADMIN" },
                    { "834801fc-ffe4-4fc8-82c2-46fbe41127f2", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storages_ShopId",
                table: "Storages",
                column: "ShopId",
                unique: true,
                filter: "[ShopId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Address_AddressId",
                table: "Shops",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
