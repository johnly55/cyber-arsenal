using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberArsenal.DataAccess.Migrations
{
    public partial class ChangePartAddReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Reference",
                table: "Parts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Parts");
        }
    }
}
