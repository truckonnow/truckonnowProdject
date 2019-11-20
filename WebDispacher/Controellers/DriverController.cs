using System;
using System.Collections.Generic;
using System.Linq;
using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using WebDispacher.Mosels.Driver;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    public class DriverController : Controller
    {
        ManagerDispatch managerDispatch = new ManagerDispatch();

        [Route("Driver/Drivers")]
        public IActionResult Drivers(int page)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Drivers = managerDispatch.GetDrivers(page);
                    actionResult = View("FullAllDrivers");
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

        [HttpGet]
        [Route("Driver/Drivers/CreateDriver")]
        public IActionResult CreateDriver()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("CreateDriver");
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

        [HttpPost]
        [Route("Driver/Drivers/CreateDriver")]
        public IActionResult CreateDriver(string fullName, string emailAddress, string password, string phoneNumbe, string trailerCapacity, string driversLicenseNumber)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    if ((fullName != null && fullName != "") && (emailAddress != null && emailAddress != "") && (emailAddress != null && emailAddress != "")
                        && (password != null && password != "") && (fullName != null && fullName != ""))
                    {
                        managerDispatch.CreateDriver(fullName, emailAddress, password, phoneNumbe, trailerCapacity, driversLicenseNumber);
                        actionResult = Redirect($"{Config.BaseReqvesteUrl}/Driver/Drivers");
                    }
                    else
                    {
                        actionResult = View("CreateDriver");
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

        [HttpGet]
        [Route("Driver/Drivers/Remove")]
        public IActionResult RemoveDriver(int id)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.RemoveDrive(id);
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Driver/Drivers");
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

        [HttpGet]
        [Route("Driver/Drivers/Edit")]
        public IActionResult EditeDriver(int id)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Driver = managerDispatch.GetDriver(id);
                    actionResult = View("EditDriver");
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

        [HttpPost]
        [Route("Driver/Drivers/Edit")]
        public IActionResult EditeDriver(int id, string fullName, string emailAddress, string password, string phoneNumbe, string trailerCapacity, string driversLicenseNumber)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.EditDrive(id, fullName, emailAddress, password, phoneNumbe, trailerCapacity, driversLicenseNumber);
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Driver/Drivers");
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

        [HttpGet]
        [Route("Driver/InspactionTrucks")]
        public IActionResult ViewAllInspactionDate(string idDriver, string nameDriver, string date)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    List<Driver> drivers = managerDispatch.GetDrivers();
                    ViewBag.InspectionTruck = managerDispatch.GetInspectionTrucks(idDriver, date)
                        .Select(x => new InspectinView()
                        {
                            Id = x.Id,
                            Date = x.Date,
                            Trailer = "---------",
                            Truck = "---------",
                            NameDriver = drivers.First(d => d.InspectionDrivers.FirstOrDefault(i => i.Id == x.Id) != null).FullName,
                        })
                        .OrderBy(x => Convert.ToDateTime(x.Date))
                        .ToList();
                    ViewBag.Drivers = drivers;
                    ViewBag.IdDriver = idDriver;
                    ViewBag.SelectData = date;
                    actionResult = View("AllInspactionTruckData");
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

        [HttpGet]
        [Route("Driver/InspactionTruck")]
        public IActionResult ViewInspaction(string idInspection, string idDriver, string date)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    Driver drivers = managerDispatch.GetDriver(Convert.ToInt32(idDriver));
                    ViewBag.InspectionTruck = managerDispatch.GetInspectionTruck(idInspection);
                    ViewBag.Drivers = drivers;
                    ViewBag.SelectData = date;
                    actionResult = View("OneInspektion");
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
    }
}