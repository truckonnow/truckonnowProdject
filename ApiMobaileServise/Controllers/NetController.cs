using ApiMobaileServise.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
