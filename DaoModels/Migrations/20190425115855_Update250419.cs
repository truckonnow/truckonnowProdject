using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class Update250419 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InspectionDriverId",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InspectionDriverId",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInspectionDriver",
                table: "Drivers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInspectionToDayDriver",
                table: "Drivers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InspectionDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionDrivers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_InspectionDriverId",
                table: "Photos",
                column: "InspectionDriverId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_InspectionDrivers_InspectionDriverId",
                table: "Photos",
                column: "InspectionDriverId",
                principalTable: "InspectionDrivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_InspectionDrivers_InspectionDriverId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_InspectionDrivers_InspectionDriverId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "InspectionDrivers");

            migrationBuilder.DropIndex(
                name: "IX_Photos_InspectionDriverId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_InspectionDriverId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "InspectionDriverId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "InspectionDriverId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IsInspectionDriver",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IsInspectionToDayDriver",
                table: "Drivers");
        }
    }
}
