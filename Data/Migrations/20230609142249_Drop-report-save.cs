using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Dropreportsave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "saves");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_reports_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_reports_posts_postId",
                        column: x => x.postId,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "saves",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saves", x => x.id);
                    table.ForeignKey(
                        name: "FK_saves_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_saves_posts_postId",
                        column: x => x.postId,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reports_postId",
                table: "reports",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_userId",
                table: "reports",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_saves_postId",
                table: "saves",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_saves_userId",
                table: "saves",
                column: "userId");
        }
    }
}
