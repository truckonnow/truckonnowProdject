using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _240919v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "How_many_keys_are_you_giving_to_client",
                table: "AskDelyveries",
                newName: "Vehicle_parked_in_the_safe_location");

            migrationBuilder.RenameColumn(
                name: "Did_client_inspected_the_vehicle",
                table: "AskDelyveries",
                newName: "Truck_on_emergency_brake");

            migrationBuilder.RenameColumn(
                name: "Are_you_giving_any_paperwork_to_a_client",
                table: "AskDelyveries",
                newName: "Truck_locked");

            migrationBuilder.AddColumn<int>(
                name: "AskDelyveryID",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "After_inspecting_the_car_press_the_confirm_button",
                table: "AskDelyveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "All_locks_on_the_trailer",
                table: "AskDelyveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Did_you_meet_the_client",
                table: "AskDelyveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Safe_delivery_location_Truck_and_trailer_parked_on",
                table: "AskDelyveries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AskDelyveryID",
                table: "Photos",
                column: "AskDelyveryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AskDelyveries_AskDelyveryID",
                table: "Photos",
                column: "AskDelyveryID",
                principalTable: "AskDelyveries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AskDelyveries_AskDelyveryID",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_AskDelyveryID",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "AskDelyveryID",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "After_inspecting_the_car_press_the_confirm_button",
                table: "AskDelyveries");

            migrationBuilder.DropColumn(
                name: "All_locks_on_the_trailer",
                table: "AskDelyveries");

            migrationBuilder.DropColumn(
                name: "Did_you_meet_the_client",
                table: "AskDelyveries");

            migrationBuilder.DropColumn(
                name: "Safe_delivery_location_Truck_and_trailer_parked_on",
                table: "AskDelyveries");

            migrationBuilder.RenameColumn(
                name: "Vehicle_parked_in_the_safe_location",
                table: "AskDelyveries",
                newName: "How_many_keys_are_you_giving_to_client");

            migrationBuilder.RenameColumn(
                name: "Truck_on_emergency_brake",
                table: "AskDelyveries",
                newName: "Did_client_inspected_the_vehicle");

            migrationBuilder.RenameColumn(
                name: "Truck_locked",
                table: "AskDelyveries",
                newName: "Are_you_giving_any_paperwork_to_a_client");
        }
    }
}
