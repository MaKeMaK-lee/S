using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OOP_L_4_L_5.Storage.Migrations
{
    public partial class remskobkiClientPaymentinfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnimeId",
                table: "tblProduct",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "tblProduct",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FigureId",
                table: "tblProduct",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblProduct_AnimeId",
                table: "tblProduct",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProduct_BookId",
                table: "tblProduct",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProduct_FigureId",
                table: "tblProduct",
                column: "FigureId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProduct_tblAnime_AnimeId",
                table: "tblProduct",
                column: "AnimeId",
                principalTable: "tblAnime",
                principalColumn: "gId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProduct_tblBook_BookId",
                table: "tblProduct",
                column: "BookId",
                principalTable: "tblBook",
                principalColumn: "gId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProduct_tblFigure_FigureId",
                table: "tblProduct",
                column: "FigureId",
                principalTable: "tblFigure",
                principalColumn: "gId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProduct_tblAnime_AnimeId",
                table: "tblProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_tblProduct_tblBook_BookId",
                table: "tblProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_tblProduct_tblFigure_FigureId",
                table: "tblProduct");

            migrationBuilder.DropIndex(
                name: "IX_tblProduct_AnimeId",
                table: "tblProduct");

            migrationBuilder.DropIndex(
                name: "IX_tblProduct_BookId",
                table: "tblProduct");

            migrationBuilder.DropIndex(
                name: "IX_tblProduct_FigureId",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "FigureId",
                table: "tblProduct");
        }
    }
}
