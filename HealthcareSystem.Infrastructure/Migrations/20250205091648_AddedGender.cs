using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthcareSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "265202dd-9c1d-4284-a24d-c5d590a6a933");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76f9340a-47bf-4dad-909a-f31517a0ff10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df0392fc-ccb2-411b-83d3-13bd78a0fc23");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38304a93-e2aa-4e74-8380-283e119ebc9d", null, "User", "USER" },
                    { "4f524bb9-09bb-4138-95ed-8ee451c671f4", null, "Admin", "ADMIN" },
                    { "b38b2fbb-736e-4483-af7b-0db3e5c6139e", null, "Doctor", "DOCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38304a93-e2aa-4e74-8380-283e119ebc9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f524bb9-09bb-4138-95ed-8ee451c671f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b38b2fbb-736e-4483-af7b-0db3e5c6139e");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "265202dd-9c1d-4284-a24d-c5d590a6a933", null, "Doctor", "DOCTOR" },
                    { "76f9340a-47bf-4dad-909a-f31517a0ff10", null, "User", "USER" },
                    { "df0392fc-ccb2-411b-83d3-13bd78a0fc23", null, "Admin", "ADMIN" }
                });
        }
    }
}
