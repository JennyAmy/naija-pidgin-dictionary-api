using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaPidginAPI.Migrations
{
    public partial class Approvedbyanddeletedbyaddedtowordandusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_PostedBy",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_PostedBy",
                table: "Words");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisabledBy",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Words_ApprovedBy",
                table: "Words",
                column: "ApprovedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_ApprovedBy",
                table: "Words",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_ApprovedBy",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_ApprovedBy",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "DisabledBy",
                table: "Users");

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
                onDelete: ReferentialAction.NoAction);
        }
    }
}
