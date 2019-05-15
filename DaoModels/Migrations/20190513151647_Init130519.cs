using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaoModels.Migrations
{
    public partial class Init130519 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ask1s",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Exact_Mileage = table.Column<string>(nullable: true),
                    Did_you_notice_any_mechanical_imperfections_wile_loading = table.Column<string>(nullable: true),
                    Did_someone_help_you_load_it = table.Column<string>(nullable: true),
                    Did_someone_load_the_vehicle_for_you = table.Column<string>(nullable: true),
                    Did_you_Damage_anything_at_the_pick_up = table.Column<string>(nullable: true),
                    What_method_of_exit_did_you_use = table.Column<string>(nullable: true),
                    Did_you_jumped_the_vehicle_to_start = table.Column<string>(nullable: true),
                    Have_you_used_winch = table.Column<string>(nullable: true),
                    How_many_keys_total_you_been_given = table.Column<string>(nullable: true),
                    All_4_wheels_are_correctly_strapped_strapped = table.Column<string>(nullable: true),
                    Type_of_strap = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ask1s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AskDelyveries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Time_Of_Delivery = table.Column<string>(nullable: true),
                    Lightbrightness = table.Column<string>(nullable: true),
                    Vehicle_Condition_on_delivery = table.Column<string>(nullable: true),
                    Weather_Conditions = table.Column<string>(nullable: true),
                    How_did_you_get_inside_of_the_vehicle = table.Column<string>(nullable: true),
                    Did_the_vehicle_starts = table.Column<string>(nullable: true),
                    Does_the_vehicle_Drives = table.Column<string>(nullable: true),
                    Anyone_Rushing_you_to_perform_the_delivery = table.Column<string>(nullable: true),
                    How_Far_is_the_Trailer_from_Delivery_destination = table.Column<string>(nullable: true),
                    Exact_mileage_after_unloading = table.Column<string>(nullable: true),
                    Anyone_helping_you_unload = table.Column<string>(nullable: true),
                    Did_someone_else_unloaded_the_vehicle_for_you = table.Column<string>(nullable: true),
                    Did_you_notice_any_imperfections_on_body_wile_vehicle_been_transported = table.Column<string>(nullable: true),
                    How_many_keys_are_you_giving_to_client = table.Column<string>(nullable: true),
                    Are_you_giving_any_paperwork_to_a_client = table.Column<string>(nullable: true),
                    Did_client_inspected_the_vehicle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AskDelyveries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Asks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Lightbrightness = table.Column<string>(nullable: true),
                    Vehicle = table.Column<string>(nullable: true),
                    Enough_distance_to_take_pictures_at_least_4ft = table.Column<string>(nullable: true),
                    Weather_conditions = table.Column<string>(nullable: true),
                    Does_The_vehicle_Starts = table.Column<string>(nullable: true),
                    Does_The_vehicle_Drives = table.Column<string>(nullable: true),
                    Anyone_Rushing_you_to_perform_the_inspection = table.Column<string>(nullable: true),
                    How_far_is_the_vehicle_from_Trailer_Aprox_in_ft = table.Column<string>(nullable: true),
                    Plate = table.Column<string>(nullable: true),
                    Exact_Mileage = table.Column<string>(nullable: true),
                    TypeVehicle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    How_Are_You_Satisfied_With_Service = table.Column<string>(nullable: true),
                    Would_You_Use_Our_Company_Again = table.Column<string>(nullable: true),
                    Would_You_Like_To_Get_An_notification_If_We_Have_Any_Promotion = table.Column<string>(nullable: true),
                    How_did_the_driver_perform = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "geolocations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_geolocations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    KeyAuthorized = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailOrLogin = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    TrailerCapacity = table.Column<string>(nullable: true),
                    DriversLicenseNumber = table.Column<string>(nullable: true),
                    IssuingState_Province = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    TokenShope = table.Column<string>(nullable: true),
                    IsInspectionDriver = table.Column<bool>(nullable: false),
                    IsInspectionToDayDriver = table.Column<bool>(nullable: false),
                    geolocationsID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_geolocations_geolocationsID",
                        column: x => x.geolocationsID,
                        principalTable: "geolocations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InspectionDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountPhoto = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    DriverId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionDrivers_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    idOrder = table.Column<string>(nullable: true),
                    InternalLoadID = table.Column<string>(nullable: true),
                    Driver = table.Column<string>(nullable: true),
                    CurrentStatus = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<string>(nullable: true),
                    CDReference = table.Column<string>(nullable: true),
                    UrlReqvest = table.Column<string>(nullable: true),
                    DispatchDate = table.Column<string>(nullable: true),
                    PickupExactly = table.Column<string>(nullable: true),
                    DeliveryEstimated = table.Column<string>(nullable: true),
                    ShipVia = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(nullable: true),
                    PriceListed = table.Column<string>(nullable: true),
                    TotalPaymentToCarrier = table.Column<string>(nullable: true),
                    OnDeliveryToCarrier = table.Column<string>(nullable: true),
                    CompanyOwesCarrier = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BrokerFee = table.Column<string>(nullable: true),
                    ContactC = table.Column<string>(nullable: true),
                    PhoneC = table.Column<string>(nullable: true),
                    FaxC = table.Column<string>(nullable: true),
                    IccmcC = table.Column<string>(nullable: true),
                    NameP = table.Column<string>(nullable: true),
                    ContactNameP = table.Column<string>(nullable: true),
                    AddresP = table.Column<string>(nullable: true),
                    StateP = table.Column<string>(nullable: true),
                    ZipP = table.Column<string>(nullable: true),
                    CityP = table.Column<string>(nullable: true),
                    PhoneP = table.Column<string>(nullable: true),
                    EmailP = table.Column<string>(nullable: true),
                    NameD = table.Column<string>(nullable: true),
                    ContactNameD = table.Column<string>(nullable: true),
                    AddresD = table.Column<string>(nullable: true),
                    StateD = table.Column<string>(nullable: true),
                    ZipD = table.Column<string>(nullable: true),
                    CityD = table.Column<string>(nullable: true),
                    PhoneD = table.Column<string>(nullable: true),
                    EmailD = table.Column<string>(nullable: true),
                    Titl1DI = table.Column<string>(nullable: true),
                    DriverrId = table.Column<int>(nullable: true),
                    DataPaid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipping_Drivers_DriverrId",
                        column: x => x.DriverrId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    path = table.Column<string>(nullable: true),
                    Base64 = table.Column<string>(nullable: true),
                    Ask1Id = table.Column<int>(nullable: true),
                    Ask1Id1 = table.Column<int>(nullable: true),
                    Ask1Id2 = table.Column<int>(nullable: true),
                    Ask1Id3 = table.Column<int>(nullable: true),
                    AskForUserDelyveryMID = table.Column<int>(nullable: true),
                    AskID = table.Column<int>(nullable: true),
                    AskID1 = table.Column<int>(nullable: true),
                    InspectionDriverId = table.Column<int>(nullable: true),
                    PhotoInspectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Ask1s_Ask1Id",
                        column: x => x.Ask1Id,
                        principalTable: "Ask1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Ask1s_Ask1Id1",
                        column: x => x.Ask1Id1,
                        principalTable: "Ask1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Ask1s_Ask1Id2",
                        column: x => x.Ask1Id2,
                        principalTable: "Ask1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Ask1s_Ask1Id3",
                        column: x => x.Ask1Id3,
                        principalTable: "Ask1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Asks_AskID",
                        column: x => x.AskID,
                        principalTable: "Asks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Asks_AskID1",
                        column: x => x.AskID1,
                        principalTable: "Asks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_InspectionDrivers_InspectionDriverId",
                        column: x => x.InspectionDriverId,
                        principalTable: "InspectionDrivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "askForUserDelyveryMs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up = table.Column<string>(nullable: true),
                    What_form_of_payment_are_you_using_to_pay_for_transportation = table.Column<string>(nullable: true),
                    CountPay = table.Column<string>(nullable: true),
                    PhotoPayId = table.Column<int>(nullable: true),
                    EmailPay = table.Column<string>(nullable: true),
                    NamePaymment = table.Column<string>(nullable: true),
                    App_will_ask_for_name_of_the_client_signature = table.Column<string>(nullable: true),
                    App_will_ask_for_signature_of_the_client_signatureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_askForUserDelyveryMs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_askForUserDelyveryMs_Photos_App_will_ask_for_signature_of_the_client_signatureId",
                        column: x => x.App_will_ask_for_signature_of_the_client_signatureId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_askForUserDelyveryMs_Photos_PhotoPayId",
                        column: x => x.PhotoPayId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AskFromUsers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Your_Full_Name = table.Column<string>(nullable: true),
                    Your_phone = table.Column<string>(nullable: true),
                    How_many_keys_are_driver_been_given = table.Column<string>(nullable: true),
                    Any_titles_been_given_to_driver = table.Column<string>(nullable: true),
                    What_form_of_payment_are_you_using_to_pay_for_transportation = table.Column<string>(nullable: true),
                    CountPay = table.Column<string>(nullable: true),
                    PhotoPayId = table.Column<int>(nullable: true),
                    EmailPay = table.Column<string>(nullable: true),
                    NamePaymment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AskFromUsers", x => x.id);
                    table.ForeignKey(
                        name: "FK_AskFromUsers_Photos_PhotoPayId",
                        column: x => x.PhotoPayId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehiclwInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<string>(nullable: true),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Plate = table.Column<string>(nullable: true),
                    VIN = table.Column<string>(nullable: true),
                    Lot = table.Column<string>(nullable: true),
                    AdditionalInfo = table.Column<string>(nullable: true),
                    AskID = table.Column<int>(nullable: true),
                    Ask1Id = table.Column<int>(nullable: true),
                    AskFromUserid = table.Column<int>(nullable: true),
                    AskDelyveryID = table.Column<int>(nullable: true),
                    askForUserDelyveryMID = table.Column<int>(nullable: true),
                    ScanId = table.Column<int>(nullable: true),
                    ShippingId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclwInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclwInformation_Ask1s_Ask1Id",
                        column: x => x.Ask1Id,
                        principalTable: "Ask1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehiclwInformation_AskDelyveries_AskDelyveryID",
                        column: x => x.AskDelyveryID,
                        principalTable: "AskDelyveries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehiclwInformation_AskFromUsers_AskFromUserid",
                        column: x => x.AskFromUserid,
                        principalTable: "AskFromUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehiclwInformation_Asks_AskID",
                        column: x => x.AskID,
                        principalTable: "Asks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehiclwInformation_Photos_ScanId",
                        column: x => x.ScanId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehiclwInformation_Shipping_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shipping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehiclwInformation_askForUserDelyveryMs_askForUserDelyveryMID",
                        column: x => x.askForUserDelyveryMID,
                        principalTable: "askForUserDelyveryMs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DamageForUsers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypePrefDamage = table.Column<string>(nullable: true),
                    TypeDamage = table.Column<string>(nullable: true),
                    TypeCurrentStatus = table.Column<string>(nullable: true),
                    IndexDamage = table.Column<int>(nullable: false),
                    FullNameDamage = table.Column<string>(nullable: true),
                    XInterest = table.Column<double>(nullable: false),
                    YInterest = table.Column<double>(nullable: false),
                    HeightDamage = table.Column<int>(nullable: false),
                    WidthDamage = table.Column<int>(nullable: false),
                    VehiclwInformationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageForUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DamageForUsers_VehiclwInformation_VehiclwInformationId",
                        column: x => x.VehiclwInformationId,
                        principalTable: "VehiclwInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoInspections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndexPhoto = table.Column<int>(nullable: false),
                    CurrentStatusPhoto = table.Column<string>(nullable: true),
                    VehiclwInformationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoInspections_VehiclwInformation_VehiclwInformationId",
                        column: x => x.VehiclwInformationId,
                        principalTable: "VehiclwInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Damages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndexImageVech = table.Column<string>(nullable: true),
                    TypePrefDamage = table.Column<string>(nullable: true),
                    TypeDamage = table.Column<string>(nullable: true),
                    TypeCurrentStatus = table.Column<string>(nullable: true),
                    IndexDamage = table.Column<int>(nullable: false),
                    FullNameDamage = table.Column<string>(nullable: true),
                    XInterest = table.Column<double>(nullable: false),
                    YInterest = table.Column<double>(nullable: false),
                    HeightDamage = table.Column<int>(nullable: false),
                    WidthDamage = table.Column<int>(nullable: false),
                    PhotoInspectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Damages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Damages_PhotoInspections_PhotoInspectionId",
                        column: x => x.PhotoInspectionId,
                        principalTable: "PhotoInspections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_askForUserDelyveryMs_App_will_ask_for_signature_of_the_client_signatureId",
                table: "askForUserDelyveryMs",
                column: "App_will_ask_for_signature_of_the_client_signatureId");

            migrationBuilder.CreateIndex(
                name: "IX_askForUserDelyveryMs_PhotoPayId",
                table: "askForUserDelyveryMs",
                column: "PhotoPayId");

            migrationBuilder.CreateIndex(
                name: "IX_AskFromUsers_PhotoPayId",
                table: "AskFromUsers",
                column: "PhotoPayId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageForUsers_VehiclwInformationId",
                table: "DamageForUsers",
                column: "VehiclwInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Damages_PhotoInspectionId",
                table: "Damages",
                column: "PhotoInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_geolocationsID",
                table: "Drivers",
                column: "geolocationsID");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionDrivers_DriverId",
                table: "InspectionDrivers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoInspections_VehiclwInformationId",
                table: "PhotoInspections",
                column: "VehiclwInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Ask1Id",
                table: "Photos",
                column: "Ask1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Ask1Id1",
                table: "Photos",
                column: "Ask1Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Ask1Id2",
                table: "Photos",
                column: "Ask1Id2");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Ask1Id3",
                table: "Photos",
                column: "Ask1Id3");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AskForUserDelyveryMID",
                table: "Photos",
                column: "AskForUserDelyveryMID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AskID",
                table: "Photos",
                column: "AskID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AskID1",
                table: "Photos",
                column: "AskID1");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_InspectionDriverId",
                table: "Photos",
                column: "InspectionDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotoInspectionId",
                table: "Photos",
                column: "PhotoInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_DriverrId",
                table: "Shipping",
                column: "DriverrId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_Ask1Id",
                table: "VehiclwInformation",
                column: "Ask1Id");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_AskDelyveryID",
                table: "VehiclwInformation",
                column: "AskDelyveryID");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_AskFromUserid",
                table: "VehiclwInformation",
                column: "AskFromUserid");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_AskID",
                table: "VehiclwInformation",
                column: "AskID");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_ScanId",
                table: "VehiclwInformation",
                column: "ScanId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_ShippingId",
                table: "VehiclwInformation",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclwInformation_askForUserDelyveryMID",
                table: "VehiclwInformation",
                column: "askForUserDelyveryMID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_PhotoInspections_PhotoInspectionId",
                table: "Photos",
                column: "PhotoInspectionId",
                principalTable: "PhotoInspections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_askForUserDelyveryMs_AskForUserDelyveryMID",
                table: "Photos",
                column: "AskForUserDelyveryMID",
                principalTable: "askForUserDelyveryMs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_askForUserDelyveryMs_Photos_App_will_ask_for_signature_of_the_client_signatureId",
                table: "askForUserDelyveryMs");

            migrationBuilder.DropForeignKey(
                name: "FK_askForUserDelyveryMs_Photos_PhotoPayId",
                table: "askForUserDelyveryMs");

            migrationBuilder.DropForeignKey(
                name: "FK_AskFromUsers_Photos_PhotoPayId",
                table: "AskFromUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VehiclwInformation_Photos_ScanId",
                table: "VehiclwInformation");

            migrationBuilder.DropTable(
                name: "DamageForUsers");

            migrationBuilder.DropTable(
                name: "Damages");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "InspectionDrivers");

            migrationBuilder.DropTable(
                name: "PhotoInspections");

            migrationBuilder.DropTable(
                name: "VehiclwInformation");

            migrationBuilder.DropTable(
                name: "Ask1s");

            migrationBuilder.DropTable(
                name: "AskDelyveries");

            migrationBuilder.DropTable(
                name: "AskFromUsers");

            migrationBuilder.DropTable(
                name: "Asks");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "askForUserDelyveryMs");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "geolocations");
        }
    }
}
