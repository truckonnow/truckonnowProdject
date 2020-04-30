using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _30042020v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountPhoto",
                table: "InspectionDrivers",
                newName: "CountPhotoTruck");

            migrationBuilder.AddColumn<int>(
                name: "CountPhotoTrailer",
                table: "InspectionDrivers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountPhotoTrailer",
                table: "InspectionDrivers");

            migrationBuilder.RenameColumn(
                name: "CountPhotoTruck",
                table: "InspectionDrivers",
                newName: "CountPhoto");
        }
    }
}
