using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthcareSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb612841-c6df-40f7-9393-b3af3f9bc878");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd65d702-508c-41c2-9311-910d529d1698");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbbd2d8b-f301-48a1-a05b-65c02e8374e4");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17bb74c4-329d-4da3-8ddb-079757a59f1e", null, "User", "USER" },
                    { "1ae2a9d2-bcca-4cc5-90f5-e060f450c6ea", null, "Doctor", "DOCTOR" },
                    { "d314b64a-8d62-4089-bd0a-bf00ed2459a2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17bb74c4-329d-4da3-8ddb-079757a59f1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ae2a9d2-bcca-4cc5-90f5-e060f450c6ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d314b64a-8d62-4089-bd0a-bf00ed2459a2");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bb612841-c6df-40f7-9393-b3af3f9bc878", null, "Admin", "ADMIN" },
                    { "bd65d702-508c-41c2-9311-910d529d1698", null, "Doctor", "DOCTOR" },
                    { "dbbd2d8b-f301-48a1-a05b-65c02e8374e4", null, "User", "USER" }
                });
        }
    }
}
