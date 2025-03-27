using EFCore_Library.Scripts;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class createFunctionGetItemsTotalValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("EFCore_Library.Scripts.Functions.GetItemsTotalValue.GetItemsTotalValue.v0.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTON IF EXISTS Dbo.GetItemsTotalValue");
        }
    }
}
