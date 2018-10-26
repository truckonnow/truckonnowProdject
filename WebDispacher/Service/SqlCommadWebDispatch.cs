using DaoModels.DAO;
using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;
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

        private void Init()
        {
            context.Shipping.Load();
            context.VehiclwInformation.Load();
        }

        public bool ExistsDataUser(string login, string password)
        {
            Init();
            return context.User.FirstOrDefault(u => u.Login == login && u.Password == password) != null;
        }

        public async void SaveKeyDatabays(string login, string password, int key)
        {
            Init();
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
            Init();
            return context.User.FirstOrDefault(u => u.KeyAuthorized == key) != null;
        }

        public List<Shipping> GetShippings(string status, int page)
        {
            Init();
            List<Shipping> shipping = null;
            shipping = context.Shipping.ToList().FindAll(s => s.CurrentStatus == status);
            
            if (page != 0)
            {
                try
                {
                    shipping = shipping.GetRange((20 * page) - 20, 20);
                }
                catch(Exception)
                {
                    shipping = shipping.GetRange((20 * page) - 20, shipping.Count % 20);
                }
            }
            else
            {
                try
                {
                    shipping = shipping.GetRange(0, 20);
                }
                catch (Exception)
                {
                    shipping = shipping.GetRange(0, shipping.Count % 20);
                }
            }
            return shipping;
        }

        public Shipping GetShipping(string id)
        {
            Init();
            Shipping shipping = null;
            shipping = context.Shipping.FirstOrDefault(s => s.Id == id);
            return shipping;
        }
    }
}
