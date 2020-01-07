using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace ApiMobaileServise.Controllers
{
    [Route("Mobile")]
    public class A_RController : Controller
    {
        ManagerMobileApi managerMobileApi = new ManagerMobileApi();

        [HttpGet]
        [Route("Init")]
        public void Init()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:/Users/Roma/source/repos/truckonnow/AuchConfig/Truckonnow-38b8427a812c.json");
            var client = ImageAnnotatorClient.Create();
            var image = Google.Cloud.Vision.V1.Image.FromFile("123.png");
            var response = client.DetectText(image);
        }

        [HttpPost]
        [Route("Avtorization")]
        public string Avtorization(string email, string password)
        {
            string token = null;
            string respons = null;
            if ((email != "" || email != null) && (password != "" || password != null))
            {
                token = managerMobileApi.Avtorization(email, password);
                if (token != null && token != "")
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", token));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Password or mail was not correct", ""));
                }
            }
            else
            {
                if (email != "" || email != null)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Email field is empty", ""));
                }
                else if (password != "" || password != null)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Password field is empty", ""));
                }
            }
            return respons;
        }

        [HttpPost]
        [Route("Avtorization/Clear")]
        public string Clear(string token)
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
                    managerMobileApi.ClearToken(token);
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", ""));
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