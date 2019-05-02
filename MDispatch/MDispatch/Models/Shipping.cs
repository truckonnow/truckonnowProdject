using System.Collections.Generic;

namespace MDispatch.Models
{
    public class Shipping
    {
        public string Id { get; set; }
        public string idOrder { get; set; }
        public string InternalLoadID { get; set; }
        public string Driver { get; set; }
        public string CurrentStatus { get; set; }
        public string LastUpdated { get; set; }
        public string CDReference { get; set; }
        public string UrlReqvest { get; set; }

        //////////////////////////ORDER INFORMATION

        public string DispatchDate { get; set; }
        public string PickupExactly { get; set; }
        public string DeliveryEstimated { get; set; }
        public string ShipVia { get; set; }
        public string Condition { get; set; }
        public string PriceListed { get; set; }
        public string TotalPaymentToCarrier { get; set; }
        public string OnDeliveryToCarrier { get; set; }
        public string CompanyOwesCarrier { get; set; }
        public string Description { get; set; }
        public string BrokerFee { get; set; }

        /////////////////////////CONTACT INFORMATION

        public string ContactC { get; set; }
        public string PhoneC { get; set; }
        public string FaxC { get; set; }
        public string IccmcC { get; set; }

        /////////////////////////VEHICLE INFORMATION

        public string YearV { get; set; }
        public string MakeV { get; set; }
        public string ModelV { get; set; }
        public string TypeV { get; set; }
        public string ColorV { get; set; }
        public string PlateV { get; set; }
        public string VIN_V { get; set; }
        public string LotV { get; set; }
        public string AdditionalInfoV { get; set; }

        /////////////////////////PICKUP INFORMATION

        public string NameP { get; set; }
        public string ContactNameP { get; set; }
        public string AddresP { get; set; }
        public string StateP { get; set; }
        public string ZipP { get; set; }
        public string CityP { get; set; }
        public string PhoneP { get; set; }
        public string EmailP { get; set; }

        /////////////////////////DELIVERY INFORMATION

        public string NameD { get; set; }
        public string ContactNameD { get; set; }
        public string AddresD { get; set; }
        public string StateD { get; set; }
        public string ZipD { get; set; }
        public string CityD { get; set; }
        public string PhoneD { get; set; }
        public string EmailD { get; set; }

        /////////////////////////DISPATCH INSTRUCTIONS

        public string Titl1DI { get; set; }
        public List<VehiclwInformation> VehiclwInformations { get; set; }

        ////////////////////////////////////////////////

        public Driver Driverr { get; set; }

        /////////////////////////////////////////////
          
        

        public string ColorCurrentStatus
        {
            get
            {
                string color = null;
                if (CurrentStatus == "Assigned")
                {
                    color = "#65CAE1";
                }
                else if (CurrentStatus == "Picked up")
                {
                    color = "#FFBF00";
                }
                else if (CurrentStatus == "Delivered,Billed" || CurrentStatus == "Delivered,Paid")
                {
                    color = "#088A08";
                }
                return color;
            }
        }

        public VehiclwInformation VehiclwInformation1
        {
            get
            {
                VehiclwInformation VehiclwInformation = null;
                if (VehiclwInformations.Count > 0)
                {
                    VehiclwInformation = VehiclwInformations[0];
                }
                else
                {
                    VehiclwInformation = new VehiclwInformation();
                }
                return VehiclwInformation;
            }
        }
        public VehiclwInformation VehiclwInformation2
        {
            get
            {
                VehiclwInformation VehiclwInformation = null;
                if (VehiclwInformations.Count > 1)
                {
                    VehiclwInformation = VehiclwInformations[1];
                }
                else
                {
                    VehiclwInformation = new VehiclwInformation();
                }
                return VehiclwInformation;
            }
        }
        public int CountVehiclw
        {
            get => VehiclwInformations.Count - 2;
        }
        public bool IsVehiclw1
        {
            get => VehiclwInformations[0] != null;
        }
        public bool IsVehiclw2
        {
            get
            {
                bool isVehiclw = false;
                if(VehiclwInformations.Count > 1)
                {
                    isVehiclw = true;
                }
                return isVehiclw;
            }
        }
        public bool IsVehiclw3
        {
            get
            {
                bool isVehiclw = false;
                if (VehiclwInformations.Count > 2)
                {
                    isVehiclw = true;
                }
                return isVehiclw;
            }
        }

        public string IcoStatus
        {
            get
            {
                string ico = "";
                if(CurrentStatus == "Assigned")
                {
                    ico = "newOrder.png";
                }
                else if(CurrentStatus == "Picked up")
                {
                    ico = "pickedUpOrder1.png";
                }
                else if(CurrentStatus == "Delivered,Billed" || CurrentStatus == "Delivered,Paid")
                {
                    ico = "deliveredOrder.png";
                }
                return ico;
            }
        }

        public string IcoViewStatus
        {
            get
            {
                string ico = "";
                if (CurrentStatus == "Assigned")
                {
                    ico = "newViewOrder.png";
                }
                else if (CurrentStatus == "Picked up")
                {
                    ico = "pickedUpViewOrder.png";
                }
                else if (CurrentStatus == "Delivered,Billed" || CurrentStatus == "Delivered,Paid")
                {
                    ico = "deliveredViewOrder.png";
                }
                return ico;
            }
        }
    }
}