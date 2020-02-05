using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _05022020v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathDoc",
                table: "Trucks");

            migrationBuilder.CreateTable(
                name: "DocumentTruckAndTrailers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdTr = table.Column<int>(nullable: false),
                    DocPath = table.Column<string>(nullable: true),
                    TypeDoc = table.Column<string>(nullable: true),
                    TypeTr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTruckAndTrailers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentTruckAndTrailers");

            migrationBuilder.AddColumn<string>(
                name: "PathDoc",
                table: "Trucks",
                nullable: true);
        }
    }
}
