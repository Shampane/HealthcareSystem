using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthcareSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LatestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61cf2aa9-4320-4feb-b05e-43a1b759f041");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ea73a29-5479-4a13-818c-8515321e676b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4847554-1846-4b7c-8041-500a44eca4e6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ec36667-ce29-43bd-82bf-2ed53bea6f66", null, "Admin", "ADMIN" },
                    { "ad15823e-f8ca-4fb6-a9dd-eb235ddb0bc3", null, "User", "USER" },
                    { "ee14d503-9e12-4a28-b533-dd041df259a9", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ec36667-ce29-43bd-82bf-2ed53bea6f66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad15823e-f8ca-4fb6-a9dd-eb235ddb0bc3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee14d503-9e12-4a28-b533-dd041df259a9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61cf2aa9-4320-4feb-b05e-43a1b759f041", null, "Admin", "ADMIN" },
                    { "8ea73a29-5479-4a13-818c-8515321e676b", null, "Doctor", "DOCTOR" },
                    { "f4847554-1846-4b7c-8041-500a44eca4e6", null, "User", "USER" }
                });
        }
    }
}
