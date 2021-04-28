using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OOP_L_4_L_5.Storage.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAnime",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 500, nullable: false),
                    szStudio = table.Column<string>(maxLength: 100, nullable: false),
                    dtDate = table.Column<DateTime>(nullable: false),
                    enAnimeType = table.Column<int>(nullable: false),
                    intEpisodesCount = table.Column<int>(nullable: false),
                    enGenre = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAnime", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblBook",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 500, nullable: false),
                    szAuthor = table.Column<string>(maxLength: 100, nullable: false),
                    dtDate = table.Column<DateTime>(nullable: false),
                    enBookType = table.Column<int>(nullable: false),
                    intPageCount = table.Column<int>(nullable: false),
                    enGenres = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBook", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblClient",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 100, nullable: false),
                    szTelephone = table.Column<string>(maxLength: 12, nullable: false),
                    szEmail = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblClient", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblFigure",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 150, nullable: false),
                    szSource = table.Column<string>(maxLength: 500, nullable: true),
                    intScale = table.Column<int>(nullable: false),
                    intWeight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFigure", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblProduct",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    gMatterId = table.Column<Guid>(nullable: false),
                    enProductType = table.Column<int>(nullable: false),
                    intPrice = table.Column<int>(nullable: false),
                    intCountInStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProduct", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblOrder",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    gClientId = table.Column<Guid>(nullable: false),
                    enOrderStatus = table.Column<int>(nullable: false),
                    dtDateOfCompletion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrder", x => x.gId);
                    table.ForeignKey(
                        name: "FK_tblOrder_tblClient_gClientId",
                        column: x => x.gClientId,
                        principalTable: "tblClient",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPaymentInfo",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 100, nullable: false),
                    szCardNumber = table.Column<string>(maxLength: 16, nullable: false),
                    szCvc = table.Column<string>(maxLength: 3, nullable: false),
                    gClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaymentInfo", x => x.gId);
                    table.ForeignKey(
                        name: "FK_tblPaymentInfo_tblClient_gClientId",
                        column: x => x.gClientId,
                        principalTable: "tblClient",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_tblOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tblOrder",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_tblProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tblProduct",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrder_gClientId",
                table: "tblOrder",
                column: "gClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentInfo_gClientId",
                table: "tblPaymentInfo",
                column: "gClientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "tblAnime");

            migrationBuilder.DropTable(
                name: "tblBook");

            migrationBuilder.DropTable(
                name: "tblFigure");

            migrationBuilder.DropTable(
                name: "tblPaymentInfo");

            migrationBuilder.DropTable(
                name: "tblOrder");

            migrationBuilder.DropTable(
                name: "tblProduct");

            migrationBuilder.DropTable(
                name: "tblClient");
        }
    }
}
