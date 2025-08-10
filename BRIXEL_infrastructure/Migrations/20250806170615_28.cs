using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIXEL_infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "WhyChooseUsSection",
                newName: "SubtitleEn");

            migrationBuilder.RenameColumn(
                name: "MainTitle",
                table: "WhyChooseUsSection",
                newName: "SubtitleAr");

            migrationBuilder.RenameColumn(
                name: "HighlightTitle",
                table: "WhyChooseUsSection",
                newName: "MainTitleEn");

            migrationBuilder.RenameColumn(
                name: "HighlightDescription",
                table: "WhyChooseUsSection",
                newName: "MainTitleAr");

            migrationBuilder.RenameColumn(
                name: "BulletPoints",
                table: "WhyChooseUsSection",
                newName: "HighlightTitleEn");

            migrationBuilder.AddColumn<string>(
                name: "BulletPointsAr",
                table: "WhyChooseUsSection",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BulletPointsEn",
                table: "WhyChooseUsSection",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HighlightDescriptionAr",
                table: "WhyChooseUsSection",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HighlightDescriptionEn",
                table: "WhyChooseUsSection",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HighlightTitleAr",
                table: "WhyChooseUsSection",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BulletPointsAr",
                table: "WhyChooseUsSection");

            migrationBuilder.DropColumn(
                name: "BulletPointsEn",
                table: "WhyChooseUsSection");

            migrationBuilder.DropColumn(
                name: "HighlightDescriptionAr",
                table: "WhyChooseUsSection");

            migrationBuilder.DropColumn(
                name: "HighlightDescriptionEn",
                table: "WhyChooseUsSection");

            migrationBuilder.DropColumn(
                name: "HighlightTitleAr",
                table: "WhyChooseUsSection");

            migrationBuilder.RenameColumn(
                name: "SubtitleEn",
                table: "WhyChooseUsSection",
                newName: "Subtitle");

            migrationBuilder.RenameColumn(
                name: "SubtitleAr",
                table: "WhyChooseUsSection",
                newName: "MainTitle");

            migrationBuilder.RenameColumn(
                name: "MainTitleEn",
                table: "WhyChooseUsSection",
                newName: "HighlightTitle");

            migrationBuilder.RenameColumn(
                name: "MainTitleAr",
                table: "WhyChooseUsSection",
                newName: "HighlightDescription");

            migrationBuilder.RenameColumn(
                name: "HighlightTitleEn",
                table: "WhyChooseUsSection",
                newName: "BulletPoints");
        }
    }
}
