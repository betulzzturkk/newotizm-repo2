using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutismEducationPlatform.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMannerProgressUserFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MannerProgress",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_MannerProgress_UserId",
                table: "MannerProgress",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MannerProgress_AspNetUsers_UserId",
                table: "MannerProgress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MannerProgress_AspNetUsers_UserId",
                table: "MannerProgress");

            migrationBuilder.DropIndex(
                name: "IX_MannerProgress_UserId",
                table: "MannerProgress");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MannerProgress",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);
        }
    }
}
