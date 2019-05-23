using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class Update220519 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "App_will_ask_for_signature_of_the_client_signatureId",
                table: "AskFromUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AskFromUsers_App_will_ask_for_signature_of_the_client_signatureId",
                table: "AskFromUsers",
                column: "App_will_ask_for_signature_of_the_client_signatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AskFromUsers_Photos_App_will_ask_for_signature_of_the_client_signatureId",
                table: "AskFromUsers",
                column: "App_will_ask_for_signature_of_the_client_signatureId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AskFromUsers_Photos_App_will_ask_for_signature_of_the_client_signatureId",
                table: "AskFromUsers");

            migrationBuilder.DropIndex(
                name: "IX_AskFromUsers_App_will_ask_for_signature_of_the_client_signatureId",
                table: "AskFromUsers");

            migrationBuilder.DropColumn(
                name: "App_will_ask_for_signature_of_the_client_signatureId",
                table: "AskFromUsers");
        }
    }
}
