using Microsoft.EntityFrameworkCore.Migrations;

namespace Tor.Migrations
{
    public partial class CreateTablePromoCode2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PromoCode",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PromoCode");
        }
    }
}
