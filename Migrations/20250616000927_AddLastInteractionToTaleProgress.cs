using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutismEducationPlatform.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddLastInteractionToTaleProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrafficSignId",
                table: "TrafficSignProgress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaleId",
                table: "TaleProgress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrafficSigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficSigns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaleProgress_TaleId",
                table: "TaleProgress",
                column: "TaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaleProgress_Tales_TaleId",
                table: "TaleProgress",
                column: "TaleId",
                principalTable: "Tales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaleProgress_Tales_TaleId",
                table: "TaleProgress");

            migrationBuilder.DropTable(
                name: "Tales");

            migrationBuilder.DropTable(
                name: "TrafficSigns");

            migrationBuilder.DropIndex(
                name: "IX_TaleProgress_TaleId",
                table: "TaleProgress");

            migrationBuilder.DropColumn(
                name: "TrafficSignId",
                table: "TrafficSignProgress");

            migrationBuilder.DropColumn(
                name: "TaleId",
                table: "TaleProgress");
        }
    }
}
