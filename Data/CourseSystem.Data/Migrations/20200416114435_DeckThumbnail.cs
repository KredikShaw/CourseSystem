using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseSystem.Data.Migrations
{
    public partial class DeckThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Decks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Decks");
        }
    }
}
