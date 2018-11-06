using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public string GetActiveOrder(string token, string status)
        {
            string respons = null;
            if(token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                List<Shipping> shippings = null;
                bool isToken = ManagerMobileApi.GetOrdersForToken(token, status, ref shippings);
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
    }
}