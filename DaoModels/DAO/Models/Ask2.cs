using System.Collections.Generic;

namespace DaoModels.DAO.Models
{
    public class Ask2
    {

        public int Id { get; set; }
        public string How_many_keys_total_you_been_given { get; set; }
        public List<Photo> Any_additional_documentation_been_given_after_loading { get; set; }
        public List<Photo> Any_additional_parts_been_given_to_you { get; set; }
        public string Car_locked { get; set; }
        public string Keys_location { get; set; }
        public string Client_friendliness { get; set; }
    }
}
