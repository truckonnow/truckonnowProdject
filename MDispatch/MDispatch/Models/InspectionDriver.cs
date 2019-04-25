using System.Collections.Generic;

namespace MDispatch.Models
{
    public class InspectionDriver
    {
        public int Id { get; set; }
        public List<Photo> PhotosTruck { get; set; }
        public string Date { get; set; }
    }
}