using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    public class DashbordController : Controller
    {
        ManagerDispatch managerDispatch = new ManagerDispatch();
        
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
                    ViewBag.Orders = managerDispatch.GetOrders("Pickedup", page);
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
    }
}