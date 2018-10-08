using System;
using System.Collections.Generic;
using System.Text;

namespace DaoModels.DAO.Models
{
    public class Shipping
    {
        public string Id { get; set; } //Idload
        public string CurrentStatus { get; set; }

        //////////////////////////ORDER INFORMATION

        public string DispatchDate { get; set; }
        public string PickupEstimated { get; set; }
        public string DeliveryEstimated { get; set; }
        public string ShipVia { get; set; }
        public string Condition { get; set; }
        public string PriceListed { get; set; }
        public string TotalPaymentToCarrier { get; set; }
        public string OnDeliveryToCarrier { get; set; }
        public string CompanyOwesCarrier { get; set; }
        public string Description { get; set; }

        /////////////////////////CONTACT INFORMATION

        public string ContactC { get; set; }
        public string PhoneC { get; set; }
        public string FaxC { get; set; }

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

        public string ContactNameP { get; set; }
        public string CoordinatesP { get; set; }
        public string PhoneP { get; set; }

        /////////////////////////DELIVERY INFORMATION

        public string ContactNameD { get; set; }
        public string CoordinatesD { get; set; }
        public string PhoneD { get; set; }

        /////////////////////////DISPATCH INFORMATION

        public string Titl1DI { get; set; }
        public string Titl2DI { get; set; }
        public string Titl3DI { get; set; }
        public string Titl4DI { get; set; }
    }
}
