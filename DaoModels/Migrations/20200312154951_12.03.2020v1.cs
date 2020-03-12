using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _12032020v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Any_additional_documentation_been_given_after_loading",
                table: "Ask2s");

            migrationBuilder.DropColumn(
                name: "Any_additional_parts_been_given_to_you",
                table: "Ask2s");

            migrationBuilder.AddColumn<int>(
                name: "Ask2Id",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ask2Id1",
                table: "Photos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Ask2Id",
                table: "Photos",
                column: "Ask2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Ask2Id1",
                table: "Photos",
                column: "Ask2Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Ask2s_Ask2Id",
                table: "Photos",
                column: "Ask2Id",
                principalTable: "Ask2s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Ask2s_Ask2Id1",
                table: "Photos",
                column: "Ask2Id1",
                principalTable: "Ask2s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Ask2s_Ask2Id",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Ask2s_Ask2Id1",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_Ask2Id",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_Ask2Id1",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Ask2Id",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Ask2Id1",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "Any_additional_documentation_been_given_after_loading",
                table: "Ask2s",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Any_additional_parts_been_given_to_you",
                table: "Ask2s",
                nullable: true);
        }
    }
}
