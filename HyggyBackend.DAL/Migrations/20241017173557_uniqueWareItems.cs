using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class uniqueWareItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WareItems_WareId",
                table: "WareItems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "028c3d27-0c81-4e6a-8383-6959ac059fbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66e3a0ac-7704-4daf-a7d3-415171f82c31");

            migrationBuilder.AddColumn<string>(
                name: "StructureFilePath",
                table: "Wares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorizedCustomerId",
                table: "WareReviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "756375f3-cc60-4061-af14-357cc688cca8", null, "User", "USER" },
                    { "cad22e5e-0232-4b19-8345-c49e68729593", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WareItems_WareId_StorageId",
                table: "WareItems",
                columns: new[] { "WareId", "StorageId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WareItems_WareId_StorageId",
                table: "WareItems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "756375f3-cc60-4061-af14-357cc688cca8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cad22e5e-0232-4b19-8345-c49e68729593");

            migrationBuilder.DropColumn(
                name: "StructureFilePath",
                table: "Wares");

            migrationBuilder.DropColumn(
                name: "AuthorizedCustomerId",
                table: "WareReviews");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "028c3d27-0c81-4e6a-8383-6959ac059fbe", null, "Admin", "ADMIN" },
                    { "66e3a0ac-7704-4daf-a7d3-415171f82c31", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WareItems_WareId",
                table: "WareItems",
                column: "WareId");
        }
    }
}
