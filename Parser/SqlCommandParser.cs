using DaoModels.DAO;
using DaoModels.DAO.Models;
using Parser.Servise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parser.DAO
{
    public class SqlCommandParser
    {
        private Context context = null;

        public SqlCommandParser()
        {
            context = new Context();
        }

        public async void AddOrder(Shipping shipping)
        {
            bool isCheckOrder = CheckUrlOrder(shipping);
            if(CheckOrder(shipping) && !isCheckOrder)
            {
                shipping.Id += new Random().Next(0, 1000);
            }
            try
            {
                if (!isCheckOrder)
                {
                    LogEr.Logerr("Info", $"Order added to database, Load Id {shipping.Id}", "ParseDataInUrl", DateTime.Now.ToShortTimeString());
                    await context.Shipping.AddAsync(shipping);
                    await context.SaveChangesAsync();
                }
                else
                {
                    LogEr.Logerr("Info", $"Order already exists in the database, Load Id {shipping.Id}", "AddOrder", DateTime.Now.ToShortTimeString());
                }
            }
            catch(Exception e)
            {
                LogEr.Logerr("Info", $"{e.Message}, Load Id {shipping.Id}", "AddOrder", DateTime.Now.ToShortTimeString());
            }
        }

        public bool CheckUrlOrder(Shipping shipping)
        {
            return context.Shipping.FirstOrDefault(s => s.UrlReqvest == shipping.UrlReqvest) != null;
        }

        private bool CheckOrder(Shipping shipping)
        { 
            return context.Shipping.FirstOrDefault(s => s.Id == shipping.Id) != null; 
        }

        public List<Driver> GetDriverInDb()
        {
            return context.Drivers.ToList();
        }

        public async Task RefreshInspectionToDayDriverInDb(int idDriver)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.Id == idDriver);
            if(driver != null)
            {
                LogEr.Logerr("Info1", $"Driver status change \"{driver.Id}\"", "RefreshInspectionToDayDriverInDb", DateTime.Now.ToShortTimeString());
                driver.IsInspectionToDayDriver = false;
                await context.SaveChangesAsync();
            }
        }

        public async Task RefreshInspectionDriverInDb(int idDriver)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.Id == idDriver);
            if (driver != null)
            {
                LogEr.Logerr("Info1", $"Refresh the driver \"{driver.Id}\" to pass inspection", "RefreshInspectionDriverInDb", DateTime.Now.ToShortTimeString());
                driver.IsInspectionDriver = false;
                await context.SaveChangesAsync();
            }
        }
    }
}   