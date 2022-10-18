using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH4_Nhom20.Data.Migrations
{
    public partial class ReviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMMENTS");

            migrationBuilder.CreateTable(
                name: "REVIEWS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CameraId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEWS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REVIEWS_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REVIEWS_CAMERA_CameraId",
                        column: x => x.CameraId,
                        principalTable: "CAMERA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_REVIEWS_CameraId",
                table: "REVIEWS",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEWS_UserId",
                table: "REVIEWS",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REVIEWS");

            migrationBuilder.CreateTable(
                name: "COMMENTS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CameraId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMMENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COMMENTS_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMMENTS_CAMERA_CameraId",
                        column: x => x.CameraId,
                        principalTable: "CAMERA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_CameraId",
                table: "COMMENTS",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTS_UserId",
                table: "COMMENTS",
                column: "UserId");
        }
    }
}
