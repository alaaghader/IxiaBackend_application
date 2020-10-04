using Microsoft.EntityFrameworkCore.Migrations;

namespace ixiaBackend_application.Migrations
{
    public partial class photourladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Types",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Sub_Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Sub_Categories");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Categories");
        }
    }
}
