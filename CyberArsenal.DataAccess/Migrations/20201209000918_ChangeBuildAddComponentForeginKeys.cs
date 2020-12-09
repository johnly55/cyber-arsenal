using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberArsenal.DataAccess.Migrations
{
    public partial class ChangeBuildAddComponentForeginKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpu",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "Gpu",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "Ram",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "Storage",
                table: "Builds");

            migrationBuilder.AddColumn<int>(
                name: "CpuId",
                table: "Builds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpuName",
                table: "Builds",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Builds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GpuId",
                table: "Builds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GpuName",
                table: "Builds",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Private",
                table: "Builds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RamId",
                table: "Builds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RamName",
                table: "Builds",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Builds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "Builds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageName",
                table: "Builds",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_CpuId",
                table: "Builds",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_GpuId",
                table: "Builds",
                column: "GpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_RamId",
                table: "Builds",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_StorageId",
                table: "Builds",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_Parts_CpuId",
                table: "Builds",
                column: "CpuId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_Parts_GpuId",
                table: "Builds",
                column: "GpuId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_Parts_RamId",
                table: "Builds",
                column: "RamId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_Parts_StorageId",
                table: "Builds",
                column: "StorageId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Builds_Parts_CpuId",
                table: "Builds");

            migrationBuilder.DropForeignKey(
                name: "FK_Builds_Parts_GpuId",
                table: "Builds");

            migrationBuilder.DropForeignKey(
                name: "FK_Builds_Parts_RamId",
                table: "Builds");

            migrationBuilder.DropForeignKey(
                name: "FK_Builds_Parts_StorageId",
                table: "Builds");

            migrationBuilder.DropIndex(
                name: "IX_Builds_CpuId",
                table: "Builds");

            migrationBuilder.DropIndex(
                name: "IX_Builds_GpuId",
                table: "Builds");

            migrationBuilder.DropIndex(
                name: "IX_Builds_RamId",
                table: "Builds");

            migrationBuilder.DropIndex(
                name: "IX_Builds_StorageId",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "CpuId",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "CpuName",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "GpuId",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "GpuName",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "Private",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "RamId",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "RamName",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Builds");

            migrationBuilder.DropColumn(
                name: "StorageName",
                table: "Builds");

            migrationBuilder.AddColumn<string>(
                name: "Cpu",
                table: "Builds",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gpu",
                table: "Builds",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ram",
                table: "Builds",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Storage",
                table: "Builds",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
