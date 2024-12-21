using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HyggyBackend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addStorageIdToShopEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var newRoles = new[]
            {
                new { Id = "2ee9e46d-768b-4969-95f7-339b2ee2de63", Name = "Accountant", NormalizedName = "ACCOUNTANT" },
                new { Id = "3f0b1101-4bc0-4d7c-992c-d7f8c3831cd8", Name = "Owner", NormalizedName = "OWNER" },
                new { Id = "593fb54a-53d0-49a8-95dd-8dcf23026394", Name = "User", NormalizedName = "USER" },
                new { Id = "77fb1e34-5af5-461c-8842-fe5113865c12", Name = "Storekeeper", NormalizedName = "STOREKEEPER" },
                new { Id = "82a7defe-07a8-4f36-b72c-91e512b5124c", Name = "Saler", NormalizedName = "SALER" },
                new { Id = "87e9e5ef-5a51-41a5-8524-9f0ca5359a38", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "e5ebe52a-0f45-42ff-bec7-9189be7ebc4f", Name = "General Accountant", NormalizedName = "GENERAL ACCOUNTANT" }
            };

            foreach (var role in newRoles)
            {
                migrationBuilder.Sql($"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = '{role.Name}') BEGIN INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{role.Id}', '{role.Name}', '{role.NormalizedName}') END");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var roleIds = new[]
            {
                "2ee9e46d-768b-4969-95f7-339b2ee2de63",
                "3f0b1101-4bc0-4d7c-992c-d7f8c3831cd8",
                "593fb54a-53d0-49a8-95dd-8dcf23026394",
                "77fb1e34-5af5-461c-8842-fe5113865c12",
                "82a7defe-07a8-4f36-b72c-91e512b5124c",
                "87e9e5ef-5a51-41a5-8524-9f0ca5359a38",
                "e5ebe52a-0f45-42ff-bec7-9189be7ebc4f"
            };

            foreach (var roleId in roleIds)
            {
                migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{roleId}'");
            }
        }
    }
}
