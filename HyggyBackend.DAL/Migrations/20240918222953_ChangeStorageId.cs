using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStorageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MainStorages_StorageId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Storages_StorageId1",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bc818de-6a89-4f78-a2b0-fb610948c14d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e82edabb-8e01-42d4-a411-2f22a91937fa");

            migrationBuilder.RenameColumn(
                name: "StorageId1",
                table: "AspNetUsers",
                newName: "MainStorageId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_StorageId1",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_MainStorageId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85a452a4-aca7-45ff-af58-3101cc42aa05", null, "User", "USER" },
                    { "bc74f9a9-e6e8-477b-8214-88f539ebd814", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MainStorages_MainStorageId",
                table: "AspNetUsers",
                column: "MainStorageId",
                principalTable: "MainStorages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Storages_StorageId",
                table: "AspNetUsers",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MainStorages_MainStorageId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Storages_StorageId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85a452a4-aca7-45ff-af58-3101cc42aa05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc74f9a9-e6e8-477b-8214-88f539ebd814");

            migrationBuilder.RenameColumn(
                name: "MainStorageId",
                table: "AspNetUsers",
                newName: "StorageId1");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_MainStorageId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_StorageId1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2bc818de-6a89-4f78-a2b0-fb610948c14d", null, "User", "USER" },
                    { "e82edabb-8e01-42d4-a411-2f22a91937fa", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MainStorages_StorageId",
                table: "AspNetUsers",
                column: "StorageId",
                principalTable: "MainStorages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Storages_StorageId1",
                table: "AspNetUsers",
                column: "StorageId1",
                principalTable: "Storages",
                principalColumn: "Id");
        }
    }
}
