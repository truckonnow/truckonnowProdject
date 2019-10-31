using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _301019v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_InspectionDrivers_InspectionDriverId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_InspectionDriverId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "InspectionDriverId",
                table: "Photos");

            migrationBuilder.CreateTable(
                name: "PhotoDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdInspaction = table.Column<int>(nullable: false),
                    path = table.Column<string>(nullable: true),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Base64 = table.Column<string>(nullable: true),
                    InspectionDriverId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoDrivers_InspectionDrivers_InspectionDriverId",
                        column: x => x.InspectionDriverId,
                        principalTable: "InspectionDrivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoDrivers_InspectionDriverId",
                table: "PhotoDrivers",
                column: "InspectionDriverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoDrivers");

            migrationBuilder.AddColumn<int>(
                name: "InspectionDriverId",
                table: "Photos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_InspectionDriverId",
                table: "Photos",
                column: "InspectionDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_InspectionDrivers_InspectionDriverId",
                table: "Photos",
                column: "InspectionDriverId",
                principalTable: "InspectionDrivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
