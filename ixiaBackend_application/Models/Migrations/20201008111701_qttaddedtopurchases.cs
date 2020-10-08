using Microsoft.EntityFrameworkCore.Migrations;

namespace ixiaBackend_application.Migrations
{
    public partial class qttaddedtopurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchases");
        }
    }
}
