using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class Update19062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoRecordId",
                table: "AskFromUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoRecordId",
                table: "askForUserDelyveryMs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    path = table.Column<string>(nullable: true),
                    VideoBase64 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AskFromUsers_VideoRecordId",
                table: "AskFromUsers",
                column: "VideoRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_askForUserDelyveryMs_VideoRecordId",
                table: "askForUserDelyveryMs",
                column: "VideoRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_askForUserDelyveryMs_Videos_VideoRecordId",
                table: "askForUserDelyveryMs",
                column: "VideoRecordId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AskFromUsers_Videos_VideoRecordId",
                table: "AskFromUsers",
                column: "VideoRecordId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_askForUserDelyveryMs_Videos_VideoRecordId",
                table: "askForUserDelyveryMs");

            migrationBuilder.DropForeignKey(
                name: "FK_AskFromUsers_Videos_VideoRecordId",
                table: "AskFromUsers");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_AskFromUsers_VideoRecordId",
                table: "AskFromUsers");

            migrationBuilder.DropIndex(
                name: "IX_askForUserDelyveryMs_VideoRecordId",
                table: "askForUserDelyveryMs");

            migrationBuilder.DropColumn(
                name: "VideoRecordId",
                table: "AskFromUsers");

            migrationBuilder.DropColumn(
                name: "VideoRecordId",
                table: "askForUserDelyveryMs");
        }
    }
}
