namespace DaoModels.DAO.Models
{
    public class DriverReport
    {
        public int Id { get; set; }
        public int IdDriver { get; set; }
        public string Comment { get; set; }
        public string FullName { get; set; }
        public string DriversLicenseNumber { get; set; }
        public string DateRegistration { get; set; }
        public string DateFired { get; set; }
    }
}