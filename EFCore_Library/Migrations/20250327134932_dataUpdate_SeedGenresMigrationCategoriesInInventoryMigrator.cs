using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class dataUpdateSeedGenresMigrationCategoriesInInventoryMigrator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDate", "IsActive", "IsDeleted", "LastModifiedDate", "LastModifiedUserId", "Name" },
                values: new object[,]
                {
                    { 1, "2fd28110-93d0-427d-9207-d55dbca680fa", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new DateTime(2025, 3, 27, 20, 49, 32, 10, DateTimeKind.Local).AddTicks(966), "2fd28110-93d0-427d-9207-d55dbca680fa", "Fantasy" },
                    { 2, "2fd28110-93d0-427d-9207-d55dbca680fa", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new DateTime(2025, 3, 27, 20, 49, 32, 10, DateTimeKind.Local).AddTicks(977), "2fd28110-93d0-427d-9207-d55dbca680fa", "Sci/Fi" },
                    { 3, "2fd28110-93d0-427d-9207-d55dbca680fa", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new DateTime(2025, 3, 27, 20, 49, 32, 10, DateTimeKind.Local).AddTicks(979), "2fd28110-93d0-427d-9207-d55dbca680fa", "Horror" },
                    { 4, "2fd28110-93d0-427d-9207-d55dbca680fa", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new DateTime(2025, 3, 27, 20, 49, 32, 10, DateTimeKind.Local).AddTicks(980), "2fd28110-93d0-427d-9207-d55dbca680fa", "Comedy" },
                    { 5, "2fd28110-93d0-427d-9207-d55dbca680fa", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new DateTime(2025, 3, 27, 20, 49, 32, 10, DateTimeKind.Local).AddTicks(982), "2fd28110-93d0-427d-9207-d55dbca680fa", "Drama" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
