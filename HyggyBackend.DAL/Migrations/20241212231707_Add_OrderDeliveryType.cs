using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_OrderDeliveryType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10020c32-477f-4587-a6ce-8521845a4bbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13fc1aff-8761-4594-a831-b058bd2e1c04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35ff5a78-68cc-4b7c-9e0b-f35faf45c32f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a799564-32e2-48a7-b65c-9eef8e2615c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74e1b791-b59a-4ac4-a5da-e53118e1c430");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8883774f-29cd-4497-a8f8-589ba52a66fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1fd6ed5-2c3e-4add-834a-34b0b7de4505");

            migrationBuilder.AddColumn<long>(
                name: "DeliveryTypeId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "OrderDeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    MinDeliveryTimeInDays = table.Column<int>(type: "int", nullable: false),
                    MaxDeliveryTimeInDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveryTypes", x => x.Id);
                });

            migrationBuilder.Sql(
                @"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'General Accountant')
                  BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                    VALUES ('061c1063-ad01-42ec-8a34-a8264e9fade9', 'General Accountant', 'GENERAL ACCOUNTANT')
                  END");

            migrationBuilder.Sql(
                @"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Saler')
                  BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                    VALUES ('22eb0b7d-0b41-45d7-9086-aa27ad76df6b', 'Saler', 'SALER')
                  END");

            migrationBuilder.Sql(
                @"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Accountant')
                  BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                    VALUES ('30a548c6-c78b-46c2-86af-6182bba41cbc', 'Accountant', 'ACCOUNTANT')
                  END");

            migrationBuilder.Sql(
                @"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Admin')
                  BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                    VALUES ('46a00b96-498e-43f2-b989-525de2336f60', 'Admin', 'ADMIN')
                  END");

            migrationBuilder.Sql(
                @"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Storekeeper')
                  BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                    VALUES ('c5274b8d-ad9a-4609-ab8d-67fa93a7d836', 'Storekeeper', 'STOREKEEPER')
                  END");

            migrationBuilder.Sql(
                @"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Owner')
                  BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                    VALUES ('d81a8913-5d0a-4a2b-b24a-80a04bcadc4e', 'Owner', 'OWNER')
                  END");

            migrationBuilder.Sql(
                @"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'User')
                  BEGIN
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                    VALUES ('f9354412-eef7-4552-8c1f-d59202ae9a38', 'User', 'USER')
                  END");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderDeliveryTypes_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId",
                principalTable: "OrderDeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderDeliveryTypes_DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderDeliveryTypes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "061c1063-ad01-42ec-8a34-a8264e9fade9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22eb0b7d-0b41-45d7-9086-aa27ad76df6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30a548c6-c78b-46c2-86af-6182bba41cbc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46a00b96-498e-43f2-b989-525de2336f60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5274b8d-ad9a-4609-ab8d-67fa93a7d836");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d81a8913-5d0a-4a2b-b24a-80a04bcadc4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9354412-eef7-4552-8c1f-d59202ae9a38");

            migrationBuilder.DropColumn(
                name: "DeliveryTypeId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10020c32-477f-4587-a6ce-8521845a4bbd", null, "Accountant", "ACCOUNTANT" },
                    { "13fc1aff-8761-4594-a831-b058bd2e1c04", null, "Storekeeper", "STOREKEEPER" },
                    { "35ff5a78-68cc-4b7c-9e0b-f35faf45c32f", null, "Owner", "OWNER" },
                    { "4a799564-32e2-48a7-b65c-9eef8e2615c7", null, "Admin", "ADMIN" },
                    { "74e1b791-b59a-4ac4-a5da-e53118e1c430", null, "General Accountant", "GENERAL ACCOUNTANT" },
                    { "8883774f-29cd-4497-a8f8-589ba52a66fe", null, "User", "USER" },
                    { "f1fd6ed5-2c3e-4add-834a-34b0b7de4505", null, "Saler", "SALER" }
                });
        }
    }
}
