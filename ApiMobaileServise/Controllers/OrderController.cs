using System;
using System.Collections.Generic;
using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiMobaileServise.Controllers
{
    [Route("Mobile")]
    public class OrderController : Controller
    {
        ManagerMobileApi ManagerMobileApi = new ManagerMobileApi();

        [HttpPost]
        [Route("ActiveOreder")]
        public string GetActiveOrder(string token)
        {
            string respons = null;
            if(token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                List<Shipping> shippings = null;
                bool isToken = ManagerMobileApi.GetOrdersForToken(token, ref shippings);
                if(isToken)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", shippings));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch(Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("SaveOrder")]
        public string Save(string token, string id, string idOrder, string name, string contactName, 
            string address, string city, string state, string zip, string phone, string email, string typeSave)
        {
            string respons = null;
            try
            {
                bool isToken = ManagerMobileApi.CheckToken(token);
                if (isToken)
                {
                    ManagerMobileApi.SavepOrder(id, idOrder, name, contactName, address, city, state, zip, phone, email, typeSave);
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", ""));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Token does not Valid", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("SaveOrder1")]
        public string Save(string token, string id, string typeSave, string payment, string paymentTeams)
        {
            string respons = null;
            try
            {
                bool isToken = ManagerMobileApi.CheckToken(token);
                if (isToken)
                {
                    ManagerMobileApi.SavepOrder(id, typeSave, payment, paymentTeams);
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", ""));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Token does not Valid", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }
    }
}