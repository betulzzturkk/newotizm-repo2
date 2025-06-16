using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutismEducationPlatform.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddExamDateAndNameToExamResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExamLevel",
                table: "ExamResults",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExamDate",
                table: "ExamResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExamName",
                table: "ExamResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamDate",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "ExamName",
                table: "ExamResults");

            migrationBuilder.AlterColumn<int>(
                name: "ExamLevel",
                table: "ExamResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
