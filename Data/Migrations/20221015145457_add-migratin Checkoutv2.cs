using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH4_Nhom20.Data.Migrations
{
    public partial class addmigratinCheckoutv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ORDERDETAILS_ORDER_InvoiceId",
                table: "ORDERDETAILS");

            migrationBuilder.DropIndex(
                name: "IX_ORDERDETAILS_InvoiceId",
                table: "ORDERDETAILS");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "ORDERDETAILS");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERDETAILS_OrderId",
                table: "ORDERDETAILS",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ORDERDETAILS_ORDER_OrderId",
                table: "ORDERDETAILS",
                column: "OrderId",
                principalTable: "ORDER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ORDERDETAILS_ORDER_OrderId",
                table: "ORDERDETAILS");

            migrationBuilder.DropIndex(
                name: "IX_ORDERDETAILS_OrderId",
                table: "ORDERDETAILS");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "ORDERDETAILS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ORDERDETAILS_InvoiceId",
                table: "ORDERDETAILS",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ORDERDETAILS_ORDER_InvoiceId",
                table: "ORDERDETAILS",
                column: "InvoiceId",
                principalTable: "ORDER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
