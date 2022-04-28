using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaPidginAPI.Migrations
{
    public partial class Isapprovedaddedtowordtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Words",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Words");
        }
    }
}
