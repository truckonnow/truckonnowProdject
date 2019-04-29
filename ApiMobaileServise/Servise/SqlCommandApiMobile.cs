using DaoModels.DAO;
using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async void SaveGPSLocationData(string token, Geolocations geolocations)
        {
            context.Drivers.Load();
            context.geolocations.Load();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            driver.geolocations = geolocations;
            await context.SaveChangesAsync();
        }

        public async void SaveTokenStoreinDb(string token, string tokenStore)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            driver.TokenShope = tokenStore;
            await context.SaveChangesAsync();
        }

        public async Task<VehiclwInformation> GetVehiclwInformationAndSaveDamageForUser(string idVech, List<DamageForUser> damageForUsers)
        {
            context.VehiclwInformation.Load();
            context.DamageForUsers.Load();
            context.Asks.Load();
            context.Photos.Load();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idVech));
            if (vehiclwInformation.DamageForUsers == null)
            {
                vehiclwInformation.DamageForUsers = new List<DamageForUser>();
            }
            vehiclwInformation.DamageForUsers.AddRange(damageForUsers);
            await context.SaveChangesAsync();
            return vehiclwInformation;
        }

        public async Task<VehiclwInformation> SavePhotoInspectionInDb(string idVe, PhotoInspection photoInspection)
        {
            context.VehiclwInformation.Load();
            context.Asks.Load();
            context.PhotoInspections.Load();
            context.Damages.Load();
            context.Photos.Load();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id.ToString() == idVe);
            if (vehiclwInformation.PhotoInspections == null)
            {
                vehiclwInformation.PhotoInspections = new List<PhotoInspection>();
            }
            if (photoInspection.IndexPhoto == 1 && photoInspection.CurrentStatusPhoto == "PikedUp")
            {
                Photo photo = new Photo();
                photo.path = $"../Photo/{vehiclwInformation.Id}/scan.png";
                photo.Base64 = JsonConvert.SerializeObject(File.ReadAllBytes($"../Scans/scan{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}.png"));
                vehiclwInformation.Scan = photo;
            }
            vehiclwInformation.PhotoInspections.Add(photoInspection);
            await context.SaveChangesAsync();
            return vehiclwInformation;
        }

        public Shipping GetShippingInDb(string idShip)
        {
            context.PhotoInspections.Load();
            context.Shipping.Load();
            context.VehiclwInformation.Load();
            context.Damages.Load();
            context.Photos.Load();
            return context.Shipping.FirstOrDefault(v => v.Id.ToString() == idShip);
        }

        public async void SaveAskInDb(string idve, Ask ask)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if (vehiclwInformation.Ask == null)
            {
                vehiclwInformation.Ask = new Ask();
            }
            vehiclwInformation.Ask = ask;
            await context.SaveChangesAsync();
        }

        public async void SavePayMethotInDb(string idVech, string payMethod, string countPay)
        {
            context.VehiclwInformation.Load();
            context.AskFromUsers.Load();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idVech));
            if (vehiclwInformation.AskFromUser == null)
            {
                vehiclwInformation.AskFromUser = new AskFromUser();
            }
            vehiclwInformation.AskFromUser.What_form_of_payment_are_you_using_to_pay_for_transportation = payMethod;
            vehiclwInformation.AskFromUser.CountPay = countPay;
            await context.SaveChangesAsync();
        }

        public async void SavePayInDb(string idVech, int type, Photo photo)
        {
            context.VehiclwInformation.Load();
            context.askForUserDelyveryMs.Load();
            context.AskFromUsers.Load();
            context.Photos.Load();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idVech));
            if (type == 1 && vehiclwInformation.AskFromUser != null)
            {
                vehiclwInformation.AskFromUser.PhotoPay = photo;
            }
            else if (type == 2 && vehiclwInformation != null && vehiclwInformation.askForUserDelyveryM != null)
            {
                vehiclwInformation.askForUserDelyveryM.PhotoPay = photo;
            }
            await context.SaveChangesAsync();
        }

        public async void SaveFeedBackInDb(Feedback feedback)
        {
            context.Feedbacks.Add(feedback);
            await context.SaveChangesAsync();
        }

        public async void ReCurentStatus(string idShip, string status)
        {
            Shipping shipping = context.Shipping.FirstOrDefault(s => s.Id == idShip);
            shipping.CurrentStatus = status;
            if (status == "Delivered,Billed")
            {
                shipping.DataPaid = DateTime.Now.AddDays(Convert.ToInt32(shipping.TotalPaymentToCarrier.Replace(" days", ""))).ToString();
            }
            await context.SaveChangesAsync();
        }

        public string GerShopTokenForShipping(string idOrder)
        {
            context.Drivers.Load();
            Shipping shipping = context.Shipping.FirstOrDefault<Shipping>(d => d.Id == idOrder);
            return shipping.Driverr.TokenShope;
        }

        public async void SaveSigPikedUpInDb(string idve, Photo sig)
        {
            context.Shipping.Load();
            context.VehiclwInformation.Load();
            context.askForUserDelyveryMs.Load();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if (vehiclwInformation.askForUserDelyveryM == null)
            {
                vehiclwInformation.askForUserDelyveryM = new AskForUserDelyveryM();
            }
            vehiclwInformation.askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature = sig;
            await context.SaveChangesAsync();
        }

        public async void SaveAsk1InDb(string idve, Ask1 ask1)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if (vehiclwInformation.Ask1 == null)
            {
                vehiclwInformation.Ask1 = new Ask1();
            }
            vehiclwInformation.Ask1 = ask1;
            await context.SaveChangesAsync();
        }

        public void SaveAskFromUserInDb(string idve, AskFromUser askFromUser)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if (vehiclwInformation.AskFromUser == null)
            {
                vehiclwInformation.AskFromUser = new AskFromUser();
            }
            vehiclwInformation.AskFromUser = askFromUser;
            context.SaveChangesAsync();
        }

        public void SaveAskDelyveryInDb(string idve, AskDelyvery askDelyvery)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if (vehiclwInformation.Ask1 == null)
            {
                vehiclwInformation.AskDelyvery = new AskDelyvery();
            }
            vehiclwInformation.AskDelyvery = askDelyvery;
            context.SaveChangesAsync();
        }

        public void SaveAskForUserDelyveryInDb(string idve, AskForUserDelyveryM askForUserDelyveryM)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id == Convert.ToInt32(idve));
            if (vehiclwInformation.Ask1 == null)
            {
                vehiclwInformation.askForUserDelyveryM = new AskForUserDelyveryM();
            }
            vehiclwInformation.askForUserDelyveryM = askForUserDelyveryM;
            context.SaveChangesAsync();
        }

        public bool CheckEmailAndPsw(string email, string password)
        {
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
                shipping.TotalPaymentToCarrier = paymentTeams;
                await context.SaveChangesAsync();
            }
        }

        public async void SaveToken(string email, string password, string token)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.EmailAddress == email && d.Password == password);
            driver.Token = token;
            await context.SaveChangesAsync();
        }

        public bool CheckToken(string token)
        {
            return context.Drivers.FirstOrDefault(d => d.Token == token) != null ? true : false;
        }

        public VehiclwInformation GetVehiclwInformationInDb(int idVech)
        {
            context.VehiclwInformation.Load();
            context.Asks.Load();
            context.Ask1s.Load();
            context.PhotoInspections.Load();
            context.Photos.Load();
            context.AskFromUsers.Load();
            context.AskDelyveries.Load();
            context.askForUserDelyveryMs.Load();
            return context.VehiclwInformation.FirstOrDefault(v => v.Id == idVech);
        }

        public List<Shipping> GetOrdersForToken(string token, ref bool isInspectionDriver)
        {
            context.Shipping.Load();
            context.VehiclwInformation.Load();
            context.Asks.Load();
            context.PhotoInspections.Load();
            context.Ask1s.Load();
            context.AskFromUsers.Load();
            context.AskDelyveries.Load();
            context.askForUserDelyveryMs.Load();
            context.Damages.Load();
            List<Shipping> Shipping1 = new List<Shipping>();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            List<Shipping> shippings = context.Shipping.ToList().FindAll(s => s.Driverr != null && s.Driverr.Id == driver.Id);
            isInspectionDriver = driver.IsInspectionDriver;
            if (shippings == null)
            {
                return new List<Shipping>();
            }
            Shipping1.AddRange(shippings.FindAll(s => s.CurrentStatus == "Picked up"));
            Shipping1.AddRange(shippings.FindAll(s => s.CurrentStatus == "Assigned"));
            //int countFor5 = Shipping1.Count / 5;
            //int ost = Shipping1.Count % 5;
            //int countGet = ost == 0 ? (5 * type) + 5 : (5 * type) + ost;
            return Shipping1; //.GetRange(5 * type, countGet);
        }

        public List<Shipping> GetOrdersDelyveryForToken(string token, int type)
        {
            context.VehiclwInformation.Load();
            context.Asks.Load();
            context.Ask1s.Load();
            context.AskDelyveries.Load();
            context.askForUserDelyveryMs.Load();
            context.PhotoInspections.Load();
            context.Damages.Load();
            List<Shipping> Shipping1 = new List<Shipping>();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            List<Shipping> shippings = context.Shipping.ToList().FindAll(s => s.Driverr != null && s.Driverr.Id == driver.Id);
            if (shippings == null)
            {
                return new List<Shipping>();
            }
            Shipping1.AddRange(shippings.FindAll(s => s.CurrentStatus == "Delivered" || s.CurrentStatus == "Paid" || s.CurrentStatus == "Biling"));
            //int countFor5 = Shipping1.Count / 5;
            //int ost = Shipping1.Count % 5;
            //int countGet = ost == 0 ? (5 * type) + 5 : (5 * type) + ost;
            return Shipping1; //.GetRange(5 * type, countGet);
        }
    }
}