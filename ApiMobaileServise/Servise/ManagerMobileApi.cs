using ApiMobaileServise.Notify;
using ApiMobaileServise.Servise.AddDamage;
using DaoModels.DAO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ApiMobaileServise.Servise
{
    public class ManagerMobileApi
    {
        SqlCommandApiMobile sqlCommandApiMobile = null;
          
        public ManagerMobileApi()
        {
            sqlCommandApiMobile = new SqlCommandApiMobile();
            CheckAndCreatedFolder();
        }

        public void SetInspectionDriver(string idDriver, string inspectionDriverStr)
        {
            InspectionDriver inspectionDriver = JsonConvert.DeserializeObject<InspectionDriver>(inspectionDriverStr);
            sqlCommandApiMobile.SetInspectionDriverInDb(idDriver, inspectionDriver);
        }

        public void SaveInspactionDriver(string idDriver, string photoJson, int indexPhoto)
        {
            Photo photo = JsonConvert.DeserializeObject<Photo>(photoJson);
            sqlCommandApiMobile.SaveInspectionDriverInDb(idDriver, photo, indexPhoto);
        }

        public void UpdateInspectionDriver(string idDriver)
        {
            sqlCommandApiMobile.UpdateInspectionDriver(idDriver);
        }

        public async Task<bool> ChechToDayInspaction(string token)
        {
            return await sqlCommandApiMobile.ChechToDayInspactionInDb(token);
        }

        public void SaveGPSLocationData(string token, string longitude, string latitude)
        {
            Geolocations geolocations = new Geolocations();
            geolocations.Latitude = latitude;
            geolocations.Longitude = longitude;
            sqlCommandApiMobile.SaveGPSLocationData(token, geolocations);
        }

        public void SaveTokenStore(string token, string tokenStore)
        {
            sqlCommandApiMobile.SaveTokenStoreinDb(token, tokenStore);
        }   

        public void SaveDamageForUser(string idVech, string damageForUserJson)
        {
            List<DamageForUser> damageForUsers = JsonConvert.DeserializeObject<List<DamageForUser>>(damageForUserJson);
            Task.Run(async() =>
            {
                VehiclwInformation vehiclwInformation = await sqlCommandApiMobile.GetVehiclwInformationAndSaveDamageForUser(idVech, damageForUsers);
                ITypeScan typeScan = GetTypeScan(vehiclwInformation.Ask.TypeVehicle);
                typeScan.SetDamage(damageForUsers, vehiclwInformation.Ask.TypeVehicle, vehiclwInformation.Scan.path);
            });
        }

        public void SaveSigPhoto(string idVech, Photo sig)
        {
            sqlCommandApiMobile.SaveSigPikedUpInDb(idVech, sig);
        }

        public VehiclwInformation GetVehiclwInformation(int idVech)
        {
            return sqlCommandApiMobile.GetVehiclwInformationInDb(idVech);
        }

        public Shipping GetShipping(string idShip)
        {
            return sqlCommandApiMobile.GetShippingInDb(idShip);
        }

        public void SavePhotoInspection(string idVe, PhotoInspection photoInspection)
        {
            Task.Run( async() =>
            {
                VehiclwInformation vehiclwInformation = await sqlCommandApiMobile.SavePhotoInspectionInDb(idVe, photoInspection);
                ITypeScan typeScan = GetTypeScan(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                typeScan.SetDamage(photoInspection, vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), vehiclwInformation.Scan.path);
            });
        }

        private ITypeScan GetTypeScan(string type)
        {
            ITypeScan typeScan = null;
            switch(type)
            {
                case "Coupe":
                    {
                        typeScan = new CoupeCar();
                        break;
                    }
                case "Suv":
                    {
                        typeScan = new SuvCar();
                        break;
                    }
                case "PickUp":
                    {
                        typeScan = new PickedUpCar();
                        break;
                    }
            }
            return typeScan;
        }

        public void SaveAsk(string idVe, int type, string jsonStrAsk)
        {
            if(type == 1)
            {
                Ask ask = JsonConvert.DeserializeObject<Ask>(jsonStrAsk);
                sqlCommandApiMobile.SaveAskInDb(idVe, ask);
            }
            else if(type == 2)
            {
                Ask1 ask1 = JsonConvert.DeserializeObject<Ask1>(jsonStrAsk);
                sqlCommandApiMobile.SaveAsk1InDb(idVe, ask1);
            }
            else if (type == 3)
            {
                AskFromUser askFromUser = JsonConvert.DeserializeObject<AskFromUser>(jsonStrAsk);
                sqlCommandApiMobile.SaveAskFromUserInDb(idVe, askFromUser);
            }
            else if(type == 4)
            {
                AskDelyvery askDelyvery = JsonConvert.DeserializeObject<AskDelyvery>(jsonStrAsk);
                sqlCommandApiMobile.SaveAskDelyveryInDb(idVe, askDelyvery);
            }
            else if (type == 5)
            {
                AskForUserDelyveryM askForUserDelyveryM = JsonConvert.DeserializeObject<AskForUserDelyveryM>(jsonStrAsk);
                sqlCommandApiMobile.SaveAskForUserDelyveryInDb(idVe, askForUserDelyveryM);
            }
        }

        public void SaveFeedBack(string jsonStrAsk)
        {
            Feedback feedback = JsonConvert.DeserializeObject<Feedback>(jsonStrAsk);
            sqlCommandApiMobile.SaveFeedBackInDb(feedback);
        }

        public void ReCurentStatus(string idShip, string status)
        {
            sqlCommandApiMobile.ReCurentStatus(idShip, status);
            Task.Run(() =>
            {
                string tokenShope = sqlCommandApiMobile.GerShopTokenForShipping(idShip);
                ManagerNotifyMobileApi managerNotifyMobileApi = new ManagerNotifyMobileApi();
                if (status == "Picked up")
                {
                    managerNotifyMobileApi.SendNotyfyStatusPickup(idShip, tokenShope);
                }
                else if (status == "Delivered,Paid")
                {
                    managerNotifyMobileApi.SendNotyfyStatusDelyvery(idShip, tokenShope, "Cars passed inspection, the order is paid");
                }
                else if (status == "Delivered,Billed")
                {
                    managerNotifyMobileApi.SendNotyfyStatusDelyvery(idShip, tokenShope, "Cars passed inspection, waiting for payment (Billing)");
                }
            });
        }

        private void CheckAndCreatedFolder()
        {
            if(!Directory.Exists("PhotoCars"))
            {
                Directory.CreateDirectory("PhotoCars");
            }
        }

        public string Avtorization(string email, string password)
        {
            string token = "";
            if (sqlCommandApiMobile.CheckEmailAndPsw(email, password))
            {
                token = CreateToken(email, password);
                sqlCommandApiMobile.SaveToken(email, password, token);
            }
            return token;
        }
        
        public void SavepOrder(string id, string idOrder, string name, string contactName, string address, 
            string city, string state, string zip, string phone, string email, string typeSave)
        {
            if (typeSave == "PikedUp")
            {
                sqlCommandApiMobile.SavePikedUpInDb(id, idOrder, name, contactName, address, city, state, zip, phone, email);
            }
            else if(typeSave == "Delivery")
            {
                sqlCommandApiMobile.SaveDeliveryInDb(id, idOrder, name, contactName, address, city, state, zip, phone, email);
            }
        }

        public void SavepOrder(string id, string typeSave, string payment, string paymentTeams)
        {
            if (typeSave == "Payment")
            {
                sqlCommandApiMobile.SavePaymentsInDb(id, payment, paymentTeams);
            }
        }

        public bool CheckToken(string token)
        {
            return sqlCommandApiMobile.CheckToken(token);
        }

        public string GetInspectionDriver(string token)
        {
            return sqlCommandApiMobile.GetInspectionDriverIndb(token);
        }

        public bool GetOrdersForToken(string token, ref List<Shipping> shippings, ref bool isInspectionDriver)
        {
            bool isToken = sqlCommandApiMobile.CheckToken(token);
            if (isToken)
            {
                shippings = sqlCommandApiMobile.GetOrdersForToken(token, ref isInspectionDriver);
            }
            return isToken;
        }

        public bool GetOrdersDelyveryForToken(string token, ref List<Shipping> shippings)
        {
            bool isToken = sqlCommandApiMobile.CheckToken(token);
            if (isToken)
            {
                shippings = sqlCommandApiMobile.GetOrdersDelyveryForToken(token, 0);
            }
            return isToken;
        }

        public void SavePay(string idVech, int type, Photo photo)
        {
            sqlCommandApiMobile.SavePayInDb(idVech, type, photo);
        }

        public void SavePayMethot(string idVech, string payMethod, string countPay)
        {
            sqlCommandApiMobile.SavePayMethotInDb(idVech, payMethod, countPay);
        }

        private string CreateToken(string email, string password)
        {
            string token = "";
            for(int i = 0; i < email.Length; i++)
            {
                token += i * new Random().Next(1, 1000) + email[i]; 
            }
            for (int i = 0; i < password.Length; i++)
            {
                token += i * new Random().Next(1, 1000) + password[i];
            }
            return token;
        }
    }
}