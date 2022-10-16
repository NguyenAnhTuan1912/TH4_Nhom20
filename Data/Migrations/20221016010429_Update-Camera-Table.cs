using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH4_Nhom20.Data.Migrations
{
    public partial class UpdateCameraTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "CAMERA",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "CAMERA",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "CAMERA");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CAMERA");
        }
    }
}
