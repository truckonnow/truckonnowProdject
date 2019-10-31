using System.Collections.Generic;

namespace DaoModels.DAO.Models
{
    public class InspectionDriver
    {
        public int Id { get; set; }
        public int CountPhoto { get; set; }
        public virtual List<PhotoDriver> PhotosTruck { get; set; }
        public string Date { get; set; }

        public InspectionDriver()
        {

        }
    }
}