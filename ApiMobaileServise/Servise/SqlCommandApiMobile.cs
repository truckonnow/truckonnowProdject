using DaoModels.DAO;
using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            context.Photos.Load();
            context.Asks.Load();
            context.Ask1s.Load();
            context.PhotoInspections.Load();
            context.User.Load();
            context.AskFromUsers.Load();
        }

        public async void SavePhotoInspectionInDb(string idVe, PhotoInspection photoInspection)
        {
            Init();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id.ToString() == idVe);
            if(vehiclwInformation.PhotoInspections == null)
            {
                vehiclwInformation.PhotoInspections = new List<PhotoInspection>();
            }
            vehiclwInformation.PhotoInspections.Add(photoInspection);
            await context.SaveChangesAsync();
        }

        public void SaveAskInDb(string idve, Ask ask)
        {
            Init();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if(vehiclwInformation.Ask == null)
            {
                vehiclwInformation.Ask = new Ask();
            }
            vehiclwInformation.Ask = ask;
            context.SaveChangesAsync();
        }

        public void SaveAsk1InDb(string idve, Ask1 ask1)
        {
            Init();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if (vehiclwInformation.Ask1 == null)
            {
                vehiclwInformation.Ask1 = new Ask1();
            }
            vehiclwInformation.Ask1 = ask1;
            context.SaveChangesAsync();
        }

        public void SaveAskFromUserInDb(string idve, AskFromUser askFromUser)
        {
            Init();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if (vehiclwInformation.Ask1 == null)
            {
                vehiclwInformation.AskFromUser = new AskFromUser();
            }
            vehiclwInformation.AskFromUser = askFromUser;
            context.SaveChangesAsync();
        }

        public bool CheckEmailAndPsw(string email, string password)
        {
            Init();
            return context.Drivers.FirstOrDefault(d => d.EmailAddress == email && d.Password == password) != null ? true : false;
        }

        public async void SavePikedUpInDb(string id, string idOrder, string name, string contactName, string address, string city, string state, string zip, string phone, string email)
        {
            Shipping shipping = context.Shipping.FirstOrDefault(s => s.Id == id);
            if(shipping != null)
            {
                shipping.idOrder = idOrder;
                shipping.NameP = name;
                shipping.ContactNameP = contactName;
                shipping.AddresP = address;
                shipping.CityP = city;
                shipping.StateP = state;
                shipping.ZipP = zip;
                shipping.PhoneP = phone;
                shipping.EmailP = email;
                await context.SaveChangesAsync();
            }
        }

        public async void SaveDeliveryInDb(string id, string idOrder, string name, string contactName, string address, string city, string state, string zip, string phone, string email)
        {
            Shipping shipping = context.Shipping.FirstOrDefault(s => s.Id == id);
            if (shipping != null)
            {
                shipping.idOrder = idOrder;
                shipping.NameD = name;
                shipping.ContactNameD = contactName;
                shipping.AddresD = address;
                shipping.CityD = city;
                shipping.StateD = state;
                shipping.ZipD = zip;
                shipping.PhoneD = phone;
                shipping.EmailD = email;
                await context.SaveChangesAsync();
            }
        }

        public async void SavePaymentsInDb(string id, string payment, string paymentTeams)
        {
            Shipping shipping = context.Shipping.FirstOrDefault(s => s.Id == id);
            if (shipping != null)
            {
                shipping.PriceListed = payment;
                shipping.OnDeliveryToCarrier = paymentTeams;
                await context.SaveChangesAsync();
            }
        }

        public async void SaveToken(string email, string password, string token)
        {
            Init();
            Driver driver = context.Drivers.FirstOrDefault(d => d.EmailAddress == email && d.Password == password);
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