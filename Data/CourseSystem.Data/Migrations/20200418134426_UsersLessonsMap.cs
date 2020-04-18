using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseSystem.Data.Migrations
{
    public partial class UsersLessonsMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersLessons",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LessonId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLessons", x => new { x.UserId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_UsersLessons_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersLessons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersLessons_LessonId",
                table: "UsersLessons",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersLessons");
        }
    }
}
