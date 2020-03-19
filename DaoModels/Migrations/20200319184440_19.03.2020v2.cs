using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _19032020v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "PasswordRecoveries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "PasswordRecoveries");
        }
    }
}
