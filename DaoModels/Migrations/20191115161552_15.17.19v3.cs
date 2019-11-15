using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _151719v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "Shipping");

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "AskFromUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "askForUserDelyveryMs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "AskFromUsers");

            migrationBuilder.DropColumn(
                name: "IsProblem",
                table: "askForUserDelyveryMs");

            migrationBuilder.AddColumn<bool>(
                name: "IsProblem",
                table: "Shipping",
                nullable: false,
                defaultValue: false);
        }
    }
}
