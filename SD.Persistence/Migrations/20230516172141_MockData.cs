using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MediumTypes",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "MediumTypeCode", "Price", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("a8f1e4f8-14e1-4f0e-8f3b-4f7b44a8be30"), 2, "BR", 10.99m, null, new DateTime(2014, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Interstellar" },
                    { new Guid("b23e5e6f-9a67-4a74-8231-2d3b6d384a0a"), 1, "DVD", 14.99m, null, new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Dark Knight" },
                    { new Guid("c1c1f6b7-1c29-4c9e-a6f2-0b582b5b8a13"), 2, "BR", 9.99m, null, new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inception" },
                    { new Guid("d3f2b0a7-836c-4b44-a1a9-9d2b4b4a3b30"), 4, "BR", 15.99m, null, new DateTime(2003, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Return of the King" },
                    { new Guid("de2a6f8d-1c4e-4c5e-b9f2-0a2b6b4a5b39"), 1, "DVD", 7.99m, null, new DateTime(2009, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inglourious Basterds" },
                    { new Guid("ef2b7b9a-7d3d-4f4d-8b2b-0b3b6b5b6b4a"), 1, "DVD", 7.99m, null, new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pulp Fiction" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a8f1e4f8-14e1-4f0e-8f3b-4f7b44a8be30"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("b23e5e6f-9a67-4a74-8231-2d3b6d384a0a"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("c1c1f6b7-1c29-4c9e-a6f2-0b582b5b8a13"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("d3f2b0a7-836c-4b44-a1a9-9d2b4b4a3b30"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("de2a6f8d-1c4e-4c5e-b9f2-0a2b6b4a5b39"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ef2b7b9a-7d3d-4f4d-8b2b-0b3b6b5b6b4a"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MediumTypes",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
