using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RatingsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Rating",
                table: "Movies",
                type: "tinyint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("19066b7c-431a-40f6-9f47-a463955f337f"),
                column: "Rating",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("6a419262-20c5-4626-927f-d181f8a34f68"),
                column: "Rating",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("75b8cac2-19e8-4acb-a406-def948c666f2"),
                column: "Rating",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("83a72fa7-f8f7-406b-b832-a42e695c2ce6"),
                column: "Rating",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");
        }
    }
}
