using EFCore_Library.Scripts;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class updateProcGetItemsForListingRemoveGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("EFCore_Library.Scripts.Procedures.GetItemsForListing.GetItemsForListing.v1.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("EFCore_Library.Scripts.Procedures.GetItemsForListing.GetItemsForListing.v0.sql");
        }
    }
}
