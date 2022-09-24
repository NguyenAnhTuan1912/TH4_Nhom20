using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH4_Nhom20.Data.Migrations
{
    public partial class Camera03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAMERA_BRAND_CategoryId",
                table: "CAMERA");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CAMERA",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_CAMERA_CategoryId",
                table: "CAMERA",
                newName: "IX_CAMERA_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAMERA_BRAND_BrandId",
                table: "CAMERA",
                column: "BrandId",
                principalTable: "BRAND",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAMERA_BRAND_BrandId",
                table: "CAMERA");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "CAMERA",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CAMERA_BrandId",
                table: "CAMERA",
                newName: "IX_CAMERA_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CAMERA_BRAND_CategoryId",
                table: "CAMERA",
                column: "CategoryId",
                principalTable: "BRAND",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
