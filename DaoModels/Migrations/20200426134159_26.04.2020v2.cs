using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _26042020v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateRegistration",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDateInspaction",
                table: "Drivers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRegistration",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LastDateInspaction",
                table: "Drivers");
        }
    }
}
