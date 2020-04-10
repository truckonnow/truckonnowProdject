using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
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

        [Route("Driver/Check")]
        public IActionResult CheckDriver(string commpanyID, string nameDriver, string driversLicense, string comment)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.DriversLicense = driversLicense;
                    ViewBag.NameDriver = nameDriver;
                    ViewBag.DriverReports = managerDispatch.GetDriversReport(commpanyID, nameDriver, driversLicense);
                    actionResult = View("DriverCheck");
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
        [Route("Driver/AddReport")]
        public IActionResult AddReport()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("ReportDriver");
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
        [Route("Driver/AddReport")]
        public IActionResult AddReport(string fullName, string driversLicenseNumber, string description)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.AddNewReportDriver(fullName, driversLicenseNumber, description);
                    actionResult = Redirect("Check");
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
        [Route("Welcome/AddReport")]
        public IActionResult WelcomeAddReport()
        {
            IActionResult actionResult = null;
            try
            {
                ViewData["hidden"] = "hidden";
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                actionResult = View("WelcomReportDriver");
            }
            catch (Exception)
            {

            }
            return actionResult;
        }

        [HttpPost]
        [Route("Welcome/AddReport")]
        public IActionResult WelcomeAddReport(string fullName, string driversLicenseNumber, string description)
        {
            IActionResult actionResult = null;
            try
            {
                ViewData["hidden"] = "hidden";
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                managerDispatch.AddNewReportDriver(fullName, driversLicenseNumber, description);
                actionResult = Redirect(Config.BaseReqvesteUrl);
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
        public IActionResult RemoveDriver(int id, string comment)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.RemoveDrive(id, comment);
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

        //[HttpGet]
        //[Route("Driver/Drivers/Restore")]
        //public IActionResult RestoreDriver(int id)
        //{
        //    IActionResult actionResult = null;
        //    try
        //    {
        //        string key = null;
        //        ViewBag.BaseUrl = Config.BaseReqvesteUrl;
        //        Request.Cookies.TryGetValue("KeyAvtho", out key);
        //        if (managerDispatch.CheckKey(key))
        //        {
        //            managerDispatch.RestoreDrive(id);
        //            actionResult = Redirect($"{Config.BaseReqvesteUrl}/Driver/Drivers");
        //        }
        //        else
        //        {
        //            if (Request.Cookies.ContainsKey("KeyAvtho"))
        //            {
        //                Response.Cookies.Delete("KeyAvtho");
        //            }
        //            actionResult = Redirect(Config.BaseReqvesteUrl);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return actionResult;
        //}

        //[HttpGet]
        //[Route("Driver/Drivers/Comment")]
        //public IActionResult CommentDriver(int id, string Comment)
        //{
        //    IActionResult actionResult = null;
        //    try
        //    {
        //        string key = null;
        //        ViewBag.BaseUrl = Config.BaseReqvesteUrl;
        //        Request.Cookies.TryGetValue("KeyAvtho", out key);
        //        if (managerDispatch.CheckKey(key))
        //        {
        //            managerDispatch.CommentDriver(id, Comment);
        //            actionResult = Redirect($"{Config.BaseReqvesteUrl}/Driver/Drivers");
        //        }
        //        else
        //        {
        //            if (Request.Cookies.ContainsKey("KeyAvtho"))
        //            {
        //                Response.Cookies.Delete("KeyAvtho");
        //            }
        //            actionResult = Redirect(Config.BaseReqvesteUrl);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return actionResult;
        //}

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
        public IActionResult ViewAllInspactionDate(string idDriver, string idTruck, string idTrailer, string date)
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
                    List<Truck> trucks = managerDispatch.GetTrucks();
                    List<Trailer> trailers = managerDispatch.GetTrailers();
                    ViewBag.InspectionTruck = managerDispatch.GetInspectionTrucks(idDriver, idTruck, idTrailer, date)
                        .Select(x => new InspectinView()
                        {
                            Id = x.Id,
                            Date = x.Date,
                            Trailer = trailers.FirstOrDefault(t => t.Id == x.IdITrailer) != null ? $"{trailers.FirstOrDefault(t => t.Id == x.IdITrailer).Make}, Plate: {trailers.FirstOrDefault(t => t.Id == x.IdITrailer).Plate}" : "---------------",
                            Truck = trucks.FirstOrDefault(t => t.Id == x.IdITruck) != null ? $"{trucks.FirstOrDefault(t => t.Id == x.IdITruck).Make} {trucks.FirstOrDefault(t => t.Id == x.IdITruck).Model}, Plate: {trucks.FirstOrDefault(t => t.Id == x.IdITruck).PlateTruk}" : "---------------",
                            NameDriver = drivers.First(d => d.InspectionDrivers.FirstOrDefault(i => i.Id == x.Id) != null).FullName,
                        })
                        .OrderBy(x => Convert.ToDateTime(x.Date))
                        .ToList();
                    ViewBag.Drivers = drivers;
                    ViewBag.Trucks = trucks;
                    ViewBag.Trailers = trailers;
                    ViewBag.IdDriver = idDriver;
                    ViewBag.IdTruck = idTruck;
                    ViewBag.IdTrailer = idTrailer;
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
                    List<Truck> trucks = managerDispatch.GetTrucks();
                    List<Trailer> trailers = managerDispatch.GetTrailers();
                    InspectionDriver inspectionDriver = managerDispatch.GetInspectionTruck(idInspection);
                    Driver drivers = managerDispatch.GetDriver(inspectionDriver.Id.ToString());
                    //inspectionDriver.PhotosTruck.ForEach((item) =>
                    //{
                    //    try
                    //    {
                    //        byte[] imageB = Convert.FromBase64String(item.Base64);
                    //        Byte[] outputBytes = null;
                    //        Image image = null;
                    //        using (var inputStream = new MemoryStream(imageB))
                    //        {
                    //            image = Image.FromStream(inputStream);
                    //            var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                    //              .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    //            var encoderParameters = new EncoderParameters(1);
                    //            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 80L);
                    //            using (var outputStream = new MemoryStream())
                    //            {
                    //                image.Save(outputStream, jpegEncoder, encoderParameters);
                    //                outputBytes = outputStream.ToArray();
                    //            }
                    //        }
                    //        item.Base64_1 = Convert.ToBase64String(outputBytes.ToArray());
                    //    }
                    //    catch
                    //    {

                    //    }
                    //});
                    ViewBag.InspectionTruck = inspectionDriver;
                    ViewBag.Drivers = drivers;
                    ViewBag.Trailer = trailers.FirstOrDefault(t => t.Id == inspectionDriver.IdITrailer) != null ? $"{trailers.FirstOrDefault(t => t.Id == inspectionDriver.IdITrailer).Make}, Plate: {trailers.FirstOrDefault(t => t.Id == inspectionDriver.IdITrailer).Plate}" : "---------------";
                    ViewBag.Truck = trucks.FirstOrDefault(t => t.Id == inspectionDriver.IdITruck) != null ? $"{trucks.FirstOrDefault(t => t.Id == inspectionDriver.IdITruck).Make} {trucks.FirstOrDefault(t => t.Id == inspectionDriver.IdITruck).Model}, Plate: {trucks.FirstOrDefault(t => t.Id == inspectionDriver.IdITruck).PlateTruk}" : "---------------";
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

        [HttpGet]
        [Route("Driver/Image")]
        public IActionResult GetShiping(string name, string type)
        {
            var imageFileStream = System.IO.File.OpenRead(name);
            return File(imageFileStream, $"image/{type}");
        }
    }
}