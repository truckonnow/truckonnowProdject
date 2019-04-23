using DaoModels.DAO;
using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;
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
            InitUserOne();
        }

        private async void InitUserOne()
        {
            if (context.User.Count() == 0)
            {
                Users users = new Users();
                users.Login = "DevRoma";
                users.Password = "truckon777";
                context.User.AddAsync(users);
                users = new Users();
                users.Login = "ArtemManager";
                users.Password = "truckon777";
                context.User.AddAsync(users);
                users = new Users();
                users.Login = "Designer";
                users.Password = "truckon777";
                context.User.AddAsync(users);
                users = new Users();
                users.Login = "Truckonnow";
                users.Password = "truckon777";
                context.User.AddAsync(users);
                users = new Users();
                users.Login = "Truckonnow1";
                users.Password = "truckon777";
                context.User.AddAsync(users);
                users = new Users();
                users.Login = "Truckonnow2";
                users.Password = "truckon777";
                context.User.AddAsync(users);
                users = new Users();
                users.Login = "Truckonnow3";
                users.Password = "truckon777";
                context.User.AddAsync(users);
                await context.SaveChangesAsync();
            }
        }

        private void Init()
        {
            context.Shipping.Load();
            context.VehiclwInformation.Load();
            context.Drivers.Load();
        }
        
        public List<Driver> GetDriversInDb()
        {
            context.geolocations.Load();
            return context.Drivers.ToList();
        }

        public async void RecurentOnDeleted(string id)
        {
            context.Shipping.Load();
            Shipping shipping = await context.Shipping.FirstOrDefaultAsync(s => s.Id == id);
            if(shipping != null)
            {
                shipping.CurrentStatus = "Deleted";
                await context.SaveChangesAsync();
            }
        }

        public async void RecurentOnArchived(string id)
        {
            context.Shipping.Load();
            Shipping shipping = await context.Shipping.FirstOrDefaultAsync(s => s.Id == id);
            if (shipping != null)
            {
                shipping.CurrentStatus = "Archived";
                await context.SaveChangesAsync();
            }
        }

        public Shipping GetShipingCurrentVehiclwInDb(string id)
        {
            context.Shipping.Load();
            context.VehiclwInformation.Load();
            context.PhotoInspections.Load();
            context.Photos.Load();
            context.Asks.Load();
            VehiclwInformation vehiclwInformation = context.VehiclwInformation.FirstOrDefault(v => v.Id.ToString() == id);
            return context.Shipping.FirstOrDefault(s => s.VehiclwInformations.FirstOrDefault(v => v == vehiclwInformation) != null);
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

        public async void RemoveVechInDb(string idVech)
        {
            context.VehiclwInformation.Remove(await context.VehiclwInformation.FirstOrDefaultAsync(v => v.Id.ToString() == idVech));
            await context.SaveChangesAsync();
        }

        public async Task<VehiclwInformation> AddVechInDb(string idOrder)
        {
            context.Shipping.Load();
            context.VehiclwInformation.Load();
            Shipping shipping = await context.Shipping.FirstOrDefaultAsync(s => s.Id.ToString() == idOrder);
            VehiclwInformation vehiclwInformation = new VehiclwInformation();
            if(shipping.VehiclwInformations == null)
            {
                shipping.VehiclwInformations = new List<VehiclwInformation>();
            }
            shipping.VehiclwInformations.Add(vehiclwInformation);
            await context.SaveChangesAsync();
            return vehiclwInformation;
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

        private int CreateIdShipping()
        {
            int id = 1;
            while(context.Shipping.FirstOrDefault(s => s.Id == id.ToString()) != null) { id = new Random().Next(0, 100000000); }
            return id;
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

        public int GetCountPageInDb(string status)
        {
            Init();
            int countPage = 0;
            List<Shipping> shipping = context.Shipping.ToList().FindAll(s => s.CurrentStatus == status);
            countPage = shipping.Count / 20;
            int remainderPage = shipping.Count % 20;
            countPage = remainderPage > 0 ? countPage + 1 : countPage;
            return countPage;
        }

        public List<Driver> GetDrivers(int page)
        {
            Init();
            List<Driver> drivers = null;
            drivers = context.Drivers.ToList();

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
            Init();
            Shipping shipping = context.Shipping.FirstOrDefault<Shipping>(s => s.Id == idOrder);
            Driver driver = context.Drivers.FirstOrDefault<Driver>(d => d.Id == Convert.ToInt32(idDriver));
            shipping.Driverr = driver;
            shipping.CurrentStatus = "Assigned";
            await context.SaveChangesAsync();
            return shipping.VehiclwInformations;
        }

        public bool CheckDriverOnShipping(string idShipping)
        {
            bool isDriverAssign = false;
            context.Drivers.Load();
            Shipping shipping = context.Shipping.FirstOrDefault<Shipping>(s => s.Id == idShipping);
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
            context.Drivers.Load();
            Shipping shipping = context.Shipping.FirstOrDefault<Shipping>(d => d.Id == idOrder);
            return shipping.Driverr.TokenShope;
        }

        public async Task<List<VehiclwInformation>> RemoveDriversInOrder(string idOrder)
        {
            Init();
            Shipping shipping = context.Shipping.FirstOrDefault<Shipping>(s => s.Id == idOrder);
            shipping.Driverr = null;
            shipping.CurrentStatus = "NewLoad";
            await context.SaveChangesAsync();
            return shipping.VehiclwInformations;
        }

        public Shipping GetShipping(string id)
        {
            Init();
            return context.Shipping.FirstOrDefault(s => s.Id == id);
        }

        public async void UpdateorderInDb(string idOrder, string idLoad, string internalLoadID, string driver, string status, string instructions, string nameP, string contactP,
            string addressP, string cityP, string stateP, string zipP, string phoneP, string emailP, string scheduledPickupDateP, string nameD, string contactD, string addressD,
            string cityD, string stateD, string zipD, string phoneD, string emailD, string ScheduledPickupDateD, string paymentMethod, string price, string paymentTerms, string brokerFee)
        {
            Init();
            Shipping shipping = context.Shipping.FirstOrDefault(s => s.Id == idOrder);
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

        public async void RemoveDriveInDb(int id)
        {
            context.Drivers.Remove(context.Drivers.FirstOrDefault(d => d.Id == id));
            await context.SaveChangesAsync();
        }
    }
}