using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIXEL_infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServicesEn",
                table: "AboutSection",
                newName: "ServicesEnJson");

            migrationBuilder.RenameColumn(
                name: "ServicesAr",
                table: "AboutSection",
                newName: "ServicesArJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServicesEnJson",
                table: "AboutSection",
                newName: "ServicesEn");

            migrationBuilder.RenameColumn(
                name: "ServicesArJson",
                table: "AboutSection",
                newName: "ServicesAr");
        }
    }
}
