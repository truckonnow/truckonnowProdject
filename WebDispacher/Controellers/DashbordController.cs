using System;
using System.Threading.Tasks;
using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    public class DashbordController : Controller
    {
        ManagerDispatch managerDispatch = new ManagerDispatch();
        private string Status { get; set; }

        [Route("Dashbord/Order/NewLoad")]
        public IActionResult NewLoad(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("NewLoad", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers();
                    ViewBag.count = managerDispatch.GetCountPage("NewLoad");
                    actionResult = View("NewLoad");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Assign")]
        [HttpPost]
        public string DriverSelect(string idOrder, string idDriver)
        {
            bool actionResult = false;
            try
            {
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                string key = null;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    if((idDriver != null && idDriver != "") && (idOrder != null && idOrder != ""))
                    {
                        managerDispatch.Assign(idOrder, idDriver);
                        actionResult = true;
                    }
                    else
                    {
                        actionResult = false;
                    }
                    
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = false;
                }
            }
            catch (Exception)
            {

            }
            return actionResult.ToString();
        }

        [Route("Dashbord/Unassign")]
        [HttpPost]
        public string DriverUnSelect(string idOrder)
        {
            bool actionResult = false;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    if (idOrder != null && idOrder != "")
                    {
                        managerDispatch.Unassign(idOrder); 
                        actionResult = true;
                    }
                    else
                    {
                        actionResult = false;
                    }

                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = false;
                }
            }
            catch (Exception)
            {

            }
            return actionResult.ToString();
        }

        [Route("Dashbord/Order/Archived")]
        public IActionResult Archived(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("Archived", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers();
                    ViewBag.count = managerDispatch.GetCountPage("Archived");
                    actionResult = View("Archived");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/Assigned")]
        public IActionResult Assigned(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("Assigned", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers();
                    ViewBag.count = managerDispatch.GetCountPage("Assigned");
                    actionResult = View("Assigned");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/Billed")]
        public IActionResult Billed(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("Billed");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/Deleted")]
        public IActionResult Deleted(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("Deleted", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers();
                    ViewBag.count = managerDispatch.GetCountPage("Deleted");
                    actionResult = View("Deleted");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/Delivered")]
        public IActionResult Delivered(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("Delivered", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers();
                    ViewBag.count = managerDispatch.GetCountPage("Delivered");
                    actionResult = View("Delivered");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/Paid")]
        public IActionResult Paid(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("Paid");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/Pickedup")]
        public IActionResult Pickedup(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("Picked up", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers();
                    ViewBag.count = managerDispatch.GetCountPage("Pickedup");
                    actionResult = View("Pickedup");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/ArchivedOrder")]
        public IActionResult DeletedOrder(string id)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.ArchvedOrder(id);
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Dashbord/Order/NewLoad");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/DeletedOrder")]
        public IActionResult DeletedOrder(string id, string status)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.DeletedOrder(id);
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Dashbord/Order/{status}");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/FullInfoOrder")]
        public IActionResult FullInfoOrder(string id, string stasus)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    if (id != "" && id != null)
                    {
                        ViewBag.Order = managerDispatch.GetOrder(id);
                        actionResult = View("FullInfoOrder");
                    }
                    else
                    {
                        actionResult = Redirect($"{Config.BaseReqvesteUrl}/Dashbord/Order/{stasus}");
                    }
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }
        
        [Route("Dashbord/Order/Edit")]
        public IActionResult EditOrder(string id, string stasus)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    if (id != "" && id != null)
                    {
                        ViewBag.Order = managerDispatch.GetOrder(id);
                        actionResult = View("EditOrder");
                        Status = stasus;
                    }
                    else
                    {
                        actionResult = Redirect($"{Config.BaseReqvesteUrl}/Dashbord/Order/{stasus}");
                    }
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/Creat")]
        public async Task<IActionResult> CreatOrderpage()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    Shipping shipping = await managerDispatch.CreateShiping();
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Dashbord/Order/Edit?id={shipping.Id}&stasus=NewLoad");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/SavaOrder")]
        public IActionResult SaveOrder(string idOrder, string idLoad, string internalLoadID, string driver, string status, string instructions, string nameP, string contactP,
            string addressP, string cityP, string stateP, string zipP, string phoneP, string emailP, string scheduledPickupDateP, string nameD, string contactD, string addressD,
            string cityD, string stateD, string zipD, string phoneD, string emailD, string ScheduledPickupDateD, string paymentMethod, string price, string paymentTerms, string brokerFee)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.Updateorder(idOrder, idLoad, internalLoadID, driver, status, instructions, nameP, contactP, addressP, cityP, stateP, zipP,
                        phoneP, emailP, scheduledPickupDateP, nameD, contactD, addressD, cityD, stateD, zipD, phoneD, emailD, ScheduledPickupDateD, paymentMethod,
                        price, paymentTerms, brokerFee);
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Dashbord/Order/NewLoad");
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [Route("Dashbord/Order/SavaVech")]
        public string SavaVech(string idVech, string VIN, string Year, string Make, string Model, string Type, string Color, string LotNumber)
        {
            string actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.SaveVechi(idVech, VIN, Year, Make, Model, Type,  Color, LotNumber);
                    actionResult = "Vehicle information saved successfully";
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = "Unauthorized user cannot change order";
                }
            }
            catch (Exception)
            {
                actionResult = "Vehicle information not saved (ERROR)";
            }
            return actionResult;
        }

        [Route("Dashbord/Order/RemoveVech")]
        public string RemoveVech(string idVech)
        {
            string actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.RemoveVechi(idVech);
                    actionResult = "Vehicle information removed successfully";
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = "Unauthorized user cannot change order";
                }
            }
            catch (Exception)
            {
                actionResult = "Vehicle information not removed (ERROR)";
            }
            return actionResult;
        }

        [Route("Dashbord/Order/AddVech")]
        public async Task<string> AddVech(string idOrder)
        {
            string actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    VehiclwInformation vehiclwInformation = await managerDispatch.AddVechi(idOrder);
                    ViewBag.Vech = vehiclwInformation;
                    actionResult = "Vehicle information Added successfully";
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    actionResult = "Unauthorized user cannot change order";
                }
            }
            catch (Exception)
            {
                actionResult = "Vehicle information not Added (ERROR)";
            }
            return actionResult;
        }
    }
}