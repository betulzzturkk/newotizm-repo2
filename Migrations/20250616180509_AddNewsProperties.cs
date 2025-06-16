using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutismEducationPlatform.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnnouncement",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "News",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnnouncement",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "News");
        }
    }
}
