﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaPidginAPI.Migrations
{
    public partial class wordclassIdadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordClasses_WordClassId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "WordClassId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordClasses_WordClassId",
                table: "Words",
                column: "WordClassId",
                principalTable: "WordClasses",
                principalColumn: "WordClassId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordClasses_WordClassId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "WordClassId",
                table: "Words",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordClasses_WordClassId",
                table: "Words",
                column: "WordClassId",
                principalTable: "WordClasses",
                principalColumn: "WordClassId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
