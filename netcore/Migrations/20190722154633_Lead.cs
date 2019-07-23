using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class Lead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "province",
                table: "Lead");

            migrationBuilder.AddColumn<DateTime>(
                name: "birthDate",
                table: "Lead",
                maxLength: 30,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthDate",
                table: "Lead");

            migrationBuilder.AddColumn<string>(
                name: "province",
                table: "Lead",
                maxLength: 30,
                nullable: true);
        }
    }
}
