using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDispacher.DAO.Models
{
    public class Shipping
    {
        public string Id { get; set; } //Idload
        public string TypeShipping { get; set; }
        public string Payment { get; set; }

        ///////////////////////////////////////////////

        public string VIN { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string LotNumber { get; set; }
        public string Price { get; set; }

        public string OriginDesk { get; set; }
        public string OriginContact { get; set; }
        public string OriginPhone { get; set; }
        public string OriginPickupDate { get; set; }
        public string BuyerNumber { get; set; }

        /////////////////////////////////////////////

        public string DestinationnDesk { get; set; }
        public string DestinationContact { get; set; }
        public string DestinationPhone { get; set; }
        public string DestinationDeliveryDate { get; set; }


        ///////////////////////////////////////////////////////////
        //Shipper
        public string Description { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
