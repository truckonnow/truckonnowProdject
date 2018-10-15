using DaoModels.DAO;
using DaoModels.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebDispacher.Dao
{
    public class SqlCommadWebDispatch
    {
        private Context context = null;
       
        public SqlCommadWebDispatch()
        {
            context = new Context();
        }

        public bool ExistsDataUser(string login, string password)
        {
            return context.User.FirstOrDefault(u => u.Login == login && u.Password == password) != null;
        }

        public async void SaveKeyDatabays(string login, string password, int key)
        {
            try
            {
                Users users = context.User.FirstOrDefault(u => u.Login == login && u.Password == password);
                users.KeyAuthorized = key.ToString();
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
               
            }
        }

        public bool CheckKeyDb(string key)
        {
            return context.User.FirstOrDefault(u => u.KeyAuthorized == key) != null;
        }

        public List<Shipping> GetShipping(string status)
        {
            List <Shipping> shipping = null;
            shipping = context.Shipping.ToList().FindAll(s => s.CurrentStatus == "NewLoad");
            return shipping;
        }
    }
}
