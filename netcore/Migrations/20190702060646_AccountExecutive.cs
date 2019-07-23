using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class AccountExecutive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "province",
                table: "AccountExecutive",
                newName: "state");

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "Warehouse",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zipCode",
                table: "Warehouse",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "Vendor",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zipCode",
                table: "Vendor",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "Lead",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zipCode",
                table: "Lead",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "Customer",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zipCode",
                table: "Customer",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "Branch",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zipCode",
                table: "Branch",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "birthDate",
                table: "AccountExecutive",
                maxLength: 30,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "zipCode",
                table: "AccountExecutive",
                maxLength: 38,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "zipCode",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "state",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "zipCode",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "state",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "zipCode",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "state",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "zipCode",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "state",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "zipCode",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "birthDate",
                table: "AccountExecutive");

            migrationBuilder.DropColumn(
                name: "zipCode",
                table: "AccountExecutive");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "AccountExecutive",
                newName: "province");
        }
    }
}
