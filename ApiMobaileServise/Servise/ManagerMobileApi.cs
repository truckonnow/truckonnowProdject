﻿using ApiMobaileServise.EmailSmtp;
using ApiMobaileServise.Notify;
using ApiMobaileServise.Servise.AddDamage;
using DaoModels.DAO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
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

        public async Task SendBol(string idShip, string email)
        {
            Shipping shipping = sqlCommandApiMobile.SendBolInDb(idShip);
            string patern = new PaternSourse().GetPaternBol(shipping);
            await new AuthMessageSender().Execute(email, "Truckonnow - BOL", patern, shipping.VehiclwInformations);
        }

        public async Task SendCoupon(string email)
        {
            string patern = new PaternSourse().GetPaternCopon();
            await new AuthMessageSender().Execute(email, "Truckonnow - Coupon", patern);
        }

        public async void SetInspectionDriver(string idDriver, string inspectionDriverStr)
        {
            InspectionDriver inspectionDriver = JsonConvert.DeserializeObject<InspectionDriver>(inspectionDriverStr);
            await sqlCommandApiMobile.SetInspectionDriverInDb(idDriver, inspectionDriver);
        }

        public async void SaveInspactionDriver(string idDriver, string photoJson, int indexPhoto)
        {
            Photo photo = JsonConvert.DeserializeObject<Photo>(photoJson);
            //photo.path = photo.path.Insert(2, $"/{DateTime.Now.ToLongDateString()}");
            await sqlCommandApiMobile.SaveInspectionDriverInDb(idDriver, photo, indexPhoto);
        }

        public async void UpdateInspectionDriver(string idDriver)
        {
            await sqlCommandApiMobile.UpdateInspectionDriver(idDriver);
        }

        public async Task<bool> ChechToDayInspaction(string token)
        {
            return await sqlCommandApiMobile.ChechToDayInspactionInDb(token);
        }

        public async Task<int> GetIndexPhoto(string token)
        {
            return await sqlCommandApiMobile.GetIndexDb(token);
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

        public async Task SaveDamageForUser(string idVech, string damageForUserJson)
        {
            List<DamageForUser> damageForUsers = JsonConvert.DeserializeObject<List<DamageForUser>>(damageForUserJson);
            Task.Run(async() =>
            {
                VehiclwInformation vehiclwInformation = await sqlCommandApiMobile.GetVehiclwInformationAndSaveDamageForUser(idVech, damageForUsers);
                ITypeScan typeScan = GetTypeScan(vehiclwInformation.Ask.TypeVehicle);
                await typeScan.SetDamage(damageForUsers, vehiclwInformation.Ask.TypeVehicle, vehiclwInformation.Scan.path);
            });
        }

        public async Task SaveSigPhoto(string idVech, string sig)
        {
            Photo photoSig = JsonConvert.DeserializeObject<Photo>(sig);
            sqlCommandApiMobile.SaveSigPikedUpInDb(idVech, photoSig);
        }

        public VehiclwInformation GetVehiclwInformation(int idVech)
        {
            return sqlCommandApiMobile.GetVehiclwInformationInDb(idVech);
        }

        public Shipping GetShipping(string idShip)
        {
            return sqlCommandApiMobile.GetShippingInDb(idShip);
        }

        public async Task SavePhotoInspection(string idVe, string photoInspectionJson)
        {
            PhotoInspection photoInspection = JsonConvert.DeserializeObject<PhotoInspection>(photoInspectionJson);
            VehiclwInformation vehiclwInformation = await sqlCommandApiMobile.SavePhotoInspectionInDb(idVe, photoInspection);
            Task.Run(async () =>
            {
                ITypeScan typeScan = GetTypeScan(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                await typeScan.SetDamage(photoInspection, vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), vehiclwInformation.Scan.path);
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
                case "Sedan":
                    {
                        typeScan = new SedanCar();
                        break;
                    }
            }
            return typeScan;
        }

        public async Task SaveAsk(string idVe, int type, string jsonStrAsk)
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
            else if (type == 6)
            {
                Ask2 ask2 = JsonConvert.DeserializeObject<Ask2>(jsonStrAsk);
                sqlCommandApiMobile.SaveAsk2InDb(idVe, ask2);
            }
        }

        public async Task SaveFeedBack(string jsonStrAsk)
        {
            Feedback feedback = JsonConvert.DeserializeObject<Feedback>(jsonStrAsk);
            sqlCommandApiMobile.SaveFeedBackInDb(feedback);
        }

        public async Task ReCurentStatus(string idShip, string status)
        {
            sqlCommandApiMobile.ReCurentStatus(idShip, status);
            await Task.Run(() =>
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

        public bool GetOrdersForToken(string token, ref List<Shipping> shippings)
        {
            bool isToken = sqlCommandApiMobile.CheckToken(token);
            if (isToken)
            {
                shippings = sqlCommandApiMobile.GetOrdersForToken(token);
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

        public bool GetOrdersArchiveForToken(string token, ref List<Shipping> shippings)
        {
            bool isToken = sqlCommandApiMobile.CheckToken(token);
            if (isToken)
            {
                shippings = sqlCommandApiMobile.GetOrdersArchiveForToken(token, 0);
            }
            return isToken;
        }

        public async Task SavePay(string idVech, int type, string photo)
        {
            Photo photo1 = JsonConvert.DeserializeObject<Photo>(photo);
            sqlCommandApiMobile.SavePayInDb(idVech, type, photo1);
        }

        public async Task SaveRecount(string idVech, int type, string video)
        {
            Video video1 = JsonConvert.DeserializeObject<Video>(video);
            await sqlCommandApiMobile.SaveRecontInDb(idVech, type, video1);
        }

        public async Task SavePayMethot(string idVech, string payMethod, string countPay)
        {
            sqlCommandApiMobile.SavePayMethotInDb(idVech, payMethod, countPay);
        }

        public void ClearToken(string token)
        {
            sqlCommandApiMobile.ClearTokenDb(token);
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