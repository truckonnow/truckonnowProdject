using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class update07052019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_InspectionDrivers_InspectionDriverId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_InspectionDriverId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "InspectionDriverId",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "InspectionDrivers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InspectionDrivers_DriverId",
                table: "InspectionDrivers",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionDrivers_Drivers_DriverId",
                table: "InspectionDrivers",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionDrivers_Drivers_DriverId",
                table: "InspectionDrivers");

            migrationBuilder.DropIndex(
                name: "IX_InspectionDrivers_DriverId",
                table: "InspectionDrivers");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "InspectionDrivers");

            migrationBuilder.AddColumn<int>(
                name: "InspectionDriverId",
                table: "Drivers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_InspectionDriverId",
                table: "Drivers",
                column: "InspectionDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_InspectionDrivers_InspectionDriverId",
                table: "Drivers",
                column: "InspectionDriverId",
                principalTable: "InspectionDrivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
