using System;
using System.Collections.Generic;
using System.Text;

namespace DaoModels.DAO.Models
{
    public class Driver
    {
        public string Id { get; set; }
        public string EmailOrLogin { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
