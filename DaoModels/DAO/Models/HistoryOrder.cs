using System.ComponentModel.DataAnnotations.Schema;

namespace DaoModels.DAO.Models
{
    public class HistoryOrder
    {
        public int Id { get; set; }
        public int IdOreder { get; set; }
        public int IdUser { get; set; }
        public int IdDriver { get; set; }
        public int IdVech { get; set; }
        public int IdConmpany { get; set; }
        [NotMapped]
        public string Action { get; set; }
        public string DateAction { get; set; }
        public string TypeAction { get; set; }
    }
}