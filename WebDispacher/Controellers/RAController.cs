using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    public class RAController : Controller
    {
        ManagerDispatch managerDispatch = new ManagerDispatch();

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult Index()
        {
            IActionResult actionResult = null;
            ViewData["hidden"] = "hidden";
            ViewData["TextError"] = "";
            ViewBag.BaseUrl = Config.BaseReqvesteUrl;
            if (Request.Cookies.ContainsKey("KeyAvtho"))
            {
                actionResult = Redirect("/Dashbord/Order/NewLoad");
            }
            else
            {
                actionResult = View("Avthorization");
            }
            return actionResult;
        }

        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult Avthorization(string Email, string Password)
        {
            IActionResult actionResult = null;
            try
            {
                if (Email == null || Password == null)
                    throw new Exception();
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                if (managerDispatch.Avthorization(Email, Password))
                {
                    ViewData["hidden"] = "";
                    actionResult = Redirect("/Dashbord/Order/NewLoad");
                    int key = managerDispatch.Createkey(Email, Password);
                    Response.Cookies.Append("KeyAvtho", key.ToString());
                }
                else
                {
                    ViewData["hidden"] = "hidden";
                    ViewData["TextError"] = "Password or mail have been entered incorrectly";
                    actionResult = View("Avthorization");
                }

            }
            catch (Exception)
            {
                ViewData["hidden"] = "hidden";
                ViewData["TextError"] = "Password or mail have been entered incorrectly";
                actionResult = View("Avthorization");
            }
            return actionResult;
        }

        [HttpGet]
        [Route("Recovery/Password")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult RecoveryPassword(string idDriver, string token)
        {
            IActionResult actionResult = null;
            try
            {
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                ViewBag.IdDriver = idDriver;
                ViewBag.Token = token;
                ViewBag.isStateActual = managerDispatch.CheckTokenFoDriver(idDriver, token);
                ViewData["hidden"] = "hidden";
                actionResult = View("RecoveryPassword");
            }
            catch (Exception)
            {
            }
            return actionResult;
        }

        [HttpPost]
        [Route("Restore/Password")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> RestorePassword(string newPassword, string idDriver, string token)
        {
            IActionResult actionResult = null;
            try
            {
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                ViewBag.IdDriver = idDriver;
                ViewBag.Token = token;
                ViewBag.isStateActual = await managerDispatch.ResetPasswordFoDriver(newPassword, idDriver, token);
                ViewData["hidden"] = "hidden";
                actionResult = View("RecoveryPassword");
            }
            catch (Exception)
            {
            }
            return actionResult;
        }
    }
}