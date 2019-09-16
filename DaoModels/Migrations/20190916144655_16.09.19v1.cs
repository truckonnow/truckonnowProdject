using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _160919v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Ask1s_Ask1Id2",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Ask1s_Ask1Id3",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_Ask1Id2",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_Ask1Id3",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Ask1Id2",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Ask1Id3",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "All_4_wheels_are_correctly_strapped_strapped",
                table: "Ask1s");

            migrationBuilder.DropColumn(
                name: "How_many_keys_total_you_been_given",
                table: "Ask1s");

            migrationBuilder.DropColumn(
                name: "Type_of_strap",
                table: "Ask1s");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ask1Id2",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ask1Id3",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "All_4_wheels_are_correctly_strapped_strapped",
                table: "Ask1s",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "How_many_keys_total_you_been_given",
                table: "Ask1s",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_of_strap",
                table: "Ask1s",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Ask1Id2",
                table: "Photos",
                column: "Ask1Id2");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Ask1Id3",
                table: "Photos",
                column: "Ask1Id3");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Ask1s_Ask1Id2",
                table: "Photos",
                column: "Ask1Id2",
                principalTable: "Ask1s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Ask1s_Ask1Id3",
                table: "Photos",
                column: "Ask1Id3",
                principalTable: "Ask1s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
