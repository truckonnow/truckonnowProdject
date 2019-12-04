using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _041219v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFired",
                table: "Drivers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IsFired",
                table: "Drivers");
        }
    }
}
