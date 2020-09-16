using Microsoft.EntityFrameworkCore.Migrations;

namespace ixiaBackend_application.Migrations
{
    public partial class favoriteseditedpurchaseidchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Purchases",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Purchases",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Favorites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Favorites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                columns: new[] { "UserId", "ProductId", "CountryId", "CurrencyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_CountryId",
                table: "Favorites",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_CurrencyId",
                table: "Favorites",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Countries_CountryId",
                table: "Favorites",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Currencies_CurrencyId",
                table: "Favorites",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Countries_CountryId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Currencies_CurrencyId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_CountryId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_CurrencyId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Favorites");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                columns: new[] { "UserId", "ProductId", "CountryId", "CurrencyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                columns: new[] { "UserId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
