using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _211119v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Exp",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Satet",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vin",
                table: "Trucks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Yera",
                table: "Trucks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exp",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Satet",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Vin",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Yera",
                table: "Trucks");
        }
    }
}
