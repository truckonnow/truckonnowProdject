using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class Update11072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataFullArcive",
                table: "Shipping",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataMobileArcive",
                table: "Shipping",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFullArcive",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "DataMobileArcive",
                table: "Shipping");
        }
    }
}
