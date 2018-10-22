using DaoModels.DAO;
using DaoModels.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMobaileServise.Servise
{
    public class SqlCommandApiMobile
    {
        private Context context = null;

        public SqlCommandApiMobile()
        {
            context = new Context();
            //Driver driver = new Driver();
            //driver.Password = "Admin";
            //driver.EmailOrLogin = "Admin";
            //context.Drivers.Add(driver);
            //context.SaveChanges();
        }


        public bool CheckEmailAndPsw(string email, string password)
        {
            return context.Drivers.FirstOrDefault(d => d.EmailOrLogin == email && d.Password == password) != null ? true : false;
        }
    }
}
