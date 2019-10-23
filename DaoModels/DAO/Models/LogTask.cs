using System.Collections.Generic;

namespace DaoModels.DAO.Models
{
    public class LogTask
    {
        public int Id { get; set; }
        public List<TaskLoad> TaskLoads { get; set; }
    }
}