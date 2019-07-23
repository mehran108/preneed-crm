using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class Opportunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "policyId",
                table: "Opportunity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    policyId = table.Column<string>(maxLength: 38, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    policyName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.policyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_policyId",
                table: "Opportunity",
                column: "policyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunity_Policy_policyId",
                table: "Opportunity",
                column: "policyId",
                principalTable: "Policy",
                principalColumn: "policyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opportunity_Policy_policyId",
                table: "Opportunity");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropIndex(
                name: "IX_Opportunity_policyId",
                table: "Opportunity");

            migrationBuilder.DropColumn(
                name: "policyId",
                table: "Opportunity");
        }
    }
}
