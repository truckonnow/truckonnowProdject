using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaoModel.DAO.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string KeyAuthorized { get; set; }
        
        public Users() { }

        public Users(string Login = null, string Password = null, string KeyAuthorized = null)
        {

        }
    }
}
