using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QueryTest.Migrations
{
    public partial class Blogger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BloggerUserId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bloogers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloogers", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BloggerUserId",
                table: "Blogs",
                column: "BloggerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Bloogers_BloggerUserId",
                table: "Blogs",
                column: "BloggerUserId",
                principalTable: "Bloogers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Bloogers_BloggerUserId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "Bloogers");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BloggerUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BloggerUserId",
                table: "Blogs");
        }
    }
}
