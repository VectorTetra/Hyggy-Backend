using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MinusStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Storages_StorageId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_StorageId",
                table: "Shops");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09ec0d64-c9e6-4e54-bcc4-c1fba343fdf3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17dc3d84-544d-48d3-af92-1ce0264d6cfe");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Shops");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "404ca1a1-aa92-479d-b562-79bf01874d1d", null, "Admin", "ADMIN" },
                    { "649cf52f-c2ed-4e14-b010-2e691d1b0d64", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "404ca1a1-aa92-479d-b562-79bf01874d1d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "649cf52f-c2ed-4e14-b010-2e691d1b0d64");

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
                    { "09ec0d64-c9e6-4e54-bcc4-c1fba343fdf3", null, "Admin", "ADMIN" },
                    { "17dc3d84-544d-48d3-af92-1ce0264d6cfe", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_StorageId",
                table: "Shops",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Storages_StorageId",
                table: "Shops",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }
    }
}
