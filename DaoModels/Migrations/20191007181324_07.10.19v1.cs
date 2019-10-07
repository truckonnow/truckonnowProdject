using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class _071019v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageForUsers_VehiclwInformation_VehiclwInformationId",
                table: "DamageForUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VehiclwInformation_AskFromUsers_AskFromUserid",
                table: "VehiclwInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_VehiclwInformation_askForUserDelyveryMs_askForUserDelyveryMID",
                table: "VehiclwInformation");

            migrationBuilder.DropIndex(
                name: "IX_VehiclwInformation_AskFromUserid",
                table: "VehiclwInformation");

            migrationBuilder.DropIndex(
                name: "IX_VehiclwInformation_askForUserDelyveryMID",
                table: "VehiclwInformation");

            migrationBuilder.DropIndex(
                name: "IX_DamageForUsers_VehiclwInformationId",
                table: "DamageForUsers");

            migrationBuilder.DropColumn(
                name: "AskFromUserid",
                table: "VehiclwInformation");

            migrationBuilder.DropColumn(
                name: "askForUserDelyveryMID",
                table: "VehiclwInformation");

            migrationBuilder.DropColumn(
                name: "VehiclwInformationId",
                table: "DamageForUsers");

            migrationBuilder.AddColumn<int>(
                name: "AskFromUserid",
                table: "Shipping",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "askForUserDelyveryMID",
                table: "Shipping",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingId",
                table: "DamageForUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_AskFromUserid",
                table: "Shipping",
                column: "AskFromUserid");

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_askForUserDelyveryMID",
                table: "Shipping",
                column: "askForUserDelyveryMID");

            migrationBuilder.CreateIndex(
                name: "IX_DamageForUsers_ShippingId",
                table: "DamageForUsers",
                column: "ShippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageForUsers_Shipping_ShippingId",
                table: "DamageForUsers",
                column: "ShippingId",
                principalTable: "Shipping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipping_AskFromUsers_AskFromUserid",
                table: "Shipping",
                column: "AskFromUserid",
                principalTable: "AskFromUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipping_askForUserDelyveryMs_askForUserDelyveryMID",
                table: "Shipping",
                column: "askForUserDelyveryMID",
                principalTable: "askForUserDelyveryMs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageForUsers_Shipping_ShippingId",
                table: "DamageForUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipping_AskFromUsers_AskFromUserid",
                table: "Shipping");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipping_askForUserDelyveryMs_askForUserDelyveryMID",
                table: "Shipping");

            migrationBuilder.DropIndex(
                name: "IX_Shipping_AskFromUserid",
                table: "Shipping");

            migrationBuilder.DropIndex(
                name: "IX_Shipping_askForUserDelyveryMID",
                table: "Shipping");

            migrationBuilder.DropIndex(
                name: "IX_DamageForUsers_ShippingId",
                table: "DamageForUsers");

            migrationBuilder.DropColumn(
                name: "AskFromUserid",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "askForUserDelyveryMID",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "ShippingId",
                table: "DamageForUsers");

            migrationBuilder.AddColumn<int>(
                name: "AskFromUserid",
                table: "VehiclwInformation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "askForUserDelyveryMID",
                table: "VehiclwInformation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehiclwInformationId",
                table: "DamageForUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_AskFromUserid",
                table: "VehiclwInformation",
                column: "AskFromUserid");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_askForUserDelyveryMID",
                table: "VehiclwInformation",
                column: "askForUserDelyveryMID");

            migrationBuilder.CreateIndex(
                name: "IX_DamageForUsers_VehiclwInformationId",
                table: "DamageForUsers",
                column: "VehiclwInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageForUsers_VehiclwInformation_VehiclwInformationId",
                table: "DamageForUsers",
                column: "VehiclwInformationId",
                principalTable: "VehiclwInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclwInformation_AskFromUsers_AskFromUserid",
                table: "VehiclwInformation",
                column: "AskFromUserid",
                principalTable: "AskFromUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehiclwInformation_askForUserDelyveryMs_askForUserDelyveryMID",
                table: "VehiclwInformation",
                column: "askForUserDelyveryMID",
                principalTable: "askForUserDelyveryMs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
