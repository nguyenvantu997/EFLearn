using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class createFunctionItemNamesPipeDelimitedString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("EFCore_Library.Scripts.Functions.ItemNamesPipeDelimitedString.ItemNamesPipeDelimitedString.v0.sql");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTON IF EXISTS Dbo.ItemNamesPipeDelimitedString");
        }
    }
}
