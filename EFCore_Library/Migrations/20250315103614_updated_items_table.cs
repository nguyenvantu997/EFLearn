﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class updateditemstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchasedDate",
                table: "Items",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldDate",
                table: "Items",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsOnSale",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PurchasedDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SoldDate",
                table: "Items");
        }
    }
}
