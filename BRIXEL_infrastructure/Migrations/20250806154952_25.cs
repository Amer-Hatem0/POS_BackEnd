using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIXEL_infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AboutSection",
                newName: "TitleEn");

            migrationBuilder.RenameColumn(
                name: "Services",
                table: "AboutSection",
                newName: "TitleAr");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AboutSection",
                newName: "ServicesEn");

            migrationBuilder.RenameColumn(
                name: "BackgroundImageUrl",
                table: "AboutSection",
                newName: "MainImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                table: "AboutSection",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "AboutSection",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ServicesAr",
                table: "AboutSection",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                table: "AboutSection");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "AboutSection");

            migrationBuilder.DropColumn(
                name: "ServicesAr",
                table: "AboutSection");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "AboutSection",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleAr",
                table: "AboutSection",
                newName: "Services");

            migrationBuilder.RenameColumn(
                name: "ServicesEn",
                table: "AboutSection",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "MainImageUrl",
                table: "AboutSection",
                newName: "BackgroundImageUrl");
        }
    }
}
