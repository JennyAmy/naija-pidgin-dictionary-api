using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaPidginAPI.Migrations
{
    public partial class useradded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostedBy",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedOn",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Words_PostedBy",
                table: "Words",
                column: "PostedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_PostedBy",
                table: "Words",
                column: "PostedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_PostedBy",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_PostedBy",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "PostedBy",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "PostedOn",
                table: "Words");
        }
    }
}
