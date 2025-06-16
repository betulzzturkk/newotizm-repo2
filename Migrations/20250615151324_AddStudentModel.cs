using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutismEducationPlatform.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Children_ChildId",
                table: "Progress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progress_Courses_CourseId",
                table: "Progress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progress",
                table: "Progress");

            migrationBuilder.RenameTable(
                name: "Progress",
                newName: "Progresses");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_CourseId",
                table: "Progresses",
                newName: "IX_Progresses_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Progress_ChildId",
                table: "Progresses",
                newName: "IX_Progresses_ChildId");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Progresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progresses",
                table: "Progresses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Hobbies = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    InstructorId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_StudentId",
                table: "Progresses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstructorId",
                table: "Students",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_Children_ChildId",
                table: "Progresses",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_Courses_CourseId",
                table: "Progresses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_Students_StudentId",
                table: "Progresses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_Children_ChildId",
                table: "Progresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_Courses_CourseId",
                table: "Progresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_Students_StudentId",
                table: "Progresses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progresses",
                table: "Progresses");

            migrationBuilder.DropIndex(
                name: "IX_Progresses_StudentId",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Progresses");

            migrationBuilder.RenameTable(
                name: "Progresses",
                newName: "Progress");

            migrationBuilder.RenameIndex(
                name: "IX_Progresses_CourseId",
                table: "Progress",
                newName: "IX_Progress_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Progresses_ChildId",
                table: "Progress",
                newName: "IX_Progress_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progress",
                table: "Progress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Children_ChildId",
                table: "Progress",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progress_Courses_CourseId",
                table: "Progress",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
