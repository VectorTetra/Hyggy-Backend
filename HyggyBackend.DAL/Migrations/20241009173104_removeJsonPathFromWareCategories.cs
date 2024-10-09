using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeJsonPathFromWareCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a943e0a-e01e-45c1-939e-5a2164370aaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5ededdb-83e6-42f1-8e1d-e130855547d0");

            migrationBuilder.DropColumn(
                name: "JSONStructureFilePath",
                table: "WareCategories3");

            migrationBuilder.DropColumn(
                name: "JSONStructureFilePath",
                table: "WareCategories2");

            migrationBuilder.DropColumn(
                name: "JSONStructureFilePath",
                table: "WareCategories1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0eff3950-60e8-423f-94a9-526a38529a10", null, "Admin", "ADMIN" },
                    { "7257eb1a-220a-421d-b0d4-b43af00e0866", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0eff3950-60e8-423f-94a9-526a38529a10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7257eb1a-220a-421d-b0d4-b43af00e0866");

            migrationBuilder.AddColumn<string>(
                name: "JSONStructureFilePath",
                table: "WareCategories3",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JSONStructureFilePath",
                table: "WareCategories2",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JSONStructureFilePath",
                table: "WareCategories1",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a943e0a-e01e-45c1-939e-5a2164370aaf", null, "Admin", "ADMIN" },
                    { "d5ededdb-83e6-42f1-8e1d-e130855547d0", null, "User", "USER" }
                });
        }
    }
}
