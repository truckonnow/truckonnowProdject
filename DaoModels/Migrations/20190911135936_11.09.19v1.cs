using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _110919v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Asks_AskID1",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_AskID1",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "AskID1",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Does_The_vehicle_Drives",
                table: "Asks");

            migrationBuilder.RenameColumn(
                name: "How_far_is_the_vehicle_from_Trailer_Aprox_in_ft",
                table: "Asks",
                newName: "Safe_delivery_location");

            migrationBuilder.RenameColumn(
                name: "Exact_Mileage",
                table: "Asks",
                newName: "Name_of_the_person_who_gave_you_keys");

            migrationBuilder.RenameColumn(
                name: "Enough_distance_to_take_pictures_at_least_4ft",
                table: "Asks",
                newName: "How_far_from_trailer");

            migrationBuilder.RenameColumn(
                name: "Does_The_vehicle_Starts",
                table: "Asks",
                newName: "Enough_space_to_take_pictures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Safe_delivery_location",
                table: "Asks",
                newName: "How_far_is_the_vehicle_from_Trailer_Aprox_in_ft");

            migrationBuilder.RenameColumn(
                name: "Name_of_the_person_who_gave_you_keys",
                table: "Asks",
                newName: "Exact_Mileage");

            migrationBuilder.RenameColumn(
                name: "How_far_from_trailer",
                table: "Asks",
                newName: "Enough_distance_to_take_pictures_at_least_4ft");

            migrationBuilder.RenameColumn(
                name: "Enough_space_to_take_pictures",
                table: "Asks",
                newName: "Does_The_vehicle_Starts");

            migrationBuilder.AddColumn<int>(
                name: "AskID1",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Does_The_vehicle_Drives",
                table: "Asks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AskID1",
                table: "Photos",
                column: "AskID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Asks_AskID1",
                table: "Photos",
                column: "AskID1",
                principalTable: "Asks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
