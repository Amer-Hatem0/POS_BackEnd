using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIXEL_infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientAddress",
                table: "Advertisements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClientContactEmail",
                table: "Advertisements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClientContactPhone",
                table: "Advertisements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Advertisements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClientUrl",
                table: "Advertisements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ClientWebsite",
                table: "Advertisements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Advertisements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LongDescriptionAr",
                table: "Advertisements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientAddress",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ClientContactEmail",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ClientContactPhone",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ClientUrl",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ClientWebsite",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "LongDescriptionAr",
                table: "Advertisements");
        }
    }
}
