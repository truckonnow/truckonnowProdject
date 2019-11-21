using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _211119v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Make = table.Column<string>(nullable: true),
                    HowLong = table.Column<string>(nullable: true),
                    Vin = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Plate = table.Column<string>(nullable: true),
                    Exp = table.Column<string>(nullable: true),
                    AnnualIns = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trailers");
        }
    }
}
