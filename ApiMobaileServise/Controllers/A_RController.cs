using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using Microsoft.AspNetCore.Mvc;

namespace ApiMobaileServise.Controllers
{
    [Route("Mobile")]
    public class A_RController : Controller
    {
        ManagerMobileApi managerMobileApi = null;

        [HttpGet]
        [Route("Avtorization")]
        public string TestAvtorization(string email, string password)
        {
            ResponseAppS<string> responseAppS = null;
            if ((email != "" && email != null)&&(password != "" && password != null))
            {
                responseAppS = new ResponseAppS<string>();
                managerMobileApi = new ManagerMobileApi();
            }
            else
            {

            }
            return "2";
        }

        [HttpPost]
        [Route("Avtorization")]
        public string Avtorization(string email, string password)
        {
            managerMobileApi = new ManagerMobileApi();
            return "2";
        }
    }
}
