using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _26042020v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Base64",
                table: "PhotoDrivers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "PhotoDrivers",
                nullable: true);
        }
    }
}
