﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreActivity1002.Migrations
{
    /// <inheritdoc />
    public partial class EncryptionMigrationStep4DropConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP CONSTRAINT [CK_Employee_MaritalStatus]");
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP CONSTRAINT [CK_Employee_HireDate]");
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP CONSTRAINT [CK_Employee_Gender]");
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP CONSTRAINT [CK_Employee_BirthDate]");
            migrationBuilder.DropIndex(
                name: "AK_Employee_NationalIDNumber",
                schema: "HumanResources",
                table: "Employee");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
