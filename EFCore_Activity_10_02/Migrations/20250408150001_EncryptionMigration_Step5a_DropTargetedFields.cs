using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreActivity1002.Migrations
{
    /// <inheritdoc />
    public partial class EncryptionMigrationStep5aDropTargetedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "AK_Employee_NationalIDNumber",
            //    schema: "HumanResources",
            //    table: "Employee");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "HumanResources",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "HumanResources",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "HireDate",
                schema: "HumanResources",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                schema: "HumanResources",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                schema: "HumanResources",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "NationalIDNumber",
                schema: "HumanResources",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "LoginID",
                schema: "HumanResources",
                table: "Employee",
                newName: "LoginId");

            migrationBuilder.AlterColumn<string>(
                name: "LoginId",
                schema: "HumanResources",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldComment: "Network login.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginId",
                schema: "HumanResources",
                table: "Employee",
                newName: "LoginID");

            migrationBuilder.AlterColumn<string>(
                name: "LoginID",
                schema: "HumanResources",
                table: "Employee",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                comment: "Network login.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "HumanResources",
                table: "Employee",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Date of birth.");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "HumanResources",
                table: "Employee",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                comment: "M = Male, F = Female");

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                schema: "HumanResources",
                table: "Employee",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Employee hired on this date.");

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                schema: "HumanResources",
                table: "Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Work title such as Buyer or Sales Representative.");

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                schema: "HumanResources",
                table: "Employee",
                type: "nchar(1)",
                fixedLength: true,
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                comment: "M = Married, S = Single");

            migrationBuilder.AddColumn<string>(
                name: "NationalIDNumber",
                schema: "HumanResources",
                table: "Employee",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                comment: "Unique national identification number such as a social security number.");

            migrationBuilder.CreateIndex(
                name: "AK_Employee_NationalIDNumber",
                schema: "HumanResources",
                table: "Employee",
                column: "NationalIDNumber",
                unique: true);
        }
    }
}
