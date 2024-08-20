using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class stilladdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c971a92-a0ac-476c-a871-a884ed63b8d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ebb93a1-5fb2-4152-b2b9-79fd4175d1d6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0196a4eb-a483-4ad7-acc9-bfb84ca924cd", null, "User", "USER" },
                    { "3faca307-a2c7-40b1-8e0f-824daf9f3f8b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0196a4eb-a483-4ad7-acc9-bfb84ca924cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3faca307-a2c7-40b1-8e0f-824daf9f3f8b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c971a92-a0ac-476c-a871-a884ed63b8d2", null, "User", "USER" },
                    { "9ebb93a1-5fb2-4152-b2b9-79fd4175d1d6", null, "Admin", "ADMIN" }
                });
        }
    }
}
