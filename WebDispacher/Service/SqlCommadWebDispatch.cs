using DaoModels.DAO;
using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDispacher.Dao
{
    public class SqlCommadWebDispatch
    {
        private Context context = null;

        public SqlCommadWebDispatch()
        {
            context = new Context();
            if (Context._cache == null) { Context._cache = new MemoryCache(new MemoryCacheOptions()); }
            InitUserOne();
        }

        private async void InitUserOne()
        {
            try
            {
                if (context.User.Count() == 0)
                {
                    Users users = new Users();
                    users.Login = "DevRoma";
                    users.Password = "polkilo123";
                    await context.User.AddAsync(users);
                    users = new Users();
                    users.Login = "ArtemManager";
                    users.Password = "truckon777";
                    await context.User.AddAsync(users);
                    users = new Users();
                    users.Login = "Designer";
                    users.Password = "truckon777";
                    await context.User.AddAsync(users);
                    users = new Users();
                    users.Login = "Truckonnow";
                    users.Password = "truckon777";
                    await context.User.AddAsync(users);
                    users = new Users();
                    users.Login = "Truckonnow1";
                    users.Password = "truckon777";
                    await context.User.AddAsync(users);
                    users = new Users();
                    users.Login = "Truckonnow2";
                    users.Password = "truckon777";
                    await context.User.AddAsync(users);
                    users = new Users();
                    users.Login = "Truckonnow3";
                    users.Password = "truckon777";
                    await context.User.AddAsync(users);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {

            } 
        }

        internal async Task<Truck> GetTruckDb(string idDriver)
        {
            Truck truck = null;
            Driver driver = await context.Drivers.Include(d => d.InspectionDrivers).FirstOrDefaultAsync(d => d.Id.ToString() == idDriver);
            if (driver != null && driver.InspectionDrivers != null && driver.InspectionDrivers.Count != 0)
            {
                InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
                truck = await context.Trucks.FirstOrDefaultAsync(t => t.Id == inspectionDriver.IdITruck);
            }
            return truck; 
        }

        internal async Task<Trailer> GetTrailerDb(string idDriver)
        {
            Trailer trailer = null;
            Driver driver = await context.Drivers.Include(d => d.InspectionDrivers).FirstOrDefaultAsync(d => d.Id.ToString() == idDriver);
            if (driver != null && driver.InspectionDrivers != null && driver.InspectionDrivers.Count != 0)
            {
                InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
                trailer = await context.Trailers.FirstOrDefaultAsync(t => t.Id == inspectionDriver.IdITrailer);
            }
            return trailer;
        }

        internal int ResetPasswordFoDriver(string newPassword, string idDriver, string token)
        {
            int isStateActual = 0;
            PasswordRecovery passwordRecovery = context.PasswordRecoveries.ToList().FirstOrDefault(p => p.IdDriver.ToString() == idDriver && p.Token == token);
            if (passwordRecovery != null && Convert.ToDateTime(passwordRecovery.Date) > DateTime.Now.AddHours(-2))
            {
                Driver driver = context.Drivers.FirstOrDefault(d => d.Id.ToString() == idDriver);
                driver.Password = newPassword;
                isStateActual = 2;
            }
            if(passwordRecovery != null)
            {
                context.PasswordRecoveries.Remove(passwordRecovery);
            }
            context.SaveChanges();
            return isStateActual;
        }

        internal int GetUserIdByKey(string key)
        {
            using (Context _context = new Context())
            {
                return _context.User.First(u => u.KeyAuthorized == key).Id;
            }
        }

        internal string GetFullNameDriverById(string idDriver)
        {
            return context.Drivers.First(d => d.Id.ToString() == idDriver).FullName;
        }

        internal string GetDriverIdByIdOrder(string idOrder)
        {
            Shipping shipping = context.Shipping
                .Include(s => s.Driverr)
                .First(s => s.Id.ToString() == idOrder);
            return shipping.Driverr.Id.ToString();
        }

        internal string GetFullNameUserByKey(string key)
        {
            return context.User.First(u => u.KeyAuthorized == key).Login;
        }

        internal int CheckReportDriverDb(string nameDriver, string driversLicense)
        {
            List<DriverReport> driverReports = new List<DriverReport>();
            if (nameDriver != null || driversLicense != null)
            {
                driverReports.AddRange(context.DriverReports.Where(d => nameDriver == d.FullName && driversLicense == d.DriversLicenseNumber));
            }
            return driverReports.Count;
        }

        internal void AddHistory(HistoryOrder historyOrder)
        {
            context.HistoryOrders.Add(historyOrder);
            context.SaveChanges();
        }

        internal string GetEmailDriverDb(string idDriver)
        {
            string emailDriver = "";
            Driver driver = context.Drivers.FirstOrDefault(d => d.Id.ToString() == idDriver);
            if (driver != null)
            {
                emailDriver = driver.EmailAddress;
            }
            return emailDriver;
        }

        internal void AddNewReportDriverDb(string fullName, string driversLicenseNumber, string description)
        {
            context.DriverReports.Add(new DriverReport()
            {
                Comment = description,
                DriversLicenseNumber = driversLicenseNumber,
                FullName = fullName
            });
            context.SaveChanges();
        }

        internal int CheckTokenFoDriverDb(string idDriver, string token)
        {
            int isStateActual = 0;
            PasswordRecovery passwordRecovery = context.PasswordRecoveries.ToList().FirstOrDefault(p => p.IdDriver.ToString() == idDriver && p.Token == token);
            if(passwordRecovery != null && Convert.ToDateTime(passwordRecovery.Date) > DateTime.Now.AddHours(-2))
            {
                isStateActual = 1;
            }
            return isStateActual;
        }

        internal VehiclwInformation GetVechById(string idVech)
        {
            return context.VehiclwInformation.FirstOrDefault(v => v.Id.ToString() == idVech);
        }

        internal string GetIdOrderByIdVech(string idVech)
        {
            Shipping shipping = context.Shipping
                .Include(s => s.VehiclwInformations)
                .FirstOrDefault(s => s.VehiclwInformations.FirstOrDefault(v => v.Id.ToString() == idVech) != null);
            return shipping.Id;
        }

        internal async Task<Truck> GetTruckByPlateDb(string truckPlate)
        {
            return await context.Trucks.FirstOrDefaultAsync(t => t.PlateTruk == truckPlate);
        }

        internal async Task<Trailer> GetTrailerByPlateDb(string trailerPlate)
        {
            return await context.Trailers.FirstOrDefaultAsync(t => t.Plate == trailerPlate);
        }

        internal List<DriverReport> GetDriversReportsDb(string commpanyID, string nameDriver, string driversLicense)
        {
            List<DriverReport> driverReports = new List<DriverReport>();
            if (nameDriver != null || driversLicense != null)
            {
                driverReports.AddRange(context.DriverReports.Where(d => 
                (nameDriver == d.FullName)
                && (driversLicense == d.DriversLicenseNumber)));
            }
            return driverReports;
        }

        internal int[] GetIdTruckAdnTrailarDb(string idDriver)
        {
            int[] idTruckAdnTrailar = null;
            InspectionDriver inspectionDriver = context.InspectionDrivers.Last();
            if(inspectionDriver != null)
            {
                idTruckAdnTrailar = new int[] { inspectionDriver.IdITruck, inspectionDriver.IdITrailer };
            }
            else
            {
                idTruckAdnTrailar = new int[0];
            }
            return idTruckAdnTrailar;
        }

        internal List<Trailer> GetTrailersDb()
        {
            return context.Trailers.ToList();
        }

        internal void RemoveTruck(string id)
        {
            context.Trucks.Remove(context.Trucks.FirstOrDefault(t => t.Id.ToString() == id));
            context.SaveChanges();
        }

        internal List<Truck> GetTrucs()
        {
            return context.Trucks.ToList();
        }

        private void Init()
        {
            context.Shipping.Load();
            context.VehiclwInformation.Load();
            context.Drivers.Load();
        }
        
        public async void SaveNewContact(Contact contact)
        {
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();
        }

        public List<Contact> GetContactsDB()
        {
            return context.Contacts.ToList();
        }

        public async Task<List<Driver>> GetDriversInDb()
        {
            List<Driver> drivers = null;
            drivers = await context.Drivers
                .Where(d => !d.IsFired)
                .Include(d => d.InspectionDrivers)
                .Include(d => d.geolocations)
                .ToListAsync();
            return drivers;
        }

        public async void RecurentOnDeleted(string id)
        {
            Shipping shipping = await context.Shipping.FirstOrDefaultAsync(s => s.Id == id);
            if(shipping != null)
            {
                if(shipping.CurrentStatus == "Delivered,Billed" || shipping.CurrentStatus == "Delivered,Paid")
                {
                    shipping.CurrentStatus = shipping.CurrentStatus.Replace("Delivered", "Deleted");
                }
                else
                {
                    shipping.CurrentStatus = "Deleted";
                }
                await context.SaveChangesAsync();
            }
        }

        public async void RecurentOnArchived(string id)
        {
            Shipping shipping = await context.Shipping.FirstOrDefaultAsync(s => s.Id == id);
            if (shipping != null)
            {
                if (shipping.CurrentStatus == "Delivered,Billed" || shipping.CurrentStatus == "Delivered,Paid")
                {
                    shipping.CurrentStatus = shipping.CurrentStatus.Replace("Delivered", "Archived");
                }
                else
                {
                    shipping.CurrentStatus = "Archived";
                }
                await context.SaveChangesAsync();
            }
        }

        internal int CreateTrukDb(string nameTruk, string yera, string make, string model, string typeTruk, string state, string exp, string vin, string owner, string plateTruk, string color)
        {
            Truck truck = new Truck()
            {
                ColorTruk = color,
                Exp = exp,
                Make = make,
                Model = model,
                NameTruk = nameTruk,
                Owner = owner,
                PlateTruk = plateTruk,
                Satet = state,
                Vin = vin,
                Yera = yera,
                TypeTruk = typeTruk
            };
            context.Trucks.Add(truck);
            context.SaveChanges();
            return truck.Id;
        }

        public Shipping GetShipingCurrentVehiclwInDb(string id)
        {
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id.ToString() == id);
            Shipping shipping = context.Shipping.Where(s => s.VehiclwInformations.FirstOrDefault(v => v == vehiclwInformation) != null)
                .Include(s => s.VehiclwInformations)
                .Include("VehiclwInformations.Ask")
                .Include("VehiclwInformations.Ask.Any_personal_or_additional_items_with_or_in_vehicle")
                .Include("VehiclwInformations.Ask1")
                .Include("VehiclwInformations.Ask1.App_will_force_driver_to_take_pictures_of_each_strap")
                .Include("VehiclwInformations.Ask1.Photo_after_loading_in_the_truck")
                .Include("VehiclwInformations.AskDelyvery")
                .Include("VehiclwInformations.AskDelyvery.Please_take_a_picture_Id_of_the_person_taking_the_delivery")
                .Include("VehiclwInformations.PhotoInspections.Photos")
                .Include(v => v.AskFromUser)
                .Include(v => v.AskFromUser.App_will_ask_for_signature_of_the_client_signature)
                .Include(v => v.AskFromUser.PhotoPay)
                .Include(v => v.AskFromUser.VideoRecord)
                .Include(v => v.AskFromUser.App_will_ask_for_signature_of_the_client_signature)
                .Include(v => v.askForUserDelyveryM)
                .Include(v => v.askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature)
                .Include(v => v.askForUserDelyveryM.Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up_photo)
                .Include(v => v.askForUserDelyveryM.PhotoPay)
                .Include(v => v.askForUserDelyveryM.VideoRecord)
                .Include("VehiclwInformations.Scan")
                .Include(v => v.Ask2)
                .FirstOrDefault();
            return context.Shipping.FirstOrDefault(s => s.VehiclwInformations.FirstOrDefault(v => v == vehiclwInformation) != null);
        }

        internal void RemoveTrailerDb(string id)
        {
            context.Trailers.Remove(context.Trailers.FirstOrDefault(t => t.Id.ToString() == id));
            context.SaveChanges();
        }

        public void Solved(string idOrder)
        {
            Shipping shipping = context.Shipping.First(s => s.Id == idOrder);
            shipping.IsProblem = false;
            context.SaveChanges();
        }

        public Driver GetDriver(int id)
        {
            return context.Drivers.FirstOrDefault(d => d.Id == id);
        }

        public Driver GetDriver(string idInspection)
        {
            InspectionDriver inspectionDriver = context.InspectionDrivers.First(i => i.Id.ToString() == idInspection);
            return context.Drivers
                .Include(d => d.InspectionDrivers)
                .First(d => d.InspectionDrivers.FirstOrDefault(ii => ii.Id == inspectionDriver.Id) != null);
        }

        public async void SavevechInDb(string idVech, VehiclwInformation vehiclwInformation)
        {
            VehiclwInformation vehiclwInformationDb = await context.VehiclwInformation.FirstOrDefaultAsync(v => v.Id.ToString() == idVech);
            vehiclwInformationDb.VIN = vehiclwInformation.VIN;
            vehiclwInformationDb.Year = vehiclwInformation.Year;
            vehiclwInformationDb.Make = vehiclwInformation.Make;
            vehiclwInformationDb.Model = vehiclwInformation.Model;
            vehiclwInformationDb.Type = vehiclwInformation.Type;
            vehiclwInformationDb.Color = vehiclwInformation.Color;
            vehiclwInformationDb.Lot = vehiclwInformation.Lot;
            await context.SaveChangesAsync();
        }

        internal int CreateTrailerDb(string name, string typeTrailer, string year, string make, string howLong, string vin, string owner, string color, string plate, string exp, string annualIns)
        {
            Trailer trailer = new Trailer()
            {
                AnnualIns = annualIns,
                Color = color,
                Exp = exp,
                HowLong = howLong,
                Make = make,
                Name = name,
                Owner = owner,
                Plate = plate,
                Vin = vin,
                Year = year,
                Type = typeTrailer
            };
            context.Trailers.Add(trailer);
            context.SaveChanges();
            return trailer.Id;
        }

        internal void SavePathDb(string id, string path)
        {
            //Truck truck = context.Trucks.First(t => t.Id.ToString() == id);
            //truck.PathDoc = path;
            //context.SaveChanges();
        }

        public async void RemoveVechInDb(string idVech)
        {
            context.VehiclwInformation.Remove(context.VehiclwInformation.FirstOrDefault(v => v.Id.ToString() == idVech));
            context.SaveChanges();
        }

        public async void AddOrder(Shipping shipping)
        {
            bool isCheckOrder = CheckUrlOrder(shipping);
            if (CheckOrder(shipping) && !isCheckOrder)
            {
                shipping.Id += new Random().Next(0, 1000);
            }
            try
            {
                if (!isCheckOrder)
                {
                    await context.Shipping.AddAsync(shipping);
                    await context.SaveChangesAsync();
                }
                else
                {
                }
            }
            catch (Exception e)
            {
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

        internal async Task<List<DocumentTruckAndTrailers>> GetTruckDocDB(string id)
        {
            return await context.DocumentTruckAndTrailers.Where(d => d.TypeTr == "Truck" && d.IdTr.ToString() == id).ToListAsync();
        }

        internal async Task<List<DocumentTruckAndTrailers>> GetTrailerDocDB(string id)
        {
            return await context.DocumentTruckAndTrailers.Where(d => d.TypeTr == "Trailer" && d.IdTr.ToString() == id).ToListAsync();
        }

        internal string GetDocumentDb(string id)
        {
            //string pathDoc = "";
            //Driver driver = context.Drivers
            //    .Include(d => d.InspectionDrivers)
            //    .FirstOrDefault(d => d.Id.ToString() == id);
            //if (driver.InspectionDrivers != null)
            //{
            //    InspectionDriver inspectionDriver = driver.InspectionDrivers.Last();
            //    Truck truck = context.Trucks.FirstOrDefault(t => t.Id == inspectionDriver.IdITruck);
            //    if (truck != null)
            //    {
            //        pathDoc = truck.PathDoc;
            //    }
            //}
            return "";
        }

        internal List<DocumentTruckAndTrailers> GetTraileDocDb(string id)
        {
            throw new NotImplementedException();
        }

        internal void RemoveDocDb(string idDock)
        {
            context.DocumentTruckAndTrailers.Remove(context.DocumentTruckAndTrailers.First(d => d.Id.ToString() == idDock));
            context.SaveChanges();
        }

        internal void SaveDocTruckDb(string path, string id, string nameDoc)
        {
            string pref = path.Remove(0, path.LastIndexOf(".") + 1);
            DocumentTruckAndTrailers documentTruckAndTrailers = new DocumentTruckAndTrailers()
            {
                DocPath = path,
                IdTr = Convert.ToInt32(id),
                NameDoc = nameDoc,
                TypeTr = "Truck",
                TypeDoc = pref
            };
            context.DocumentTruckAndTrailers.Add(documentTruckAndTrailers);
            context.SaveChanges();
        }

        internal void SaveDocTrailekDb(string path, string id, string nameDoc)
        {
            string pref = path.Remove(0, path.LastIndexOf(".") + 1);
            DocumentTruckAndTrailers documentTruckAndTrailers = new DocumentTruckAndTrailers()
            {
                DocPath = path,
                IdTr = Convert.ToInt32(id),
                NameDoc = nameDoc,
                TypeTr = "Trailer",
                TypeDoc = pref
            };
            context.DocumentTruckAndTrailers.Add(documentTruckAndTrailers);
            context.SaveChanges();
        }

        public async Task<VehiclwInformation> AddVechInDb(string idOrder)
        {
            Shipping shipping = await context.Shipping
                .Include(s => s.VehiclwInformations)
                .FirstOrDefaultAsync(s => s.Id.ToString() == idOrder);
            VehiclwInformation vehiclwInformation = new VehiclwInformation();
            if(shipping.VehiclwInformations == null)
            {
                shipping.VehiclwInformations = new List<VehiclwInformation>();
            }
            shipping.VehiclwInformations.Add(vehiclwInformation);
            await context.SaveChangesAsync();
            return vehiclwInformation;
        }

        internal List<HistoryOrder> GetHistoryOrderByIdOrder(string idOrder)
        {
            return context.HistoryOrders.Where(ho => ho.IdOreder.ToString() == idOrder).ToList();
        }

        public async Task<Shipping> CreateShipping()
        {
            Shipping shipping = new Shipping();
            shipping.Id = CreateIdShipping().ToString();
            shipping.CurrentStatus = "NewLoad";
            context.Shipping.Add(shipping);
            await context.SaveChangesAsync();
            Shipping shipping1 = context.Shipping.FirstOrDefault(s => s.Id == shipping.Id);
            return shipping;
        }

        public List<InspectionDriver> GetInspectionTrucksDb(string idDriver, string idTruck, string idTrailer, string date)
        {

            List<InspectionDriver> inspectionDrivers = new List<InspectionDriver>();
            context.Drivers
                .Include(d => d.InspectionDrivers).ToList()
                .Where(d => idDriver == "0" || d.Id.ToString() == idDriver)
                .ToList()
                .ForEach((item) =>
                {
                    inspectionDrivers.AddRange(item.InspectionDrivers
                        .Where(iD => (date == "0" || (Convert.ToDateTime(iD.Date).Month == Convert.ToDateTime(date).Month && Convert.ToDateTime(iD.Date).Year == Convert.ToDateTime(date).Year))
                        && (idTruck == "0" || iD.IdITruck.ToString() == idTruck)
                        && (idTrailer == "0" || iD.IdITrailer.ToString() == idTrailer)));
                });

            return inspectionDrivers;
        }

        private int CreateIdShipping()
        {
            int id = 1;
            while(context.Shipping.FirstOrDefault(s => s.Id == id.ToString()) != null) { id = new Random().Next(0, 100000000); }
            return id;
        }

        public InspectionDriver GetInspectionTruck(string idInspection)
        {
            return context.InspectionDrivers
                .Where(i => i.Id.ToString() == idInspection)
                .Include(i => i.PhotosTruck)
                .First();
        }

        public bool ExistsDataUser(string login, string password)
        {
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
            return context.User.FirstOrDefault(u => u.KeyAuthorized == key) != null;
        }

        public async Task<List<Shipping>> GetShippings(string status, int page)
        {
            List<Shipping> shipping = null;
            shipping = await context.Shipping
                .Where(s => s.CurrentStatus == status)
                .Include(s => s.Driverr)
                .ToListAsync();
            
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

        public async Task<int> GetCountPageInDb(string status)
        {
            int countPage = 0;
            List<Shipping> shipping = await context.Shipping.Where(s => s.CurrentStatus == status).ToListAsync();
            countPage = shipping.Count / 20;
            int remainderPage = shipping.Count % 20;
            countPage = remainderPage > 0 ? countPage + 1 : countPage;
            return countPage;
        }

        public bool CheckInspactionDriverToDay(int idDriver)
        {
            Driver driver = context.Drivers.Include(d => d.InspectionDrivers).FirstOrDefault(d => d.Id == idDriver);
            InspectionDriver inspectionDriver = driver.InspectionDrivers != null && driver.InspectionDrivers.Count != 0 ? driver.InspectionDrivers.Last() : null;
            if (inspectionDriver == null)
            {
                driver.IsInspectionDriver = false;
                driver.IsInspectionToDayDriver = false;
                context.SaveChanges();
            }
            else if (Convert.ToDateTime(inspectionDriver.Date).Date != DateTime.Now.Date)
            {
                if (DateTime.Now.Hour >= 12)
                {
                    driver.IsInspectionDriver = false;
                    driver.IsInspectionToDayDriver = false;
                }
                else if (DateTime.Now.Hour <= 12 && 6 >= DateTime.Now.Hour)
                {
                    driver.IsInspectionDriver = true;
                    driver.IsInspectionToDayDriver = false;
                }
                context.SaveChanges();
            }
            return driver.IsInspectionToDayDriver;
        }

        public List<Driver> GetDrivers(int page)
        {
            List<Driver> drivers = null;
            drivers = context.Drivers.Where(d => !d.IsFired).ToList();
            if (page == -1)
            {
            }
            else if(page != 0)
            {
                try
                {
                    drivers = drivers.GetRange((20 * page) - 20, 20);
                }
                catch (Exception)
                {
                    drivers = drivers.GetRange((20 * page) - 20, drivers.Count % 20);
                }
            }
            else
            {
                try
                {
                    drivers = drivers.GetRange(0, 20);
                }
                catch (Exception)
                {
                    drivers = drivers.GetRange(0, drivers.Count % 20);
                }
            }
            return drivers;
        }

        public async Task<List<VehiclwInformation>> AddDriversInOrder(string idOrder, string idDriver)
        {
            Shipping shipping = context.Shipping
                .Include(s => s.Driverr)
                .Include(s => s.VehiclwInformations)
                .FirstOrDefault(s => s.Id == idOrder);
            Driver driver = context.Drivers.FirstOrDefault(d => d.Id == Convert.ToInt32(idDriver));
            shipping.Driverr = driver;
            shipping.CurrentStatus = "Assigned";
            await context.SaveChangesAsync();
            return shipping.VehiclwInformations;
        }

        public bool CheckDriverOnShipping(string idShipping)
        {
            bool isDriverAssign = false;
            Shipping shipping = context.Shipping
                .Include(s => s.Driverr)
                .FirstOrDefault(s => s.Id == idShipping);
            if(shipping.Driverr != null)
            {
                isDriverAssign = true;
            }
            return isDriverAssign;
        }

        public string GerShopToken(string idDriver)
        {
            Driver driver = context.Drivers.FirstOrDefault<Driver>(d => d.Id == Convert.ToInt32(idDriver));
            return driver.TokenShope;
        }

        public string GerShopTokenForShipping(string idOrder)
        {
            Shipping shipping = context.Shipping
                .Include(s => s.Driverr)
                .FirstOrDefault(d => d.Id == idOrder);
            return shipping.Driverr.TokenShope;
        }

        public async Task<List<VehiclwInformation>> RemoveDriversInOrder(string idOrder)
        {
            Shipping shipping = context.Shipping
                .Include(s => s.Driverr)
                .Include(s => s.VehiclwInformations)
                .FirstOrDefault(s => s.Id == idOrder);
            shipping.Driverr = null;
            shipping.CurrentStatus = "NewLoad";
            await context.SaveChangesAsync();
            return shipping.VehiclwInformations;
        }

        public Shipping GetShipping(string id)
        {
            return context.Shipping.Include(s => s.VehiclwInformations).FirstOrDefault(s => s.Id == id);
        }

        public async void UpdateorderInDb(string idOrder, string idLoad, string internalLoadID, string driver, string status, string instructions, string nameP, string contactP,
            string addressP, string cityP, string stateP, string zipP, string phoneP, string emailP, string scheduledPickupDateP, string nameD, string contactD, string addressD,
            string cityD, string stateD, string zipD, string phoneD, string emailD, string ScheduledPickupDateD, string paymentMethod, string price, string paymentTerms, string brokerFee)
        {
            Shipping shipping = context.Shipping.FirstOrDefault(s => s.Id == idOrder);
            shipping.idOrder = idLoad != null ? idLoad : shipping.Id;
            shipping.InternalLoadID = internalLoadID != null ? internalLoadID : shipping.InternalLoadID;
            //shipping.Driverr = internalLoadID != null ? internalLoadID : shipping.InternalLoadID;
            if(status == "Delivered")
            {
                shipping.CurrentStatus = shipping.CurrentStatus.Replace("Deleted", "Delivered");
            }
            else if(status == "Archived")
            {
                shipping.CurrentStatus = shipping.CurrentStatus.Replace("Archived", "Delivered");
            }
            else
            {
                shipping.CurrentStatus = status != null ? status : shipping.CurrentStatus;
            }
            shipping.Titl1DI = instructions != null ? instructions : shipping.Titl1DI;
            shipping.NameP = nameP != null ? nameP : shipping.NameD;
            shipping.ContactNameP = contactP != null ? contactP : shipping.ContactNameP;
            shipping.AddresP = addressP != null ? addressP : shipping.AddresP;
            shipping.CityP = cityP != null ? cityP : shipping.CityP;
            shipping.StateP = stateP != null ? stateP : shipping.StateP;
            shipping.ZipP = zipP != null ? zipP : shipping.ZipP;
            shipping.PhoneP = phoneP != null ? phoneP : shipping.PhoneP;
            shipping.EmailP = emailP != null ? emailP : shipping.EmailP;
            shipping.PickupExactly = scheduledPickupDateP != null ? scheduledPickupDateP : shipping.PickupExactly;
            shipping.NameD = nameD != null ? nameD : shipping.NameD;
            shipping.ContactNameD = contactD != null ? contactD : shipping.ContactNameD;
            shipping.AddresD = addressD != null ? addressD : shipping.AddresD;
            shipping.CityD = cityD != null ? cityD : shipping.CityD;
            shipping.StateD = stateD != null ? stateD : shipping.StateD;
            shipping.ZipD = zipD != null ? zipD : shipping.ZipD;
            shipping.PhoneD = phoneD != null ? phoneD : shipping.PhoneD;
            shipping.EmailD = emailD != null ? emailD : shipping.EmailD;
            shipping.DeliveryEstimated = ScheduledPickupDateD != null ? ScheduledPickupDateD : shipping.DeliveryEstimated;
            shipping.TotalPaymentToCarrier = paymentMethod != null ? paymentMethod : shipping.TotalPaymentToCarrier;
            shipping.PriceListed = price != null ? price : shipping.PriceListed;
            shipping.BrokerFee = brokerFee != null ? brokerFee : shipping.BrokerFee;
            await context.SaveChangesAsync();
        }

        public async void CreateOrderInDb(string idOrder, string idLoad, string internalLoadID, string driver, string status, string instructions, string nameP, string contactP,
            string addressP, string cityP, string stateP, string zipP, string phoneP, string emailP, string scheduledPickupDateP, string nameD, string contactD, string addressD,
            string cityD, string stateD, string zipD, string phoneD, string emailD, string ScheduledPickupDateD, string paymentMethod, string price, string paymentTerms, string brokerFee)
        {
            Init();
            Shipping shipping = new Shipping();
            shipping.idOrder = idLoad != null ? idLoad : shipping.Id;
            shipping.InternalLoadID = internalLoadID != null ? internalLoadID : shipping.InternalLoadID;
            //shipping.Driverr = internalLoadID != null ? internalLoadID : shipping.InternalLoadID;
            shipping.CurrentStatus = status != null ? status : shipping.CurrentStatus;
            shipping.Titl1DI = instructions != null ? instructions : shipping.Titl1DI;
            shipping.NameP = nameP != null ? nameP : shipping.NameD;
            shipping.ContactNameP = contactP != null ? contactP : shipping.ContactNameP;
            shipping.AddresP = addressP != null ? addressP : shipping.AddresP;
            shipping.CityP = cityP != null ? cityP : shipping.CityP;
            shipping.StateP = stateP != null ? stateP : shipping.StateP;
            shipping.ZipP = zipP != null ? zipP : shipping.ZipP;
            shipping.PhoneP = phoneP != null ? phoneP : shipping.PhoneP;
            shipping.EmailP = emailP != null ? emailP : shipping.EmailP;
            shipping.PickupExactly = scheduledPickupDateP != null ? scheduledPickupDateP : shipping.PickupExactly;
            shipping.NameD = nameD != null ? nameD : shipping.NameD;
            shipping.ContactNameD = contactD != null ? contactD : shipping.ContactNameD;
            shipping.AddresD = addressD != null ? addressD : shipping.AddresD;
            shipping.CityD = cityD != null ? cityD : shipping.CityD;
            shipping.StateD = stateD != null ? stateD : shipping.StateD;
            shipping.ZipD = zipD != null ? zipD : shipping.ZipD;
            shipping.PhoneD = phoneD != null ? phoneD : shipping.PhoneD;
            shipping.EmailD = emailD != null ? emailD : shipping.EmailD;
            shipping.DeliveryEstimated = ScheduledPickupDateD != null ? ScheduledPickupDateD : shipping.DeliveryEstimated;
            shipping.TotalPaymentToCarrier = paymentMethod != null ? paymentMethod : shipping.TotalPaymentToCarrier;
            shipping.PriceListed = price != null ? price : shipping.PriceListed;
            shipping.BrokerFee = brokerFee != null ? brokerFee : shipping.BrokerFee;
            context.Shipping.Add(shipping);
            await context.SaveChangesAsync();
        }

        public async void AddDriver(Driver driver)
        {
            await context.Drivers.AddAsync(driver);
            await context.SaveChangesAsync();
        }



        public async void UpdateDriver(Driver driver)
        {
            context.Drivers.Update(driver);
            await context.SaveChangesAsync();
        }

        public void RemoveDriveInDb(int id, string comment)
        {
            Driver driver = context.Drivers
                .FirstOrDefault(d => d.Id == id);
            driver.IsFired = true;
            DriverReport driverReport = new DriverReport()
            {
                Comment = comment,
                DriversLicenseNumber = driver.DriversLicenseNumber,
                FullName = driver.FullName,
                IdDriver = driver.Id,
                DateRegistration = driver.DateRegistration,
                DateFired = DateTime.Now.ToString()
            };
            context.DriverReports.Add(driverReport);
            context.SaveChanges();
        }

        public async void RestoreDriveInDb(int id)
        {
            Driver driver = context.Drivers.FirstOrDefault(d => d.Id == id);
            driver.IsFired = false;
            await context.SaveChangesAsync();
        }
    }
}