using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberArsenal.DataAccess.Migrations
{
    public partial class ChangeBuildToAddApplicationUserForeginKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Builds",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_ApplicationUserId",
                table: "Builds",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_AspNetUsers_ApplicationUserId",
                table: "Builds",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Builds_AspNetUsers_ApplicationUserId",
                table: "Builds");

            migrationBuilder.DropIndex(
                name: "IX_Builds_ApplicationUserId",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Builds");
        }
    }
}
