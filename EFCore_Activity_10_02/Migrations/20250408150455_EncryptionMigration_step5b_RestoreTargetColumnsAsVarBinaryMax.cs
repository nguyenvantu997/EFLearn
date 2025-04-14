using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreActivity1002.Migrations
{
    /// <inheritdoc />
    public partial class EncryptionMigrationstep5bRestoreTargetColumnsAsVarBinaryMax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<byte[]>(
                name: "BirthDate",
                schema: "HumanResources",
                table: "Employee",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "Date of birth.");

            migrationBuilder.AddColumn<byte[]>(
                name: "Gender",
                schema: "HumanResources",
                table: "Employee",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "M = Male, F = Female");

            migrationBuilder.AddColumn<byte[]>(
                name: "HireDate",
                schema: "HumanResources",
                table: "Employee",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "Employee hired on this date.");

            migrationBuilder.AddColumn<byte[]>(
                name: "JobTitle",
                schema: "HumanResources",
                table: "Employee",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "Work title such as Buyer or Sales Representative.");

            migrationBuilder.AddColumn<byte[]>(
                name: "MaritalStatus",
                schema: "HumanResources",
                table: "Employee",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "M = Married, S = Single");

            migrationBuilder.AddColumn<byte[]>(
                name: "NationalIDNumber",
                schema: "HumanResources",
                table: "Employee",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "Unique national identification number such as a social security number.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
