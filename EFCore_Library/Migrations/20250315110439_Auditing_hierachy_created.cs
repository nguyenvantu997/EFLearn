﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Auditinghierachycreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Items",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserId",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Items");
        }
    }
}
