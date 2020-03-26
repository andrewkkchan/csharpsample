using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QueryTest.Migrations
{
    public partial class Blogger_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Bloogers_BloggerUserId",
                table: "Blogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bloogers",
                table: "Bloogers");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BloggerUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bloogers");

            migrationBuilder.DropColumn(
                name: "BloggerUserId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BloggerId",
                table: "Bloogers",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bloogers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloggerId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bloogers",
                table: "Bloogers",
                column: "BloggerId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BloggerId",
                table: "Blogs",
                column: "BloggerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Bloogers_BloggerId",
                table: "Blogs",
                column: "BloggerId",
                principalTable: "Bloogers",
                principalColumn: "BloggerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Bloogers_BloggerId",
                table: "Blogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bloogers",
                table: "Bloogers");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BloggerId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BloggerId",
                table: "Bloogers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bloogers");

            migrationBuilder.DropColumn(
                name: "BloggerId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bloogers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "BloggerUserId",
                table: "Blogs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bloogers",
                table: "Bloogers",
                column: "UserId");

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
    }
}
