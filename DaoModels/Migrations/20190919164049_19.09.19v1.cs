using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _190919v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ask2Id",
                table: "Shipping",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ask2s",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    How_many_keys_total_you_been_given = table.Column<string>(nullable: true),
                    Any_additional_documentation_been_given_after_loading = table.Column<string>(nullable: true),
                    Any_additional_parts_been_given_to_you = table.Column<string>(nullable: true),
                    Car_locked = table.Column<string>(nullable: true),
                    Keys_location = table.Column<string>(nullable: true),
                    Client_friendliness = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ask2s", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_Ask2Id",
                table: "Shipping",
                column: "Ask2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipping_Ask2s_Ask2Id",
                table: "Shipping",
                column: "Ask2Id",
                principalTable: "Ask2s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipping_Ask2s_Ask2Id",
                table: "Shipping");

            migrationBuilder.DropTable(
                name: "Ask2s");

            migrationBuilder.DropIndex(
                name: "IX_Shipping_Ask2Id",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "Ask2Id",
                table: "Shipping");
        }
    }
}
