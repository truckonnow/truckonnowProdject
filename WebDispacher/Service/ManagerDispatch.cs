using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebDispacher.Dao;
using WebDispacher.Notify;
using WebDispacher.Service.EmailSmtp;

namespace WebDispacher.Service
{
    public class ManagerDispatch
    {
        public SqlCommadWebDispatch _sqlEntityFramworke = null;

        public ManagerDispatch()
        {
            _sqlEntityFramworke = new SqlCommadWebDispatch();
        }
        
        public async Task<List<Driver>> GetDrivers()
        {
            return await _sqlEntityFramworke.GetDriversInDb();
        }

        public void DeletedOrder(string id)
        {
            _sqlEntityFramworke.RecurentOnDeleted(id);
        }

        public void ArchvedOrder(string id)
        {
            _sqlEntityFramworke.RecurentOnArchived(id);
        }

        internal async Task AddNewOrder(string urlPage)
        {
            GetDataCentralDispatch getDataCentralDispatch = new GetDataCentralDispatch();
            Shipping shipping = await getDataCentralDispatch.GetShipping(urlPage);
            _sqlEntityFramworke.AddOrder(shipping);
        }

        public List<Truck> GetTrucks()
        {
            return _sqlEntityFramworke.GetTrucs();
        }

        public List<Contact> GetContacts()
        {
            return _sqlEntityFramworke.GetContactsDB();
        }

        internal int[] GetIdTruckAdnTrailar(string idDriver)
        {
            return _sqlEntityFramworke.GetIdTruckAdnTrailarDb(idDriver);
        }

        internal async Task<Trailer> GetTrailer(string idDriver)
        {
            return await _sqlEntityFramworke.GetTrailerDb(idDriver);
        }

        internal async Task<Truck> GetTruck(string idDriver)
        {
            return await _sqlEntityFramworke.GetTruckDb(idDriver);
        }

        public async void SaveVechi(string idVech, string VIN, string Year, string Make, string Model, string Type, string Color, string LotNumber)
        {
            VehiclwInformation vehiclwInformation = new VehiclwInformation();
            vehiclwInformation.VIN = VIN;
            vehiclwInformation.Year = Year;
            vehiclwInformation.Make = Make;
            vehiclwInformation.Model = Model;
            vehiclwInformation.Type = Type;
            vehiclwInformation.Color = Color;
            vehiclwInformation.Lot = LotNumber;
            _sqlEntityFramworke.SavevechInDb(idVech, vehiclwInformation);
        }

        internal async Task<Truck> GetTruckByPlate(string truckPlate)
        {
            return await _sqlEntityFramworke.GetTruckByPlateDb(truckPlate);
        }

        internal int CheckTokenFoDriver(string idDriver, string token)
        {
            return _sqlEntityFramworke.CheckTokenFoDriverDb(idDriver, token);
        }

        internal async Task<int> ResetPasswordFoDriver(string newPassword, string idDriver, string token)
        {
            int isStateActual = _sqlEntityFramworke.ResetPasswordFoDriver(newPassword, idDriver, token);
            if(isStateActual == 2)
            {
                string emailDriver = _sqlEntityFramworke.GetEmailDriverDb(idDriver);
                string patern = new PaternSourse().GetPaternDataAccountDriver(emailDriver, newPassword);
                await new AuthMessageSender().Execute(emailDriver, "Password changed successfully", patern);
            }
            else
            {
                string emailDriver = _sqlEntityFramworke.GetEmailDriverDb(idDriver);
                string patern = new PaternSourse().GetPaternNoRestoreDataAccountDriver();
                await new AuthMessageSender().Execute(emailDriver, "Password reset attempt failed", patern);
            }
            return isStateActual;
        }

