using System.Collections.Generic;

namespace ApiMobaileServise.Servise.ModelInspertionDriver.Truck
{
    public class PickupFourWheel : ITransportVehicle
    {
        public int CountPhoto { get; set; } = 21;
        public string Type { get; set; } = "PickupFourWheel";
        public bool IsNextInspection { get; set; } = true;
        public List<string> NamePatern { get; set; }
        public string PlateTruck { get; set; }
        public string PlateTraler { get; set; }

        public PickupFourWheel()
        {
            NamePatern = new List<string>()
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "11",
                "12",
                "13",
                "14",
                "15",
                "16",
                "18",
                "19",
                "21",
            };
        }
    }
}