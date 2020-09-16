using Microsoft.EntityFrameworkCore.Migrations;

namespace ixiaBackend_application.Migrations
{
    public partial class tablePurchaseEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Purchases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                columns: new[] { "UserId", "ProductId", "CountryId", "CurrencyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CountryId",
                table: "Purchases",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Countries_CountryId",
                table: "Purchases",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Countries_CountryId",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_CountryId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Purchases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                columns: new[] { "UserId", "ProductId" });
        }
    }
}
