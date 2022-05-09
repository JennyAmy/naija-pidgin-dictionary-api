using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaPidginAPI.Migrations
{
    public partial class Removedallforeignkeysfromwordtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_ApprovedBy",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_DeletedBy",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_PostedBy",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_ApprovedBy",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_DeletedBy",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_PostedBy",
                table: "Words");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Words_ApprovedBy",
                table: "Words",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Words_DeletedBy",
                table: "Words",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Words_PostedBy",
                table: "Words",
                column: "PostedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_ApprovedBy",
                table: "Words",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_DeletedBy",
                table: "Words",
                column: "DeletedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_PostedBy",
                table: "Words",
                column: "PostedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
