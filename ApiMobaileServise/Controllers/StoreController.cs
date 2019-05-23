using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ApiMobaileServise.Controllers
{
    [Route("Mobile")]
    public class StoreController : Controller
    {
        private ManagerMobileApi managerMobileApi = new ManagerMobileApi();

        [HttpPost]
        [Route("tokenStore/Save")]
        public string TokenStoreSave(string token, string tokenStore)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    Task.Run(() =>
                    {
                        managerMobileApi.SaveTokenStore(token, tokenStore);
                    });
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", null));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
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
