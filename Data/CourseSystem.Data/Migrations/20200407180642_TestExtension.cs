using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseSystem.Data.Migrations
{
    public partial class TestExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WrongAnswer1",
                table: "Segments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WrongAnswer2",
                table: "Segments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WrongAnswer3",
                table: "Segments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WrongAnswer1",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "WrongAnswer2",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "WrongAnswer3",
                table: "Segments");
        }
    }
}
