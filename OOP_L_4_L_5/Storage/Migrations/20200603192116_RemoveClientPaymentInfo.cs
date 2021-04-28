using Microsoft.EntityFrameworkCore.Migrations;

namespace OOP_L_4_L_5.Storage.Migrations
{
    public partial class RemoveClientPaymentInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblPaymentInfo_gClientId",
                table: "tblPaymentInfo");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentInfo_gClientId",
                table: "tblPaymentInfo",
                column: "gClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblPaymentInfo_gClientId",
                table: "tblPaymentInfo");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaymentInfo_gClientId",
                table: "tblPaymentInfo",
                column: "gClientId",
                unique: true);
        }
    }
}
