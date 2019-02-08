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
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("NewLoad", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers(-1);
                    actionResult = View("NewLoad");
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

        [Route("Dashbord/Assign")]
        [HttpPost]
        public string DriverSelect(string idOrder, string idDriver)
        {
            bool actionResult = false;
            try
            {
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
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("Archived");
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

        [Route("Dashbord/Order/Assigned")]
        public IActionResult Assigned(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("Assigned", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers(-1);
                    actionResult = View("Assigned");
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

        [Route("Dashbord/Order/Billed")]
        public IActionResult Billed(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
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
                    actionResult = Redirect("http://localhost:22929");
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
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("Deleted");
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

        [Route("Dashbord/Order/Delivered")]
        public IActionResult Delivered(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("Delivered", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers(-1);
                    actionResult = View("Delivered");
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

        [Route("Dashbord/Order/Paid")]
        public IActionResult Paid(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
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
                    actionResult = Redirect("http://localhost:22929");
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
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Orders = managerDispatch.GetOrders("Picked up", page);
                    ViewBag.Drivers = managerDispatch.GetDrivers(-1);
                    actionResult = View("Pickedup");
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

        [Route("Dashbord/Order/FullInfoOrder")]
        public IActionResult FullInfoOrder(string id, string stasus)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
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
                        actionResult = Redirect($"http://localhost:22929/Dashbord/Order/{stasus}");
                    }
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
        
        [Route("Dashbord/Order/Edit")]
        public IActionResult EditOrder(string id, string stasus)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
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
                        actionResult = Redirect($"http://localhost:22929/Dashbord/Order/{stasus}");
                    }
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

        [Route("Dashbord/Order/Creat")]
        public async Task<IActionResult> CreatOrderpage()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Order = await managerDispatch.CreateShiping();
                    actionResult = View("CreateOrder");
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

        [Route("Dashbord/Order/SavaOrder")]
        public IActionResult SaveOrder(string idOrder, string idLoad, string internalLoadID, string driver, string status, string instructions, string nameP, string contactP,
            string addressP, string cityP, string stateP, string zipP, string phoneP, string emailP, string scheduledPickupDateP, string nameD, string contactD, string addressD,
            string cityD, string stateD, string zipD, string phoneD, string emailD, string ScheduledPickupDateD, string paymentMethod, string price, string paymentTerms, string brokerFee)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.Updateorder(idOrder, idLoad, internalLoadID, driver, status, instructions, nameP, contactP, addressP, cityP, stateP, zipP,
                        phoneP, emailP, scheduledPickupDateP, nameD, contactD, addressD, cityD, stateD, zipD, phoneD, emailD, ScheduledPickupDateD, paymentMethod,
                        price, paymentTerms, brokerFee);
                    actionResult = Redirect($"http://localhost:22929/Dashbord/Order/NewLoad");
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

        [Route("Dashbord/Order/SavaVech")]
        public IActionResult SavaVech()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
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