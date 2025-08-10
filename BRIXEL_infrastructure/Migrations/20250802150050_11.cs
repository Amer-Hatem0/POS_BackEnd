using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIXEL_infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Question",
                table: "FAQs",
                newName: "QuestionEn");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "FAQs",
                newName: "QuestionAr");

            migrationBuilder.AddColumn<string>(
                name: "AnswerAr",
                table: "FAQs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AnswerEn",
                table: "FAQs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerAr",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "AnswerEn",
                table: "FAQs");

            migrationBuilder.RenameColumn(
                name: "QuestionEn",
                table: "FAQs",
                newName: "Question");

            migrationBuilder.RenameColumn(
                name: "QuestionAr",
                table: "FAQs",
                newName: "Answer");
        }
    }
}
