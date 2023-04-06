using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediumTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediumTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    MediumTypeCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movies_MediumTypes_MediumTypeCode",
                        column: x => x.MediumTypeCode,
                        principalTable: "MediumTypes",
                        principalColumn: "Code");
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Drama" },
                    { 3, "Science Fiction" },
                    { 4, "Comedy" },
                    { 5, "Thriller" }
                });

            migrationBuilder.InsertData(
                table: "MediumTypes",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "BR", "Blu-Ray" },
                    { "BR3D", "3D Blu-Ray" },
                    { "BR4K", "4K Blu-Ray" },
                    { "BRHD", "HD Blu-Ray" },
                    { "DVD", "Digital Versitale Disc" },
                    { "VHS", "Videokassette" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "MediumTypeCode", "Price", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("19066b7c-431a-40f6-9f47-a463955f337f"), 1, "VHS", 4.9m, new DateTime(1985, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rambo" },
                    { new Guid("6a419262-20c5-4626-927f-d181f8a34f68"), 3, "DVD", 99.99m, new DateTime(1987, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars - Episode IV" },
                    { new Guid("75b8cac2-19e8-4acb-a406-def948c666f2"), 3, "BR3D", 12.99m, new DateTime(2016, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Trek - Beyond" },
                    { new Guid("83a72fa7-f8f7-406b-b832-a42e695c2ce6"), 5, "BR", 8.50m, new DateTime(2005, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dexter" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MediumTypeCode",
                table: "Movies",
                column: "MediumTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Title",
                table: "Movies",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "MediumTypes");
        }
    }
}
