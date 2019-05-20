using DaoModels.DAO;
using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InspectionDriver = DaoModels.DAO.Models.InspectionDriver;

namespace ApiMobaileServise.Servise
{
    public class SqlCommandApiMobile
    {
        private Context context = null;

        public SqlCommandApiMobile()
        {
            context = new Context();
        }

        public async void SetInspectionDriverInDb(string idDriver, InspectionDriver inspectionDriver)
        {
            context.InspectionDrivers.Load();
            Driver driver = await context.Drivers.FirstOrDefaultAsync(d => d.Id == Convert.ToUInt32(idDriver));
            if(driver.InspectionDrivers == null)
            {
                driver.InspectionDrivers = new List<InspectionDriver>();
            }
            driver.InspectionDrivers.Add(inspectionDriver);
            await context.SaveChangesAsync();
        }

        public async void SaveInspectionDriverInDb(string idDriver, Photo photo, int IndexPhoto)
        {
            InspectionDriver inspectionDrivers = null;
            context.Drivers.Include("InspectionDrivers.PhotosTruck").ToList();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Id == Convert.ToUInt32(idDriver));
            if (driver.InspectionDrivers != null && driver.InspectionDrivers.Count != 0)
            {
                inspectionDrivers = driver.InspectionDrivers.Last();
                if (inspectionDrivers.CountPhoto >= IndexPhoto)
                {
                    driver.InspectionDrivers = new List<InspectionDriver>();
                    inspectionDrivers = new InspectionDriver();
                }
                if(inspectionDrivers.PhotosTruck == null)
                {
                    inspectionDrivers.PhotosTruck = new List<Photo>();
                }
                inspectionDrivers.CountPhoto++;
                inspectionDrivers.PhotosTruck.Add(photo);
                await context.SaveChangesAsync();
            }
            else
            {
                driver.InspectionDrivers = new List<InspectionDriver>();
                inspectionDrivers = new InspectionDriver();
                inspectionDrivers.PhotosTruck = new List<Photo>();
                inspectionDrivers.CountPhoto++;
                inspectionDrivers.PhotosTruck.Add(photo);
                driver.InspectionDrivers.Add(inspectionDrivers);
                await context.SaveChangesAsync();
            }
        }

        public async void UpdateInspectionDriver(string idDriver)
        {
            context.InspectionDrivers.Load();
            Driver driver = await context.Drivers.FirstOrDefaultAsync(d => d.Id == Convert.ToUInt32(idDriver));
            InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
            inspectionDriver.Date = DateTime.Now.ToString();
            driver.IsInspectionDriver = true;
            driver.IsInspectionToDayDriver = true;
            await context.SaveChangesAsync();
        }

        public async Task<bool> ChechToDayInspactionInDb(string token)
        {
            bool isInspaction = false;
            context.Drivers.Load();
            context.InspectionDrivers.Load();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            if (driver.InspectionDrivers != null && driver.InspectionDrivers.Count != 0)
            {
                InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
                DateTime dateTime = Convert.ToDateTime(inspectionDriver.Date);
                if(dateTime.Date < DateTime.Now.Date || (inspectionDriver.CountPhoto <= 40))
                {
                    isInspaction = false;
                }
                else
                {
                    driver.IsInspectionToDayDriver = true;
                    driver.IsInspectionDriver = true;
                    isInspaction = true;
                }
            }
            else
            {
                isInspaction = false;
            }
            await context.SaveChangesAsync();
            return isInspaction;
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
            context.Drivers.Load();
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
            //context.PhotoInspections.Load();
            //context.Shipping.Load();
            //context.VehiclwInformation.Load();
            //context.Damages.Load();
            //context.Photos.Load();
            Shipping shipping = context.Shipping.Where(s => s.Id.ToString() == idShip)
                .Include("VehiclwInformations.PhotoInspections.Damages")
                .Include("VehiclwInformations.Scan")
                .FirstOrDefault();
            return shipping;
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
            Shipping shipping = context.Shipping.FirstOrDefault(d => d.Id == idOrder);
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

        public string GetInspectionDriverIndb(string token)
        {
            string statusAndTimeInInspection = null;
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            if(driver.IsInspectionDriver)
            {
                if(driver.IsInspectionToDayDriver)
                {
                    statusAndTimeInInspection = "true,true,0";
                }
                else
                {
                    string TimOfInsection = "";
                    if ((12 - DateTime.Now.Hour) < 0 || DateTime.Now.Hour < 5)
                    {
                        TimOfInsection = "0";
                    }
                    else
                    {
                        TimOfInsection = (12 - DateTime.Now.Hour).ToString();
                    }
                    statusAndTimeInInspection = "true,false,"+ TimOfInsection;
                }
            }
            else
            {
                if (driver.IsInspectionToDayDriver)
                {
                    statusAndTimeInInspection = "false,true,0";
                }
                else
                {
                    statusAndTimeInInspection = "false,false,0";
                }
            }
            statusAndTimeInInspection += $",{driver.Id}";
            return statusAndTimeInInspection;
        }

        public VehiclwInformation GetVehiclwInformationInDb(int idVech)
        {
            //context.VehiclwInformation.Load();
            //context.Asks.Load();
            //context.Ask1s.Load();
            //context.Photos.Load();
            //context.AskFromUsers.Load();
            //context.AskDelyveries.Load();
            //context.askForUserDelyveryMs.Load();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.Where(v => v.Id == idVech)
                .Include("Ask.Any_personal_or_additional_items_with_or_in_vehicle")
                .Include("Ask.Any_paperwork_or_documentation")
                .Include("Ask1.Any_additional_parts_been_given_to_you")
                .Include("Ask1.Any_additional_documentation_been_given_after_loading")
                .Include("Ask1.App_will_force_driver_to_take_pictures_of_each_strap")
                .Include("Ask1.Photo_after_loading_in_the_truck")
                .Include("PhotoInspections.Photos")
                .Include("AskFromUser.PhotoPay")
                .Include(v => v.AskDelyvery)
                .Include("askForUserDelyveryM.PhotoPay")
                .Include("askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature")
                .Include(v => v.Scan)
                .Include(v => v.DamageForUsers)
                .FirstOrDefault();
            return vehiclwInformation;
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
            Shipping1.AddRange(shippings.FindAll(s => s.CurrentStatus == "Delivered,Paid" || s.CurrentStatus == "Delivered,Billed"));
            //int countFor5 = Shipping1.Count / 5;
            //int ost = Shipping1.Count % 5;
            //int countGet = ost == 0 ? (5 * type) + 5 : (5 * type) + ost;
            return Shipping1; //.GetRange(5 * type, countGet);
        }
    }
}