using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _26042020v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateFired",
                table: "DriverReports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateRegistration",
                table: "DriverReports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFired",
                table: "DriverReports");

            migrationBuilder.DropColumn(
                name: "DateRegistration",
                table: "DriverReports");
        }
    }
}
