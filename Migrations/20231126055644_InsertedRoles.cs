using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace shoes_final_exam.Migrations
{
    /// <inheritdoc />
    public partial class InsertedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "375f43c2-8360-47f0-9c78-e6e2c606c686", "eecd3b1b-20f2-4594-b8a5-235e0af8ede4", "User", "USER" },
                    { "9eaefe81-258e-45e5-96d1-2b170f02b5db", "81077a37-3d37-4c5d-a883-34b77a4c5e07", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "375f43c2-8360-47f0-9c78-e6e2c606c686");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9eaefe81-258e-45e5-96d1-2b170f02b5db");
        }
    }
}
