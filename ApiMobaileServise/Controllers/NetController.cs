using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace ApiMobaileServise.Controllers
{
    [Route("Mobile")]
    public class NetController : Controller
    {
        [HttpGet]
        [Route("Net")]
        public string CheckNet()
        {
            string respons = null;
            try
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", true));
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpGet]
        [Route("Sync")]
        public string CheckSync()
        {
            string respons = null;
            try
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", DateTime.Now.AddHours(-3)));
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }
    }
}