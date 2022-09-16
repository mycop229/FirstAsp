using Microsoft.EntityFrameworkCore.Migrations;

namespace Tor.Migrations
{
    public partial class idntknow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Summa",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summa",
                table: "AspNetUsers");
        }
    }
}
