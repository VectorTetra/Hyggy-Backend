using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AfterReposCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71d20b48-8aee-4185-89f1-022745a5c732");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5ad47c3-5fed-4931-93c9-6bc5987ddf95");

            migrationBuilder.AddColumn<long>(
                name: "WareTrademarkId",
                table: "Wares",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WareId",
                table: "OrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PriceHistoryId",
                table: "OrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OrderId",
                table: "OrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BlogCategories1",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerWare",
                columns: table => new
                {
                    CustomerFavoritesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FavoriteWaresId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerWare", x => new { x.CustomerFavoritesId, x.FavoriteWaresId });
                    table.ForeignKey(
                        name: "FK_CustomerWare_AspNetUsers_CustomerFavoritesId",
                        column: x => x.CustomerFavoritesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerWare_Wares_FavoriteWaresId",
                        column: x => x.FavoriteWaresId,
                        principalTable: "Wares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WareReviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WareId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareReviews_Wares_WareId",
                        column: x => x.WareId,
                        principalTable: "Wares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WareTrademarks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareTrademarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories2",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviewImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogCategory1Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogCategories2_BlogCategories1_BlogCategory1Id",
                        column: x => x.BlogCategory1Id,
                        principalTable: "BlogCategories1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviewImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogCategory2Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogCategories2_BlogCategory2Id",
                        column: x => x.BlogCategory2Id,
                        principalTable: "BlogCategories2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6e66cc7e-bbc0-46d8-b402-93595f6a2cb5", null, "Admin", "ADMIN" },
                    { "f29f0912-130c-4457-9979-cf3ce46a417e", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wares_WareTrademarkId",
                table: "Wares",
                column: "WareTrademarkId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories2_BlogCategory1Id",
                table: "BlogCategories2",
                column: "BlogCategory1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogCategory2Id",
                table: "Blogs",
                column: "BlogCategory2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWare_FavoriteWaresId",
                table: "CustomerWare",
                column: "FavoriteWaresId");

            migrationBuilder.CreateIndex(
                name: "IX_WareReviews_WareId",
                table: "WareReviews",
                column: "WareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_WareTrademarks_WareTrademarkId",
                table: "Wares",
                column: "WareTrademarkId",
                principalTable: "WareTrademarks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wares_WareTrademarks_WareTrademarkId",
                table: "Wares");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "CustomerWare");

            migrationBuilder.DropTable(
                name: "WareReviews");

            migrationBuilder.DropTable(
                name: "WareTrademarks");

            migrationBuilder.DropTable(
                name: "BlogCategories2");

            migrationBuilder.DropTable(
                name: "BlogCategories1");

            migrationBuilder.DropIndex(
                name: "IX_Wares_WareTrademarkId",
                table: "Wares");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e66cc7e-bbc0-46d8-b402-93595f6a2cb5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f29f0912-130c-4457-9979-cf3ce46a417e");

            migrationBuilder.DropColumn(
                name: "WareTrademarkId",
                table: "Wares");

            migrationBuilder.AlterColumn<long>(
                name: "WareId",
                table: "OrderItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PriceHistoryId",
                table: "OrderItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "OrderId",
                table: "OrderItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71d20b48-8aee-4185-89f1-022745a5c732", null, "Admin", "ADMIN" },
                    { "d5ad47c3-5fed-4931-93c9-6bc5987ddf95", null, "User", "USER" }
                });
        }
    }
}
