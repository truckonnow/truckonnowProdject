﻿using DaoModels.DAO;
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

        public Shipping SendBolInDb(string idShip)
        {
            Shipping shipping = context.Shipping.Where(s => s.Id == idShip)
                .Include("VehiclwInformations.Scan")
                .Include("VehiclwInformations.AskFromUser.App_will_ask_for_signature_of_the_client_signature")
                .Include("VehiclwInformations.askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature")
                .FirstOrDefault();
            return shipping;
        }

        public async void SetInspectionDriverInDb(string idDriver, InspectionDriver inspectionDriver)
        {
            Driver driver = await context.Drivers.Where(d => d.Id == Convert.ToUInt32(idDriver))
                .Include(d => d.InspectionDrivers)
                .FirstOrDefaultAsync();
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
            Driver driver = await context.Drivers.Where(d => d.Id == Convert.ToUInt32(idDriver))
                   .Include(d => d.InspectionDrivers)
                   .FirstOrDefaultAsync();
            InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
            inspectionDriver.Date = DateTime.Now.ToString();
            driver.IsInspectionDriver = true;
            driver.IsInspectionToDayDriver = true;
            await context.SaveChangesAsync();
        }

        public async Task<bool> ChechToDayInspactionInDb(string token)
        {
            bool isInspaction = false;
            Driver driver = await context.Drivers.Where(d => d.Token == token)
                 .Include(d => d.InspectionDrivers)
                 .FirstOrDefaultAsync();
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
            Driver driver = context.Drivers.Where(d => d.Token == token)
                .Include(d => d.geolocations)
                .FirstOrDefault();
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
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.Where(v => v.Id.ToString() == idVech)
                 .Include(v => v.Ask)
                 .Include("PhotoInspections.Photos")
                 .Include("DamageForUsers")
                 .FirstOrDefault();
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
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.Where(v => v.Id.ToString() == idVe)
                .Include(v => v.Ask)
                .Include(v => v.Scan)
                .Include("PhotoInspections.Photos")
                .Include("PhotoInspections.Damages")
                .FirstOrDefault();
            if (vehiclwInformation.PhotoInspections == null)
            {
                vehiclwInformation.PhotoInspections = new List<PhotoInspection>();
            }
            if (photoInspection.IndexPhoto == 1 && photoInspection.CurrentStatusPhoto == "PikedUp")
            {
                Photo photo = new Photo();
                photo.path = $"../Photo/{vehiclwInformation.Id}/scan.jpg";
                photo.Base64 = JsonConvert.SerializeObject(File.ReadAllBytes($"../Scans/scan{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}.jpg"));
                vehiclwInformation.Scan = photo;
            }
            vehiclwInformation.PhotoInspections.Add(photoInspection);
            await context.SaveChangesAsync();
            return vehiclwInformation;
        }

        public Shipping GetShippingInDb(string idShip)
        {
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
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.Where(v => v.Id == Convert.ToInt32(idVech))
                .Include(v => v.AskFromUser)
                .FirstOrDefault();
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
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.Where(v => v.Id == Convert.ToInt32(idVech))
                .Include(v => v.AskFromUser)
                .Include(v => v.askForUserDelyveryM)
                .FirstOrDefault();
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

        public async Task SaveRecontInDb(string idVech, int type, Video video)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.Where(v => v.Id == Convert.ToInt32(idVech))
                .Include(v => v.AskFromUser)
                .Include(v => v.askForUserDelyveryM)
                .FirstOrDefault();
            if (type == 1 && vehiclwInformation.AskFromUser != null)
            {
                vehiclwInformation.AskFromUser.VideoRecord = video;
            }
            else if (type == 2 && vehiclwInformation != null && vehiclwInformation.askForUserDelyveryM != null)
            {
                vehiclwInformation.askForUserDelyveryM.VideoRecord = video;
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
            try
            {
                if (status == "Delivered,Billed" && shipping.TotalPaymentToCarrier.Contains(" days"))
                {
                    shipping.DataPaid = DateTime.Now.AddDays(Convert.ToInt32(shipping.TotalPaymentToCarrier.Replace(" days", ""))).ToString();
                }
            }
            catch
            { }
            await context.SaveChangesAsync();
        }

        public string GerShopTokenForShipping(string idOrder)
        {
            Shipping shipping = context.Shipping.Where(d => d.Id == idOrder)
                .Include(s => s.Driverr)
                .FirstOrDefault();
            return shipping.Driverr.TokenShope;
        }

        public async void SaveSigPikedUpInDb(string idve, Photo sig)
        {
            context.askForUserDelyveryMs.Load();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.Where(v => v.Id == Convert.ToInt32(idve))
                .Include(v => v.askForUserDelyveryM)
                .FirstOrDefault();
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
            Shipping shipping = context.Shipping.Where(s => s.VehiclwInformations.FirstOrDefault(v => v.Id == Convert.ToInt32(idve)) != null)
                .Include(s => s.VehiclwInformations)
                .FirstOrDefault();
            if (shipping.VehiclwInformations[0].AskFromUser == null)
            {
                shipping.VehiclwInformations[0].AskFromUser = new AskFromUser();
            }
            shipping.VehiclwInformations[0].AskFromUser = askFromUser;
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

        public async void SaveAskForUserDelyveryInDb(string idve, AskForUserDelyveryM askForUserDelyveryM)
        {

            Shipping shipping = context.Shipping.Where(s => s.VehiclwInformations.FirstOrDefault(v => v.Id == Convert.ToInt32(idve)) != null)
                .Include(s => s.VehiclwInformations)
                .FirstOrDefault();
            if (shipping.VehiclwInformations[0].askForUserDelyveryM == null)
            {
                shipping.VehiclwInformations[0].askForUserDelyveryM = new AskForUserDelyveryM();
            }
            shipping.VehiclwInformations[0].askForUserDelyveryM = askForUserDelyveryM;
            await context.SaveChangesAsync();
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

        public async void ClearTokenDb(string token)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            driver.Token = null;
            driver.TokenShope = null;
            await context.SaveChangesAsync();
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

        public List<Shipping> GetOrdersForToken(string token)
        {

            List<Shipping> Shipping1 = new List<Shipping>();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            List<Shipping> shippings = context.Shipping.Where(s => s.Driverr != null && s.Driverr.Id == driver.Id)
                .Include("VehiclwInformations.Ask")
                .Include("VehiclwInformations.Ask1")
                .Include("VehiclwInformations.PhotoInspections.Damages")
                .Include("VehiclwInformations.AskFromUser")
                .Include("VehiclwInformations.AskDelyvery")
                .Include("VehiclwInformations.askForUserDelyveryM")
                .ToList();
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
            List<Shipping> Shipping1 = new List<Shipping>();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            List<Shipping> shippings = context.Shipping.Where(s => s.Driverr != null && s.Driverr.Id == driver.Id)
                 .Include("VehiclwInformations.Ask")
                 .Include("VehiclwInformations.Ask1")
                 .Include("VehiclwInformations.PhotoInspections.Damages")
                 .Include("VehiclwInformations.AskFromUser")
                 .Include("VehiclwInformations.AskDelyvery")
                 .Include("VehiclwInformations.askForUserDelyveryM")
                 .ToList();
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