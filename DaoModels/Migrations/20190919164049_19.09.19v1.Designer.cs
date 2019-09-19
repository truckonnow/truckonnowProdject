﻿// <auto-generated />
using System;
using DaoModels.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DaoModels.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190919164049_19.09.19v1")]
    partial class _190919v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DaoModels.DAO.Models.Ask", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anyone_Rushing_you_to_perform_the_inspection");

                    b.Property<string>("Enough_space_to_take_pictures");

                    b.Property<string>("How_far_from_trailer");

                    b.Property<string>("Lightbrightness");

                    b.Property<string>("Name_of_the_person_who_gave_you_keys");

                    b.Property<string>("Plate");

                    b.Property<string>("Safe_delivery_location");

                    b.Property<string>("TypeVehicle");

                    b.Property<string>("Vehicle");

                    b.Property<string>("Weather_conditions");

                    b.HasKey("ID");

                    b.ToTable("Asks");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Ask1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Did_someone_help_you_load_it");

                    b.Property<string>("Did_someone_load_the_vehicle_for_you");

                    b.Property<string>("Did_you_Damage_anything_at_the_pick_up");

                    b.Property<string>("Did_you_jumped_the_vehicle_to_start");

                    b.Property<string>("Did_you_notice_any_mechanical_imperfections_wile_loading");

                    b.Property<string>("Exact_Mileage");

                    b.Property<string>("Have_you_used_winch");

                    b.Property<string>("What_method_of_exit_did_you_use");

                    b.HasKey("Id");

                    b.ToTable("Ask1s");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Ask2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Any_additional_documentation_been_given_after_loading");

                    b.Property<string>("Any_additional_parts_been_given_to_you");

                    b.Property<string>("Car_locked");

                    b.Property<string>("Client_friendliness");

                    b.Property<string>("How_many_keys_total_you_been_given");

                    b.Property<string>("Keys_location");

                    b.HasKey("Id");

                    b.ToTable("Ask2s");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.AskDelyvery", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anyone_Rushing_you_to_perform_the_delivery");

                    b.Property<string>("Anyone_helping_you_unload");

                    b.Property<string>("Are_you_giving_any_paperwork_to_a_client");

                    b.Property<string>("Did_client_inspected_the_vehicle");

                    b.Property<string>("Did_someone_else_unloaded_the_vehicle_for_you");

                    b.Property<string>("Did_the_vehicle_starts");

                    b.Property<string>("Did_you_notice_any_imperfections_on_body_wile_vehicle_been_transported");

                    b.Property<string>("Does_the_vehicle_Drives");

                    b.Property<string>("Exact_mileage_after_unloading");

                    b.Property<string>("How_Far_is_the_Trailer_from_Delivery_destination");

                    b.Property<string>("How_did_you_get_inside_of_the_vehicle");

                    b.Property<string>("How_many_keys_are_you_giving_to_client");

                    b.Property<string>("Lightbrightness");

                    b.Property<string>("Time_Of_Delivery");

                    b.Property<string>("Vehicle_Condition_on_delivery");

                    b.Property<string>("Weather_Conditions");

                    b.HasKey("ID");

                    b.ToTable("AskDelyveries");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.AskForUserDelyveryM", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("App_will_ask_for_name_of_the_client_signature");

                    b.Property<int?>("App_will_ask_for_signature_of_the_client_signatureId");

                    b.Property<string>("CountPay");

                    b.Property<string>("EmailPay");

                    b.Property<string>("Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up");

                    b.Property<string>("NamePaymment");

                    b.Property<int?>("PhotoPayId");

                    b.Property<int?>("VideoRecordId");

                    b.Property<string>("What_form_of_payment_are_you_using_to_pay_for_transportation");

                    b.HasKey("ID");

                    b.HasIndex("App_will_ask_for_signature_of_the_client_signatureId");

                    b.HasIndex("PhotoPayId");

                    b.HasIndex("VideoRecordId");

                    b.ToTable("askForUserDelyveryMs");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.AskFromUser", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Any_titles_been_given_to_driver");

                    b.Property<int?>("App_will_ask_for_signature_of_the_client_signatureId");

                    b.Property<string>("CountPay");

                    b.Property<string>("EmailPay");

                    b.Property<string>("How_many_keys_are_driver_been_given");

                    b.Property<string>("NamePaymment");

                    b.Property<int?>("PhotoPayId");

                    b.Property<int?>("VideoRecordId");

                    b.Property<string>("What_form_of_payment_are_you_using_to_pay_for_transportation");

                    b.Property<string>("Your_Full_Name");

                    b.Property<string>("Your_phone");

                    b.HasKey("id");

                    b.HasIndex("App_will_ask_for_signature_of_the_client_signatureId");

                    b.HasIndex("PhotoPayId");

                    b.HasIndex("VideoRecordId");

                    b.ToTable("AskFromUsers");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("ID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Damage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullNameDamage");

                    b.Property<int>("HeightDamage");

                    b.Property<int>("IndexDamage");

                    b.Property<string>("IndexImageVech");

                    b.Property<int?>("PhotoInspectionId");

                    b.Property<string>("TypeCurrentStatus");

                    b.Property<string>("TypeDamage");

                    b.Property<string>("TypePrefDamage");

                    b.Property<int>("WidthDamage");

                    b.Property<double>("XInterest");

                    b.Property<double>("YInterest");

                    b.HasKey("ID");

                    b.HasIndex("PhotoInspectionId");

                    b.ToTable("Damages");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.DamageForUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullNameDamage");

                    b.Property<int>("HeightDamage");

                    b.Property<int>("IndexDamage");

                    b.Property<string>("TypeCurrentStatus");

                    b.Property<string>("TypeDamage");

                    b.Property<string>("TypePrefDamage");

                    b.Property<int?>("VehiclwInformationId");

                    b.Property<int>("WidthDamage");

                    b.Property<double>("XInterest");

                    b.Property<double>("YInterest");

                    b.HasKey("ID");

                    b.HasIndex("VehiclwInformationId");

                    b.ToTable("DamageForUsers");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DriversLicenseNumber");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("EmailOrLogin");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsInspectionDriver");

                    b.Property<bool>("IsInspectionToDayDriver");

                    b.Property<string>("IssuingState_Province");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Token");

                    b.Property<string>("TokenShope");

                    b.Property<string>("TrailerCapacity");

                    b.Property<int?>("geolocationsID");

                    b.HasKey("Id");

                    b.HasIndex("geolocationsID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Feedback", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("How_Are_You_Satisfied_With_Service");

                    b.Property<string>("How_did_the_driver_perform");

                    b.Property<string>("Would_You_Like_To_Get_An_notification_If_We_Have_Any_Promotion");

                    b.Property<string>("Would_You_Use_Our_Company_Again");

                    b.HasKey("id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Geolocations", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.HasKey("ID");

                    b.ToTable("geolocations");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.InspectionDriver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountPhoto");

                    b.Property<string>("Date");

                    b.Property<int?>("DriverId");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("InspectionDrivers");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Ask1Id");

                    b.Property<int?>("Ask1Id1");

                    b.Property<int?>("AskForUserDelyveryMID");

                    b.Property<int?>("AskID");

                    b.Property<string>("Base64");

                    b.Property<double>("Height");

                    b.Property<int?>("InspectionDriverId");

                    b.Property<int?>("PhotoInspectionId");

                    b.Property<double>("Width");

                    b.Property<string>("path");

                    b.HasKey("Id");

                    b.HasIndex("Ask1Id");

                    b.HasIndex("Ask1Id1");

                    b.HasIndex("AskForUserDelyveryMID");

                    b.HasIndex("AskID");

                    b.HasIndex("InspectionDriverId");

                    b.HasIndex("PhotoInspectionId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.PhotoInspection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrentStatusPhoto");

                    b.Property<int>("IndexPhoto");

                    b.Property<int?>("VehiclwInformationId");

                    b.HasKey("Id");

                    b.HasIndex("VehiclwInformationId");

                    b.ToTable("PhotoInspections");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Shipping", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddresD");

                    b.Property<string>("AddresP");

                    b.Property<int?>("Ask2Id");

                    b.Property<string>("BrokerFee");

                    b.Property<string>("CDReference");

                    b.Property<string>("CityD");

                    b.Property<string>("CityP");

                    b.Property<string>("CompanyOwesCarrier");

                    b.Property<string>("Condition");

                    b.Property<string>("ContactC");

                    b.Property<string>("ContactNameD");

                    b.Property<string>("ContactNameP");

                    b.Property<string>("CurrentStatus");

                    b.Property<string>("DataCancelOrder");

                    b.Property<string>("DataFullArcive");

                    b.Property<string>("DataPaid");

                    b.Property<string>("DeliveryEstimated");

                    b.Property<string>("Description");

                    b.Property<string>("DispatchDate");

                    b.Property<string>("Driver");

                    b.Property<int?>("DriverrId");

                    b.Property<string>("EmailD");

                    b.Property<string>("EmailP");

                    b.Property<string>("FaxC");

                    b.Property<string>("IccmcC");

                    b.Property<string>("InternalLoadID");

                    b.Property<string>("LastUpdated");

                    b.Property<string>("NameD");

                    b.Property<string>("NameP");

                    b.Property<string>("OnDeliveryToCarrier");

                    b.Property<string>("PhoneC");

                    b.Property<string>("PhoneD");

                    b.Property<string>("PhoneP");

                    b.Property<string>("PickupExactly");

                    b.Property<string>("PriceListed");

                    b.Property<string>("ShipVia");

                    b.Property<string>("StateD");

                    b.Property<string>("StateP");

                    b.Property<string>("Titl1DI");

                    b.Property<string>("TotalPaymentToCarrier");

                    b.Property<string>("UrlReqvest");

                    b.Property<string>("ZipD");

                    b.Property<string>("ZipP");

                    b.Property<string>("idOrder");

                    b.HasKey("Id");

                    b.HasIndex("Ask2Id");

                    b.HasIndex("DriverrId");

                    b.ToTable("Shipping");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KeyAuthorized");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.VehiclwInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo");

                    b.Property<int?>("Ask1Id");

                    b.Property<int?>("AskDelyveryID");

                    b.Property<int?>("AskFromUserid");

                    b.Property<int?>("AskID");

                    b.Property<string>("Color");

                    b.Property<string>("Lot");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<string>("Plate");

                    b.Property<int?>("ScanId");

                    b.Property<string>("ShippingId");

                    b.Property<string>("Type");

                    b.Property<string>("VIN");

                    b.Property<string>("Year");

                    b.Property<int?>("askForUserDelyveryMID");

                    b.HasKey("Id");

                    b.HasIndex("Ask1Id");

                    b.HasIndex("AskDelyveryID");

                    b.HasIndex("AskFromUserid");

                    b.HasIndex("AskID");

                    b.HasIndex("ScanId");

                    b.HasIndex("ShippingId");

                    b.HasIndex("askForUserDelyveryMID");

                    b.ToTable("VehiclwInformation");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("VideoBase64");

                    b.Property<string>("path");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.AskForUserDelyveryM", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.Photo", "App_will_ask_for_signature_of_the_client_signature")
                        .WithMany()
                        .HasForeignKey("App_will_ask_for_signature_of_the_client_signatureId");

                    b.HasOne("DaoModels.DAO.Models.Photo", "PhotoPay")
                        .WithMany()
                        .HasForeignKey("PhotoPayId");

                    b.HasOne("DaoModels.DAO.Models.Video", "VideoRecord")
                        .WithMany()
                        .HasForeignKey("VideoRecordId");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.AskFromUser", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.Photo", "App_will_ask_for_signature_of_the_client_signature")
                        .WithMany()
                        .HasForeignKey("App_will_ask_for_signature_of_the_client_signatureId");

                    b.HasOne("DaoModels.DAO.Models.Photo", "PhotoPay")
                        .WithMany()
                        .HasForeignKey("PhotoPayId");

                    b.HasOne("DaoModels.DAO.Models.Video", "VideoRecord")
                        .WithMany()
                        .HasForeignKey("VideoRecordId");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Damage", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.PhotoInspection")
                        .WithMany("Damages")
                        .HasForeignKey("PhotoInspectionId");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.DamageForUser", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.VehiclwInformation")
                        .WithMany("DamageForUsers")
                        .HasForeignKey("VehiclwInformationId");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Driver", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.Geolocations", "geolocations")
                        .WithMany()
                        .HasForeignKey("geolocationsID");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.InspectionDriver", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.Driver")
                        .WithMany("InspectionDrivers")
                        .HasForeignKey("DriverId");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Photo", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.Ask1")
                        .WithMany("App_will_force_driver_to_take_pictures_of_each_strap")
                        .HasForeignKey("Ask1Id");

                    b.HasOne("DaoModels.DAO.Models.Ask1")
                        .WithMany("Photo_after_loading_in_the_truck")
                        .HasForeignKey("Ask1Id1");

                    b.HasOne("DaoModels.DAO.Models.AskForUserDelyveryM")
                        .WithMany("Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up_photo")
                        .HasForeignKey("AskForUserDelyveryMID");

                    b.HasOne("DaoModels.DAO.Models.Ask")
                        .WithMany("Any_personal_or_additional_items_with_or_in_vehicle")
                        .HasForeignKey("AskID");

                    b.HasOne("DaoModels.DAO.Models.InspectionDriver")
                        .WithMany("PhotosTruck")
                        .HasForeignKey("InspectionDriverId");

                    b.HasOne("DaoModels.DAO.Models.PhotoInspection")
                        .WithMany("Photos")
                        .HasForeignKey("PhotoInspectionId");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.PhotoInspection", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.VehiclwInformation")
                        .WithMany("PhotoInspections")
                        .HasForeignKey("VehiclwInformationId");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.Shipping", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.Ask2", "Ask2")
                        .WithMany()
                        .HasForeignKey("Ask2Id");

                    b.HasOne("DaoModels.DAO.Models.Driver", "Driverr")
                        .WithMany()
                        .HasForeignKey("DriverrId");
                });

            modelBuilder.Entity("DaoModels.DAO.Models.VehiclwInformation", b =>
                {
                    b.HasOne("DaoModels.DAO.Models.Ask1", "Ask1")
                        .WithMany()
                        .HasForeignKey("Ask1Id");

                    b.HasOne("DaoModels.DAO.Models.AskDelyvery", "AskDelyvery")
                        .WithMany()
                        .HasForeignKey("AskDelyveryID");

                    b.HasOne("DaoModels.DAO.Models.AskFromUser", "AskFromUser")
                        .WithMany()
                        .HasForeignKey("AskFromUserid");

                    b.HasOne("DaoModels.DAO.Models.Ask", "Ask")
                        .WithMany()
                        .HasForeignKey("AskID");

                    b.HasOne("DaoModels.DAO.Models.Photo", "Scan")
                        .WithMany()
                        .HasForeignKey("ScanId");

                    b.HasOne("DaoModels.DAO.Models.Shipping")
                        .WithMany("VehiclwInformations")
                        .HasForeignKey("ShippingId");

                    b.HasOne("DaoModels.DAO.Models.AskForUserDelyveryM", "askForUserDelyveryM")
                        .WithMany()
                        .HasForeignKey("askForUserDelyveryMID");
                });
#pragma warning restore 612, 618
        }
    }
}
