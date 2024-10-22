using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class multipleWareStatusesInWare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wares_WareStatuses_StatusId",
                table: "Wares");

            migrationBuilder.DropIndex(
                name: "IX_Wares_StatusId",
                table: "Wares");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26fbfc57-d0eb-4515-9eb3-098da151ab41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9142b22-fca0-4367-a731-0099f3b1987b");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Wares");

            migrationBuilder.CreateTable(
                name: "WareWareStatus",
                columns: table => new
                {
                    StatusesId = table.Column<long>(type: "bigint", nullable: false),
                    WaresId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareWareStatus", x => new { x.StatusesId, x.WaresId });
                    table.ForeignKey(
                        name: "FK_WareWareStatus_WareStatuses_StatusesId",
                        column: x => x.StatusesId,
                        principalTable: "WareStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WareWareStatus_Wares_WaresId",
                        column: x => x.WaresId,
                        principalTable: "Wares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a943e0a-e01e-45c1-939e-5a2164370aaf", null, "Admin", "ADMIN" },
                    { "d5ededdb-83e6-42f1-8e1d-e130855547d0", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WareWareStatus_WaresId",
                table: "WareWareStatus",
                column: "WaresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WareWareStatus");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a943e0a-e01e-45c1-939e-5a2164370aaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5ededdb-83e6-42f1-8e1d-e130855547d0");

            migrationBuilder.AddColumn<long>(
                name: "StatusId",
                table: "Wares",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26fbfc57-d0eb-4515-9eb3-098da151ab41", null, "User", "USER" },
                    { "d9142b22-fca0-4367-a731-0099f3b1987b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wares_StatusId",
                table: "Wares",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_WareStatuses_StatusId",
                table: "Wares",
                column: "StatusId",
                principalTable: "WareStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
