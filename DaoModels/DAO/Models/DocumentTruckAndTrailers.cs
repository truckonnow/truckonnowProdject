namespace DaoModels.DAO.Models
{
    public class DocumentTruckAndTrailers
    {
        public int Id { get; set; }
        public int IdTr { get; set; }
        public string NameDoc { get; set; }
        public string DocPath { get; set; }
        public string TypeDoc { get; set; }
        public string TypeTr { get; set; }
    }
}