        internal void AddHistory(string key, string idConmpany, string idOrder, string idVech, string idDriver, string action)
        {
            HistoryOrder historyOrder = new HistoryOrder();
            int idUser = _sqlEntityFramworke.GetUserIdByKey(key);
            if(action == "Assign")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                //string fullNameDriver = _sqlEntityFramworke.GetFullNameDriverById(idDriver);
                //historyOrder.Action = $"{fullNameUser} assign the driver ordered {fullNameDriver}";
                historyOrder.TypeAction = "Assign";
            }
            else if(action == "Unassign")
            {
                idDriver = _sqlEntityFramworke.GetDriverIdByIdOrder(idOrder);
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                //string fullNameDriver = _sqlEntityFramworke.GetFullNameDriverById(idDriver);
                //historyOrder.Action = $"{fullNameUser} withdrew an order from {fullNameDriver} driver";
                historyOrder.TypeAction = "Unassign";
            }
            else if (action == "Solved")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                //historyOrder.Action = $"{fullNameUser} clicked on the \"Solved\" button";
                historyOrder.TypeAction = "Solved";
            }
            else if (action == "ArchivedOrder")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                //historyOrder.Action = $"{fullNameUser} transferred the order to the archive";
                historyOrder.TypeAction = "ArchivedOrder";
            }
            else if (action == "DeletedOrder")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                //historyOrder.Action = $"{fullNameUser} transferred the order to deleted orders";
                historyOrder.TypeAction = "DeletedOrder";
            }
            else if (action == "Creat")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                //historyOrder.Action = $"{fullNameUser} created an order";
                historyOrder.TypeAction = "Creat";
            }
            else if (action == "SavaOrder")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                //historyOrder.Action = $"{fullNameUser} edited the order";
                historyOrder.TypeAction = "SavaOrder";
            }
            else if (action == "SavaVech")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                idOrder = _sqlEntityFramworke.GetIdOrderByIdVech(idVech);
                //VehiclwInformation vehiclwInformation = _sqlEntityFramworke.GetVechById(idVech);
                //historyOrder.Action = $"{fullNameUser} edited the vehicle {vehiclwInformation.Year} {vehiclwInformation.Make} {vehiclwInformation.Make}";
                historyOrder.TypeAction = "SavaVech";
            }
            else if (action == "RemoveVech")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                idOrder = _sqlEntityFramworke.GetIdOrderByIdVech(idVech);
                //VehiclwInformation vehiclwInformation = _sqlEntityFramworke.GetVechById(idVech);
                //historyOrder.Action = $"{fullNameUser} removed the vehicle {vehiclwInformation.Year} {vehiclwInformation.Make} {vehiclwInformation.Make}";
                historyOrder.TypeAction = "RemoveVech";
            }
            else if (action == "AddVech")
            {
                //string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                //historyOrder.Action = $"{fullNameUser} created a vehicle";
                historyOrder.TypeAction = "AddVech";
            }

            historyOrder.IdConmpany = Convert.ToInt32(idConmpany);
            historyOrder.IdDriver = Convert.ToInt32(idDriver);
            historyOrder.IdOreder = Convert.ToInt32(idOrder);
            historyOrder.IdVech = Convert.ToInt32(idVech);
            historyOrder.IdUser = idUser;
            historyOrder.DateAction = DateTime.Now.ToString();
            _sqlEntityFramworke.AddHistory(historyOrder);
        }

        public string GetStrAction(string key, string idConmpany, string idOrder, string idVech, string idDriver, string action)
        {
            string strAction = "";
            //int idUser = _sqlEntityFramworke.GetUserIdByKey(key);
            if (action == "Assign")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                string fullNameDriver = _sqlEntityFramworke.GetFullNameDriverById(idDriver);
                strAction = $"{fullNameUser} assign the driver ordered {fullNameDriver}";
            }
            else if (action == "Unassign")
            {
                idDriver = _sqlEntityFramworke.GetDriverIdByIdOrder(idOrder);
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                string fullNameDriver = _sqlEntityFramworke.GetFullNameDriverById(idDriver);
                strAction = $"{fullNameUser} withdrew an order from {fullNameDriver} driver";
            }
            else if (action == "Solved")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                strAction = $"{fullNameUser} clicked on the \"Solved\" button";
            }
            else if (action == "ArchivedOrder")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                strAction = $"{fullNameUser} transferred the order to the archive";
            }
            else if (action == "DeletedOrder")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                strAction = $"{fullNameUser} transferred the order to deleted orders";
            }
            else if (action == "Creat")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                strAction = $"{fullNameUser} created an order";
            }
            else if (action == "SavaOrder")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                strAction = $"{fullNameUser} edited the order";
            }
            else if (action == "SavaVech")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                VehiclwInformation vehiclwInformation = _sqlEntityFramworke.GetVechById(idVech);
                strAction = $"{fullNameUser} edited the vehicle {vehiclwInformation.Year} y. {vehiclwInformation.Make} {vehiclwInformation.Model}";
            }
            else if (action == "RemoveVech")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                VehiclwInformation vehiclwInformation = _sqlEntityFramworke.GetVechById(idVech);
                strAction = $"{fullNameUser} removed the vehicle {vehiclwInformation.Year} y. {vehiclwInformation.Make} {vehiclwInformation.Make}";
            }
            else if (action == "AddVech")
            {
                string fullNameUser = _sqlEntityFramworke.GetFullNameUserByKey(key);
                strAction = $"{fullNameUser} created a vehicle";
            }
            return strAction;
        }

        internal int CheckReportDriver(string fullName, string driversLicenseNumber)
        {
            return _sqlEntityFramworke.CheckReportDriverDb(fullName, driversLicenseNumber);
        }

        internal async Task<Trailer> GetTrailerkByPlate(string trailerPlate)
        {
            return await _sqlEntityFramworke.GetTrailerByPlateDb(trailerPlate);
        }

        internal List<DriverReport> GetDriversReport(string commpanyID, string nameDriver, string driversLicense)
        {
            return _sqlEntityFramworke.GetDriversReportsDb(commpanyID, nameDriver, driversLicense);
        }

        internal void AddNewReportDriver(string fullName, string driversLicenseNumber, string description)
        {
            _sqlEntityFramworke.AddNewReportDriverDb(fullName, driversLicenseNumber, description);
        }

        internal List<Trailer> GetTrailers()
        {
            return _sqlEntityFramworke.GetTrailersDb();
        }

        internal void RemoveTruck(string id)
        {
            _sqlEntityFramworke.RemoveTruck(id);
        }

        public async void RemoveVechi(string idVech)
        {
            _sqlEntityFramworke.RemoveVechInDb(idVech);
        }

        public async Task<VehiclwInformation> AddVechi(string idOrder)
        {
            return await _sqlEntityFramworke.AddVechInDb(idOrder);
        }

        public async Task<Shipping> CreateShiping()
        {
            return await _sqlEntityFramworke.CreateShipping();
        }

        public Shipping GetShipingCurrentVehiclwIn(string id)
        {
            return _sqlEntityFramworke.GetShipingCurrentVehiclwInDb(id);
        }

        public bool Avthorization(string login, string password)
        {
            return _sqlEntityFramworke.ExistsDataUser(login, password);
        }

        public bool CheckKey(string key)
        {
            return key != null && _sqlEntityFramworke.CheckKeyDb(key);
        }

        public List<Driver> GetDrivers(int pag)
        {
            return _sqlEntityFramworke.GetDrivers(pag);
        }

        public async void Assign(string idOrder, string idDriver)
        {
            bool isDriverAssign = _sqlEntityFramworke.CheckDriverOnShipping(idOrder);
            string tokenShope = null;
            if (isDriverAssign)
            {
                tokenShope = _sqlEntityFramworke.GerShopTokenForShipping(idOrder);
            }
            List<VehiclwInformation> vehiclwInformations = await _sqlEntityFramworke.AddDriversInOrder(idOrder, idDriver);
             Task.Run(() =>
            {
                ManagerNotifyWeb managerNotify = new ManagerNotifyWeb();
                string tokenShope1 = _sqlEntityFramworke.GerShopTokenForShipping(idOrder);
                if (!isDriverAssign)
                {
                    managerNotify.SendNotyfyAssign(idOrder, tokenShope1, vehiclwInformations);
                }
                else
                {
                    managerNotify.SendNotyfyAssign(idOrder, tokenShope1, vehiclwInformations);
                    managerNotify.SendNotyfyUnassign(idOrder, tokenShope, vehiclwInformations);
                }
            });
        }

        public void CreateTruk(string nameTruk, string yera, string make, string model, string state, string exp, string vin, string owner, string plateTruk, string color, IFormFile registrationDoc, IFormFile ensuresDoc, IFormFile _3Doc)
        {
            int id = _sqlEntityFramworke.CreateTrukDb(nameTruk, yera, make, model, state, exp, vin, owner, plateTruk, color);
            Task.Run(() =>
            {
                SaveDocTruck(registrationDoc, "Registration", id.ToString());
                SaveDocTruck(ensuresDoc, "Inshurance", id.ToString());
                SaveDocTruck(_3Doc, "3", id.ToString());
            });
        }

        public void CreateContact(string fullName, string emailAddress, string phoneNumbe)
        {
            Contact contact = new Contact();
            contact.Email = emailAddress;
            contact.Name = fullName;
            contact.Phone = phoneNumbe;
            contact.Phone = phoneNumbe;
            _sqlEntityFramworke.SaveNewContact(contact);
        }

        public async void Unassign(string idOrder)
        {
            ManagerNotifyWeb managerNotify = new ManagerNotifyWeb();
            string tokenShope = _sqlEntityFramworke.GerShopTokenForShipping(idOrder);
            List<VehiclwInformation> vehiclwInformations = await _sqlEntityFramworke.RemoveDriversInOrder(idOrder);
            Task.Run(() =>
            {
                managerNotify.SendNotyfyUnassign(idOrder, tokenShope, vehiclwInformations);
            });
        }

        public void Solved(string idOrder)
        {
            _sqlEntityFramworke.Solved(idOrder);
        }

        //public Shipping GetDriver()
        //{
        //    return _sqlEntityFramworke.GetShipping(id);
        //}

        public int Createkey(string login, string password)
        {
            Random random = new Random();
            int key = random.Next(1000, 1000000000);
            _sqlEntityFramworke.SaveKeyDatabays(login, password, key);
            return key;
        }

        internal void RestoreDrive(int id)
        {
            _sqlEntityFramworke.RestoreDriveInDb(id);
        }

        public async Task<int> GetCountPage(string status)
        {
            return await _sqlEntityFramworke.GetCountPageInDb(status);
        }

        internal void RemoveTrailer(string id)
        {
            _sqlEntityFramworke.RemoveTrailerDb(id);
        }

        public async Task<List<Shipping>> GetOrders(string status, int page)
        {
            return await _sqlEntityFramworke.GetShippings(status, page);
        }

        public Shipping GetOrder(string id)
        {
            return _sqlEntityFramworke.GetShipping(id);
        }

        public Driver GetDriver(int id)
        {
            return _sqlEntityFramworke.GetDriver(id);
        }

        public Driver GetDriver(string idInspection)
        {
            return _sqlEntityFramworke.GetDriver(idInspection);
        }


        public void Updateorder(string idOrder, string idLoad, string internalLoadID, string driver, string status, string instructions, string nameP, string contactP,
            string addressP, string cityP, string stateP, string zipP, string phoneP, string emailP, string scheduledPickupDateP, string nameD, string contactD, string addressD,
            string cityD, string stateD, string zipD, string phoneD, string emailD, string ScheduledPickupDateD, string paymentMethod, string price, string paymentTerms, string brokerFee)
        {
            _sqlEntityFramworke.UpdateorderInDb(idOrder, idLoad, internalLoadID, driver, status, instructions, nameP, contactP, addressP, cityP, stateP, zipP,
                        phoneP, emailP, scheduledPickupDateP, nameD, contactD, addressD, cityD, stateD, zipD, phoneD, emailD, ScheduledPickupDateD, paymentMethod,
                        price, paymentTerms, brokerFee);
        }

        public void CreateOrder(string idOrder, string idLoad, string internalLoadID, string driver, string status, string instructions, string nameP, string contactP,
            string addressP, string cityP, string stateP, string zipP, string phoneP, string emailP, string scheduledPickupDateP, string nameD, string contactD, string addressD,
            string cityD, string stateD, string zipD, string phoneD, string emailD, string ScheduledPickupDateD, string paymentMethod, string price, string paymentTerms, string brokerFee)
        {
            _sqlEntityFramworke.UpdateorderInDb(idOrder, idLoad, internalLoadID, driver, status, instructions, nameP, contactP, addressP, cityP, stateP, zipP,
                        phoneP, emailP, scheduledPickupDateP, nameD, contactD, addressD, cityD, stateD, zipD, phoneD, emailD, ScheduledPickupDateD, paymentMethod,
                        price, paymentTerms, brokerFee);
        }

        internal void CreateTrailer(string name, string year, string make, string howLong, string vin, string owner, string color, string plate, string exp, string annualIns, IFormFile registrationDoc, IFormFile ensuresDoc, IFormFile _3Doc)
        {
            int id = _sqlEntityFramworke.CreateTrailerDb(name, year, make, howLong, vin, owner, color, plate, exp, annualIns);
            Task.Run(() =>
            {
                SaveDocTrailer(registrationDoc, "Registration", id.ToString());
                SaveDocTrailer(ensuresDoc, "Inshurance", id.ToString());
                SaveDocTrailer(_3Doc, "3", id.ToString());
            });
        }

        public void CreateDriver(string fullName, string emailAddress, string password, string phoneNumbe, string trailerCapacity, string driversLicenseNumber)
        {
            Driver driver = new Driver();
            driver.FullName = fullName;
            driver.EmailAddress = emailAddress;
            driver.Password = password;
            driver.PhoneNumber = phoneNumbe;
            driver.TrailerCapacity = trailerCapacity;
            driver.DriversLicenseNumber = driversLicenseNumber;
            driver.DateRegistration = DateTime.Now.ToString();
            _sqlEntityFramworke.AddDriver(driver);
        }

        internal void SavePath(string id, string path)
        {
            _sqlEntityFramworke.SavePathDb(id, path);
        }

        public List<InspectionDriver> GetInspectionTrucks(string idDriver, string idTruck, string idTrailer, string date)
        {
            return _sqlEntityFramworke.GetInspectionTrucksDb(idDriver, idTruck, idTrailer, date);
        }

        public void RemoveDrive(int id, string comment)
        {
            _sqlEntityFramworke.RemoveDriveInDb(id, comment);
        }

        internal async Task<string> GetDocument(string id)
        {
            return _sqlEntityFramworke.GetDocumentDb(id);
        }

        public void EditDrive(int id, string fullName, string emailAddress, string password, string phoneNumbe, string trailerCapacity, string driversLicenseNumber)
        {
            Driver driver = new Driver();
            driver.Id = id;
            driver.FullName = fullName;
            driver.EmailAddress = emailAddress;
            driver.Password = password;
            driver.PhoneNumber = phoneNumbe;
            driver.TrailerCapacity = trailerCapacity;
            driver.DriversLicenseNumber = driversLicenseNumber;
            _sqlEntityFramworke.UpdateDriver(driver);
        }

        public async Task<List<DocumentTruckAndTrailers>> GetTruckDoc(string id)
        {
            return await _sqlEntityFramworke.GetTruckDocDB(id);
        }

        public InspectionDriver GetInspectionTruck(string idInspection)
        {
            return _sqlEntityFramworke.GetInspectionTruck(idInspection);
        }

        internal void SaveDocTruck(IFormFile uploadedFile, string nameDoc, string id)
        {
            string path = $"../Document/Truck/{id}/" + uploadedFile.FileName;
            if(!Directory.Exists("../Document/Truck"))
            {
                Directory.CreateDirectory($"../Document/Truck");
            }
            if (!Directory.Exists($"../Document/Truck/{id}"))
            {
                Directory.CreateDirectory($"../Document/Truck/{id}");
            }
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }
            _sqlEntityFramworke.SaveDocTruckDb(path, id, nameDoc);
        }

        internal void SaveDocTrailer(IFormFile uploadedFile, string nameDoc, string id)
        {
            string path = $"../Document/Traile/{id}/" + uploadedFile.FileName;
            if (!Directory.Exists("../Document/Traile"))
            {
                Directory.CreateDirectory($"../Document/Traile");
            }
            if (!Directory.Exists($"../Document/Traile/{id}"))
            {
                Directory.CreateDirectory($"../Document/Traile/{id}");
            }
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }
            _sqlEntityFramworke.SaveDocTrailekDb(path, id, nameDoc);
        }

        internal async Task<List<DocumentTruckAndTrailers>> GetTraileDoc(string id)
        {
            return await _sqlEntityFramworke.GetTrailerDocDB(id);
        }

        internal void RemoveDoc(string idDock)
        {
            _sqlEntityFramworke.RemoveDocDb(idDock);
        }

        internal bool SendRemindInspection(int idDriver)
        {
            ManagerNotifyWeb managerNotifyWeb = new ManagerNotifyWeb();
            bool isInspactionDriverToDay = _sqlEntityFramworke.CheckInspactionDriverToDay(idDriver);
            if (!isInspactionDriverToDay)
            {
                string tokenShiping = _sqlEntityFramworke.GerShopToken(idDriver.ToString());
                managerNotifyWeb.SendSendNotyfyRemindInspection(tokenShiping);
            }
            return isInspactionDriverToDay;
        }

        internal List<HistoryOrder> GetHistoryOrder(string idOrder)
        {
            return _sqlEntityFramworke.GetHistoryOrderByIdOrder(idOrder);
        }
    }
}