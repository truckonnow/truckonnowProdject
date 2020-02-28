using System.Collections.Generic;

namespace MDispatch.Models
{
    public class InspectionDriver
    {
        public int Id { get; set; }
        public List<Photo> PhotosTruck { get; set; }
        public string Date { get; set; }
        public int IdITruck { get; set; }
        public int IdITrailer { get; set; }
    }
}