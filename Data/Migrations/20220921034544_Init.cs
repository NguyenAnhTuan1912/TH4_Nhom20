using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH4_Nhom20.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BRAND",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRAND", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CAMERA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMERA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAMERA_BRAND_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BRAND",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAMERA_CategoryId",
                table: "CAMERA",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAMERA");

            migrationBuilder.DropTable(
                name: "BRAND");
        }
    }
}
