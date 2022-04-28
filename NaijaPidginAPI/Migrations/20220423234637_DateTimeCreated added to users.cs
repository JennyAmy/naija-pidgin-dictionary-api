using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaPidginAPI.Migrations
{
    public partial class DateTimeCreatedaddedtousers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCreated",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeCreated",
                table: "Users");
        }
    }
}
