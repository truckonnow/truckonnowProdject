using ApiMobaileServise.Models;
using DaoModels.DAO;
using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public List<Shipping> GetShipingPayd()
        {
            return context.Shipping.Where(s => s.CurrentStatus == "Delivered,Paid").ToList();
        }

        internal List<Driver> GetDriverInDb()
        {
            return context.Drivers.ToList();
        }

        internal List<Trailer> GetTrailers()
        {
            return context.Trailers.ToList();
        }

        internal List<Truck> GetTruck()
        {
            return context.Trucks.ToList();
        }

        public Shipping SendBolInDb(string idShip)
        {
            Shipping shipping = context.Shipping.Where(s => s.Id == idShip)
                .Include("VehiclwInformations.Scan")
                .Include(s => s.askForUserDelyveryM)
                .Include(s => s.AskFromUser)
                .FirstOrDefault();
            return shipping;
        }

        public async Task RefreshInspectionDriverInDb(int idDriver)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.Id == idDriver);
            if (driver != null)
            {
                driver.IsInspectionDriver = false;
                await context.SaveChangesAsync();
            }
        }

        public async Task RefreshInspectionToDayDriverInDb(int idDriver)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.Id == idDriver);
            if (driver != null)
            {
                driver.IsInspectionToDayDriver = false;
                await context.SaveChangesAsync();
            }
        }

        internal void SetPlateTruck(int id, string idDriver)
        {
            Driver driver = context.Drivers.Include(d => d.InspectionDrivers)
                       .FirstOrDefault(d => d.Id == Convert.ToInt32(idDriver));
            InspectionDriver inspectionDrivers = driver.InspectionDrivers.FirstOrDefault(i => DateTime.Parse(i.Date).Date == DateTime.Now.Date);
            if (inspectionDrivers != null)
            {
                Truck truck = context.Trucks.First(t => t.Id == id);
                inspectionDrivers.IdITruck = truck.Id;
                context.SaveChanges();
            }
        }

        internal void SetPlateTrailer(int id, string idDriver)
        {
            Driver driver = context.Drivers.Include(d => d.InspectionDrivers)
                       .FirstOrDefault(d => d.Id == Convert.ToInt32(idDriver));
            InspectionDriver inspectionDrivers = driver.InspectionDrivers.FirstOrDefault(i => DateTime.Parse(i.Date).Date == DateTime.Now.Date);
            if (inspectionDrivers != null)
            {
                Trailer trailer = context.Trailers.First(t => t.Id == id);
                inspectionDrivers.IdITruck = trailer.Id;
                context.SaveChanges();
            }
        }

        public async Task SetInspectionDriverInDb(string idDriver, InspectionDriver inspectionDriver)
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

        internal bool CheckFullNameAndPasswrodDB(string email, string fullName)
        {
            bool isCheckFullNameAdnPassword = false;
            Driver driver = context.Drivers.FirstOrDefault(d => d.EmailAddress == email && d.FullName == fullName);
            if(driver != null)
            {
                isCheckFullNameAdnPassword = true;
            }
            return isCheckFullNameAdnPassword;
        }

        public async Task SaveInspectionDriverInDb(string idDriver, PhotoDriver photo, int IndexPhoto)
        {
            try
            {
                Driver driver = context.Drivers.Include(d => d.InspectionDrivers)
                    .FirstOrDefault(d => d.Id == Convert.ToInt32(idDriver));
                if (driver.InspectionDrivers != null && driver.InspectionDrivers.Count != 0)
                {
                    InspectionDriver inspectionDrivers = driver.InspectionDrivers.FirstOrDefault(i => DateTime.Parse(i.Date).Date == DateTime.Now.Date);
                    if (inspectionDrivers == null)
                    {
                        inspectionDrivers = new InspectionDriver();
                        inspectionDrivers.Date = DateTime.Now.ToString();
                        inspectionDrivers.PhotosTruck = new List<PhotoDriver>();
                        driver.InspectionDrivers.Add(inspectionDrivers);
                    }
                    await context.SaveChangesAsync();
                    photo.IdInspaction = inspectionDrivers.Id;
                    photo.IndexPhoto = IndexPhoto;
                    inspectionDrivers.PhotosTruck = context.PhotoDrivers.Where(p => p.IdInspaction == inspectionDrivers.Id).ToList();
                    if (inspectionDrivers.PhotosTruck.FirstOrDefault(p => p.IndexPhoto == IndexPhoto) == null)
                    {
                        inspectionDrivers.CountPhoto++;
                        inspectionDrivers.PhotosTruck.Add(photo);
                    }
                    else
                    {

                    }
                    await context.SaveChangesAsync();
                }
                else
                {
                    InspectionDriver inspectionDrivers = new InspectionDriver();
                    driver.InspectionDrivers = new List<InspectionDriver>();
                    inspectionDrivers.Date = DateTime.Now.ToString();
                    inspectionDrivers.PhotosTruck = new List<PhotoDriver>();
                    inspectionDrivers.CountPhoto++;
                    inspectionDrivers.PhotosTruck.Add(photo);
                    driver.InspectionDrivers.Add(inspectionDrivers);
                    await context.SaveChangesAsync();
                    photo.IdInspaction = inspectionDrivers.Id;
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {

            }
        }

        internal string CheckTralerAndTruckDb(string token)
        {
            string plates = null;
            Driver driver =  context.Drivers.Where(d => d.Token == token)
                   .Include(d => d.InspectionDrivers)
                   .FirstOrDefault(); 
            InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
            List<Truck> trucks = context.Trucks.ToList();
            List<Trailer> trailers = context.Trailers.ToList();
            DateTime dateTime = Convert.ToDateTime(inspectionDriver.Date);
            if (dateTime.Date == DateTime.Now.Date)
            {
                Truck truck = trucks.FirstOrDefault(t => t.Id == inspectionDriver.IdITruck);
                Trailer trailer = trailers.FirstOrDefault(t => t.Id == inspectionDriver.IdITrailer);
                string plateTruck = truck != null ? truck.PlateTruk : "";
                string plateTrailer = trailer != null ? trailer.Plate : "";
                plates = $"{plateTruck},{plateTrailer}";
            }
            else
            {
                plates = ",";
            }
            return plates;
        }

        internal string GetDocumentDB(string id)
        {
            //string pathDoc = "";
            //Driver driver = context.Drivers
            //    .Include(d => d.InspectionDrivers)
            //    .FirstOrDefault(d => d.Id.ToString() == id);
            //if(driver.InspectionDrivers != null)
            //{
            //    InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
            //    Truck truck = context.Trucks.FirstOrDefault(t => t.Id == inspectionDriver.IdITruck);
            //    if(truck != null)
            //    {
            //        pathDoc = truck.PathDoc;
            //    }
            //}
            return "";
        }

        public async Task UpdateInspectionDriver(string idDriver)
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
                if(dateTime.Date != DateTime.Now.Date || (inspectionDriver.CountPhoto <= 44))
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

        internal void SavePhotoStrapInDb(string id, List<Photo> photos)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id.ToString() == id);
            if (vehiclwInformation != null)
            {
                vehiclwInformation.Ask1.App_will_force_driver_to_take_pictures_of_each_strap = photos;
                context.SaveChanges();
            }
        }

        internal void SavePhotoinTruckInDb(string id, List<Photo> photos)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id.ToString() == id);
            if(vehiclwInformation != null)
            {
                vehiclwInformation.Ask1.Photo_after_loading_in_the_truck = photos;
                context.SaveChanges();
            }
        }

        internal string GetLastInspaction(string idDriver)
        {
            string lastInspectionDriver = null;
            Driver driver = context.Drivers.Where(d => d.Id.ToString() == idDriver)
                 .Include(d => d.InspectionDrivers)
                 .FirstOrDefault();
            if(driver.InspectionDrivers != null && driver.InspectionDrivers.Count != 0)
            {
                InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
                Truck truck = context.Trucks.FirstOrDefault(t => t.Id == inspectionDriver.IdITruck);
                Trailer trailer = context.Trailers.FirstOrDefault(t => t.Id == inspectionDriver.IdITrailer);
                lastInspectionDriver = inspectionDriver.Date.Remove(inspectionDriver.Date.IndexOf(" ") + 1);
                if(truck != null)
                {
                    lastInspectionDriver += $",{truck.PlateTruk}";
                }
                if (trailer != null)
                {
                    lastInspectionDriver += $",{trailer.Plate}";
                }

            }
            return lastInspectionDriver;
        }

        internal bool SetTralerAndTruck(string token, string plateTrailer, string plateTruck)
        {
            Truck truck = context.Trucks.FirstOrDefault(t => t.PlateTruk == plateTruck);
            Trailer trailer = context.Trailers.FirstOrDefault(t => t.Plate == plateTrailer);
            if (truck == null && trailer == null)
                return false;
            Driver driver = context.Drivers
                .Include(d => d.InspectionDrivers)
                .First(d => d.Token == token);
            InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
            DateTime dateTime = Convert.ToDateTime(inspectionDriver.Date);
            if (dateTime.Date == DateTime.Now.Date)
            {
                if (truck != null)
                {
                    inspectionDriver.IdITruck = truck.Id;
                }
                if (trailer != null)
                {
                    inspectionDriver.IdITrailer = trailer.Id;
                }
            }
            else
            {
                if (truck != null)
                {
                    driver.InspectionDrivers.Add(new InspectionDriver()
                    {
                        IdITruck = truck.Id,
                        Date = DateTime.Now.ToString(),
                        CountPhoto = 0
                    });
                }
                if (trailer != null)
                {
                    driver.InspectionDrivers.Add(new InspectionDriver()
                    {
                        IdITrailer = trailer.Id,
                        Date = DateTime.Now.ToString(),
                        CountPhoto = 0
                    });
                }
            }
            context.SaveChanges();
            return true;
        }

        public int GetIndexDb(string token)
        {
            int indexPhoto = 0;
            Driver driver = context.Drivers.Where(d => d.Token == token)
                 .Include(d => d.InspectionDrivers)
                 .FirstOrDefault();
            if (driver.InspectionDrivers != null && driver.InspectionDrivers.Count != 0)
            {
                InspectionDriver inspectionDrivers = driver.InspectionDrivers.FirstOrDefault(i => DateTime.Parse(i.Date).Date == DateTime.Now.Date);
                if (inspectionDrivers == null)
                {
                    File.WriteAllText("GetIndexDb.txt", "indexPhoto = 1;");
                    indexPhoto = 1;
                }
                else
                {
                    indexPhoto = inspectionDrivers.CountPhoto + 1;
                    File.WriteAllText("GetIndexDb.txt", $"indexPhoto = inspectionDrivers.CountPhoto + 1;  {inspectionDrivers.CountPhoto + 1}");
                }
            }
            else
            {
                File.WriteAllText("GetIndexDb.txt", "indexPhoto = 1;");
                indexPhoto = 1;
            }
            return indexPhoto;
        }

        public async void SaveGPSLocationData(string token, Geolocations geolocations)
        {
            Driver driver = context.Drivers.Where(d => d.Token == token)
                .Include(d => d.geolocations)
                .FirstOrDefault();
            driver.geolocations = geolocations;
            await context.SaveChangesAsync();
        }

        internal async void SaveAsk2InDb(string idShiping, Ask2 ask2)
        {
            Shipping shipping = context.Shipping.FirstOrDefault(s => s.Id == idShiping);
            shipping.Ask2 = ask2;
            await context.SaveChangesAsync();
        }

        public async void SaveTokenStoreinDb(string token, string tokenStore)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            driver.TokenShope = tokenStore;
            await context.SaveChangesAsync();
        }

        public async Task<VehiclwInformation> GetVehiclwInformationAndSaveDamageForUser(string idVech, string idShiping, List<DamageForUser> damageForUsers)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.Where(v => v.Id.ToString() == idVech)
                 .Include(v => v.Ask)
                 .Include("PhotoInspections.Photos")
                 .Include("DamageForUsers")
                 .FirstOrDefault();
            Shipping shipping = context.Shipping
                 .Include("DamageForUsers")
                 .FirstOrDefault(v => v.Id == idShiping);
            if (shipping.DamageForUsers == null)
            {
                shipping.DamageForUsers = new List<DamageForUser>();
            }
            shipping.DamageForUsers.AddRange(damageForUsers);
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
                photo.Base64 = Convert.ToBase64String(File.ReadAllBytes($"../Scans/scan{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}.jpg"));
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

            shipping.UrlReqvest = "";
            return shipping;
        }

        public Shipping GetShippingPhotInDb(string idShip)
        {
            Shipping shipping = context.Shipping.Where(s => s.Id.ToString() == idShip)
                .Include("VehiclwInformations.PhotoInspections.Damages")
                .Include("VehiclwInformations.Scan")
                .Include("VehiclwInformations.PhotoInspections.Photos")
                .FirstOrDefault();

            shipping.UrlReqvest = "";
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

        public async void SavePayMethotInDb(string idShiping, string payMethod, string countPay)
        {
            Shipping shipping = context.Shipping.Where(v => v.Id == idShiping)
                .Include(v => v.AskFromUser)
                .FirstOrDefault();
            if (shipping.AskFromUser == null)
            {
                shipping.AskFromUser = new AskFromUser();
            }
            shipping.AskFromUser.What_form_of_payment_are_you_using_to_pay_for_transportation = payMethod;
            shipping.AskFromUser.CountPay = countPay;
            await context.SaveChangesAsync();
        }

        public bool CheckProplemDb(string idShiping)
        {
            Shipping shipping = context.Shipping.First(s => s.Id == idShiping);
            return shipping.IsProblem;
        }

        public void SetProblemDb(string idShiping)
        {
            Shipping shipping = context.Shipping.First(s => s.Id == idShiping);
            shipping.IsProblem = true;
            context.SaveChanges();
        }

        public async void SavePayInDb(string idShiping, int type, Photo photo)
        {
            Shipping shipping = context.Shipping.Where(v => v.Id == idShiping)
                .Include(v => v.AskFromUser)
                .Include(v => v.askForUserDelyveryM)
                .FirstOrDefault();
            if (type == 1 && shipping.AskFromUser != null)
            {
                shipping.AskFromUser.PhotoPay = photo;
            }
            else if (type == 2 && shipping != null && shipping.askForUserDelyveryM != null)
            {
                shipping.askForUserDelyveryM.PhotoPay = photo;
            }
            await context.SaveChangesAsync();
        }

        public async Task SaveRecontInDb(string idShiping, int type, Video video)
        {
            Shipping shipping = context.Shipping.Where(v => v.Id == idShiping)
                .Include(v => v.AskFromUser)
                .Include(v => v.askForUserDelyveryM)
                .Include("AskFromUser.PhotoPay")
                .Include("AskFromUser.VideoRecord")
                .FirstOrDefault();
            if (type == 1 && shipping.AskFromUser != null)
            {
                shipping.AskFromUser.VideoRecord = video;
            }
            else if (type == 2 && shipping != null && shipping.askForUserDelyveryM != null)
            {
                shipping.askForUserDelyveryM.VideoRecord = video;
            }
            await context.SaveChangesAsync();
        }

        public List<Tasks> CheckTask(string token)
        {
            Driver driver = context.Drivers
                .First(d => d.Token == token);
            return context.TaskLoads
                .Where(t => t.IdDriver == driver.Id.ToString())
                .Select(t => new Tasks()
                {
                    IdTask = t.Id.ToString(),
                    Method =t.NameMethod
                })
                .ToList();
        }

        public async Task<string> LoadTaskDb(string idTask, byte[] buffer)
        {
            TaskLoad taskLoad = await context.TaskLoads
                .FirstOrDefaultAsync(l => l.Id.ToString() == idTask);
            if(taskLoad == null)
            {
                return "No";
            }
            string str = Encoding.Default.GetString(taskLoad.Array);
            string str1 = Encoding.Default.GetString(buffer);
            taskLoad.Array = Encoding.Default.GetBytes(str + str1);
            await context.SaveChangesAsync();
            return taskLoad.Id.ToString();
        }

        public async Task<string[]> EndTaskDb(string idTask)
        {
            LogTask logTask = await context.LogTasks
                .Include(l => l.TaskLoads)
                .FirstOrDefaultAsync();
            if(logTask == null)
            {
                return null;
            }
            TaskLoad taskLoad = logTask.TaskLoads.FirstOrDefault(l => l.Id.ToString() == idTask);
            if (taskLoad == null)
            {
                return null;
            }
            string str = Encoding.Default.GetString(taskLoad.Array);
            context.TaskLoads.Remove(taskLoad);
            await context.SaveChangesAsync();
            return new string[] { str, taskLoad.OptionalParameter};
        }

        public async Task<string> StartTaskDb(string nameMethod, string optionalParameter, string token)
        {
            Driver driver = context.Drivers.First(d => d.Token == token);
            LogTask logTask = context.LogTasks
                .Include(l => l.TaskLoads)
                .FirstOrDefault();
            if(logTask == null)
            {
                context.LogTasks.Add(new LogTask()
                {
                    TaskLoads = new List<TaskLoad>()
                });
                context.SaveChanges();
                logTask = context.LogTasks
                 .Include(l => l.TaskLoads)
                 .FirstOrDefault();
            }
            TaskLoad taskLoad = new TaskLoad()
            {
                Array = new byte[0],
                NameMethod = nameMethod,
                OptionalParameter = optionalParameter,
                IdDriver = driver.Id.ToString()
            };
            logTask.TaskLoads.Add(taskLoad);
            context.SaveChanges();
            return taskLoad.Id.ToString();
        }

        public async void SaveFeedBackInDb(Feedback feedback)
        {
            context.Feedbacks.Add(feedback);
            await context.SaveChangesAsync();
        }

        public async void ReCurentStatus(string idShip, string status)
        {
            try
            {
                Shipping shipping = context.Shipping.FirstOrDefault(s => s.Id == idShip);
                shipping.CurrentStatus = status;
                if (status == "Delivered,Billed" && shipping.TotalPaymentToCarrier.Contains(" days"))
                {
                    shipping.DataPaid = DateTime.Now.AddDays(Convert.ToInt32(shipping.TotalPaymentToCarrier.Replace(" days", ""))).ToString();
                    shipping.DataCancelOrder = DateTime.Now.ToString();
                }
                else if (status == "Delivered,Paid")
                {
                    shipping.DataFullArcive = DateTime.Now.AddDays(21).ToString();
                    shipping.DataCancelOrder = DateTime.Now.ToString();
                }
                else if (status == "Delivered,Billed")
                {

                }
                else if (shipping.CurrentStatus == "Archived")
                {
                    shipping.CurrentStatus = shipping.CurrentStatus.Replace("Delivered", "Archived");
                }
                await context.SaveChangesAsync();
            }
            catch
            { }
        }

        public string GerShopTokenForShipping(string idOrder)
        {
            Shipping shipping = context.Shipping.Where(d => d.Id == idOrder)
                .Include(s => s.Driverr)
                .FirstOrDefault();
            return shipping.Driverr.TokenShope;
        }

        public async void SaveSigPikedUpInDb(string idShip, Photo sig)
        {
            Shipping shipping = context.Shipping.Where(v => v.Id == idShip)
                .Include(v => v.AskFromUser)
                .Include(v => v.AskFromUser.App_will_ask_for_signature_of_the_client_signature)
                .FirstOrDefault();
            if (shipping.AskFromUser == null)
            {
                shipping.AskFromUser = new AskFromUser();
            }
            shipping.AskFromUser.App_will_ask_for_signature_of_the_client_signature = sig;
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

        public void SaveAskFromUserInDb(string idShip, AskFromUser askFromUser)
        {
            Shipping shipping = context.Shipping.Where(s => s.Id == idShip)
                .FirstOrDefault();
            if (shipping.AskFromUser == null)
            {
                shipping.AskFromUser = new AskFromUser();
            }
            shipping.AskFromUser = askFromUser;
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

        public async void SaveAskForUserDelyveryInDb(string idShiping, AskForUserDelyveryM askForUserDelyveryM)
        {

            Shipping shipping = context.Shipping.Where(v => v.Id == idShiping)
                .FirstOrDefault();
            if (shipping.askForUserDelyveryM == null)
            {
                shipping.askForUserDelyveryM = new AskForUserDelyveryM();
            }
            shipping.askForUserDelyveryM = askForUserDelyveryM;
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

        public string SaveToken(string email, string password, string token)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.EmailAddress == email && d.Password == password);
            driver.Token = token;
            context.SaveChanges();
            return driver.Id.ToString();
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
            Driver driver = context.Drivers.Include(d => d.InspectionDrivers).FirstOrDefault(d => d.Token == token);
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
                .Include("VehiclwInformations.Ask1.App_will_force_driver_to_take_pictures_of_each_strap")
                .Include("VehiclwInformations.PhotoInspections.Damages")
                .Include(s => s.AskFromUser.App_will_ask_for_signature_of_the_client_signature)
                .Include(s => s.AskFromUser.PhotoPay)
                .Include(s => s.AskFromUser.VideoRecord)
                .Include("VehiclwInformations.AskDelyvery")
                .Include(s => s.askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature)
                .Include(s => s.askForUserDelyveryM.PhotoPay)
                .Include(s => s.askForUserDelyveryM.VideoRecord)
                .Include(s => s.Ask2)
                .ToList();
            if (shippings == null)
            {
                return new List<Shipping>();
            }
            Shipping1.AddRange(shippings.FindAll(s => s.CurrentStatus == "Picked up"));
            Shipping1.AddRange(shippings.FindAll(s => s.CurrentStatus == "Assigned"));
            Shipping1.ForEach((item) => item.UrlReqvest = "");
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
                 .Include("VehiclwInformations")
                 .ToList();
            if (shippings == null)
            {
                return new List<Shipping>();
            }
            Shipping1.AddRange(shippings.FindAll(s => (s.CurrentStatus == "Delivered,Paid" || s.CurrentStatus == "Delivered,Billed" || s.CurrentStatus == "Archived,Billed" || s.CurrentStatus == "Archived,Paid") 
            && s.DataCancelOrder != null && DateTime.Parse(s.DataCancelOrder).AddDays(7) > DateTime.Now));
            Shipping1.ForEach((item) => item.UrlReqvest = "");
            //int countFor5 = Shipping1.Count / 5;
            //int ost = Shipping1.Count % 5;
            //int countGet = ost == 0 ? (5 * type) + 5 : (5 * type) + ost;
            return Shipping1; //.GetRange(5 * type, countGet);
        }

        public List<Shipping> GetOrdersArchiveForToken(string token, int type)
        {
            List<Shipping> Shipping1 = new List<Shipping>();
            Driver driver = context.Drivers.FirstOrDefault(d => d.Token == token);
            List<Shipping> shippings = context.Shipping.Where(s => s.Driverr != null && s.Driverr.Id == driver.Id)
                 .Include("VehiclwInformations")
                 .ToList();
            if (shippings == null)
            {
                return new List<Shipping>();
            }
            Shipping1.AddRange(shippings.FindAll(s => (s.CurrentStatus == "Delivered,Paid" || s.CurrentStatus == "Delivered,Billed" || s.CurrentStatus == "Archived,Billed" || s.CurrentStatus == "Archived,Paid") 
            && s.DataCancelOrder != null && DateTime.Parse(s.DataCancelOrder).AddDays(7) < DateTime.Now && DateTime.Parse(s.DataCancelOrder).AddDays(21) > DateTime.Now));
            Shipping1.ForEach((item) => item.UrlReqvest = "");
            //int countFor5 = Shipping1.Count / 5;
            //int ost = Shipping1.Count % 5;
            //int countGet = ost == 0 ? (5 * type) + 5 : (5 * type) + ost;
            return Shipping1; //.GetRange(5 * type, countGet);
        }
    }
}