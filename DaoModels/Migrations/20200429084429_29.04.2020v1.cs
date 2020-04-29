using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _29042020v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Trailers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Trailers");
        }
    }
}
