using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDispacher.Dao.Models
{
    public class Users
    {
        public int id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        //Конструктор для БД
        public Users() { }

    }
}
