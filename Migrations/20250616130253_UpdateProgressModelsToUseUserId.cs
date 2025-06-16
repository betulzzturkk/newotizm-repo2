using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutismEducationPlatform.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProgressModelsToUseUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Children_ChildId",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseProgress_Children_ChildId",
                table: "CourseProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_Children_ChildId",
                table: "Progresses");

            migrationBuilder.DropIndex(
                name: "IX_Progresses_ChildId",
                table: "Progresses");

            migrationBuilder.DropIndex(
                name: "IX_CourseProgress_ChildId",
                table: "CourseProgress");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_ChildId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "TaleTitle",
                table: "TaleProgress");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "CourseProgress");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "ActivityLogs");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Tales",
                newName: "VideoUrl");

            migrationBuilder.RenameColumn(
                name: "Progress",
                table: "TaleProgress",
                newName: "ProgressPercentage");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Tales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Progresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseProgress",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ActivityLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_UserId",
                table: "Progresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgress_UserId",
                table: "CourseProgress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                table: "ActivityLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseProgress_AspNetUsers_UserId",
                table: "CourseProgress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_AspNetUsers_UserId",
                table: "Progresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseProgress_AspNetUsers_UserId",
                table: "CourseProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_AspNetUsers_UserId",
                table: "Progresses");

            migrationBuilder.DropIndex(
                name: "IX_Progresses_UserId",
                table: "Progresses");

            migrationBuilder.DropIndex(
                name: "IX_CourseProgress_UserId",
                table: "CourseProgress");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tales");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Tales");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseProgress");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ActivityLogs");

            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "Tales",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "ProgressPercentage",
                table: "TaleProgress",
                newName: "Progress");

            migrationBuilder.AddColumn<string>(
                name: "TaleTitle",
                table: "TaleProgress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "Progresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "CourseProgress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "ActivityLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_ChildId",
                table: "Progresses",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgress_ChildId",
                table: "CourseProgress",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ChildId",
                table: "ActivityLogs",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Children_ChildId",
                table: "ActivityLogs",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseProgress_Children_ChildId",
                table: "CourseProgress",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_Children_ChildId",
                table: "Progresses",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
