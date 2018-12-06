﻿using System.Collections.Generic;

namespace MDispatch.Models
{
    public class VehiclwInformation
    {
        public string Id { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public string VIN { get; set; }
        public string Lot { get; set; }
        public string AdditionalInfo { get; set; }
        public List<Photo> Photos { get; set; }
    }
}