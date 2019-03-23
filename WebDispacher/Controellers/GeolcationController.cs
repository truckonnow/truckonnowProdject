using System;
using Microsoft.AspNetCore.Mvc;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    [Route("Geolcation")]
    public class GeolcationController : Controller
    {
        ManagerDispatch managerDispatch = new ManagerDispatch();

        [Route("Map")]
        public IActionResult GeolocationPageGet()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Driver = managerDispatch.GetDrivers();
                    actionResult = View("MapsGeoDriver");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect("http://localhost:22929");
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }
    }
}