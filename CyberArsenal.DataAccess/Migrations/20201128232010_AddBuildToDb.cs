using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberArsenal.DataAccess.Migrations
{
    public partial class AddBuildToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Builds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Cpu = table.Column<string>(maxLength: 40, nullable: false),
                    Gpu = table.Column<string>(maxLength: 40, nullable: false),
                    Ram = table.Column<string>(maxLength: 40, nullable: false),
                    Storage = table.Column<string>(maxLength: 40, nullable: false),
                    StorageSecondary = table.Column<string>(maxLength: 40, nullable: true),
                    MotherBoard = table.Column<string>(maxLength: 40, nullable: true),
                    PowerSupply = table.Column<string>(maxLength: 40, nullable: true),
                    Case = table.Column<string>(maxLength: 40, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Builds");
        }
    }
}
