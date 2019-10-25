namespace DaoModels.DAO.Models
{
    public class TaskLoad
    {
        public int Id { get; set; }
        public string NameMethod { get; set; }
        public string OptionalParameter { get; set; }
        public string IdDriver { get; set; }
        public byte[] Array { get; set; }
    }
}