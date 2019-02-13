using System.Collections.Generic;

namespace DaoModels.DAO.Models
{
    public class VehiclwInformation
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Plate { get; set; }
        public string VIN { get; set; }
        public string Lot { get; set; }
        public string AdditionalInfo { get; set; }
        public Ask Ask { get; set; }
        public Ask1 Ask1 { get; set; }
        public List<PhotoInspection> PhotoInspections { get; set; }
        public AskFromUser AskFromUser { get; set; }
        public AskDelyvery AskDelyvery { get; set; }
        public AskForUserDelyveryM askForUserDelyveryM { get; set; }
        public Photo Scan { get; set; }
    }
}
