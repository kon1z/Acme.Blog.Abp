using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.Blog.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ArticleContents_ArticleId",
                table: "ArticleContents",
                column: "ArticleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleContents_Articles_ArticleId",
                table: "ArticleContents",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleContents_Articles_ArticleId",
                table: "ArticleContents");

            migrationBuilder.DropIndex(
                name: "IX_ArticleContents_ArticleId",
                table: "ArticleContents");
        }
    }
}
