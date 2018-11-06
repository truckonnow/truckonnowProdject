using DaoModels.DAO;
using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;
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
        }

        private void Init()
        {
            context.Shipping.Load();
            context.VehiclwInformation.Load();
            context.Drivers.Load();
        }

        public bool CheckEmailAndPsw(string email, string password)
        {
            Init();
            return context.Drivers.FirstOrDefault(d => d.EmailOrLogin == email && d.Password == password) != null ? true : false;
        }

        public async void SaveToken(string email, string password, string token)
        {
            Init();
            Driver driver = context.Drivers.FirstOrDefault(d => d.EmailOrLogin == email && d.Password == password);
            driver.Token = token;
            await context.SaveChangesAsync();
        }

        public bool CheckToken(string token)
        {
            return context.Drivers.FirstOrDefault(d => d.Token == token) != null ? true : false;
        }

        public List<Shipping> GetOrdersForToken(string token, string status)
        {
            Init();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            List<Shipping> shippings = context.Shipping.ToList().FindAll(s => s.Driverr != null && s.Driverr.Id == driver.Id);
            if(shippings == null)
            {
                return new List<Shipping>();
            }
            return shippings.FindAll(s => s.CurrentStatus == status);
        }
    }
}