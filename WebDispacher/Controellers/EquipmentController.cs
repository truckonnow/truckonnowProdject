using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    [Route("Equipment")]
    public class EquipmentController : Controller
    {
        private ManagerDispatch managerDispatch = new ManagerDispatch();

        [Route("Trucks")]
        public IActionResult Trucks()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Trucks = managerDispatch.GetTrucks();
                    actionResult = View($"AllTruck");
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

        [Route("Trailers")]
        public IActionResult Index()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.Trailers = managerDispatch.GetTrailers();
                    actionResult = View($"AllTrailer");
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

        [Route("Truck/Remove")]
        public IActionResult RemoveTruck(string id)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.RemoveTruck(id);
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Equipment/Trucks");
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
        [Route("CreateTruck")]
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
                    actionResult = View("CreateTruck");
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
        [Route("CreateTruck")]
        public IActionResult CreateDriver(string nameTruk, string yera, string make, string model, string state, string exp, string vin, string owner, string plateTruk, string color)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    
                        managerDispatch.CreateTruk(nameTruk, yera, make, model, state, exp, vin, owner, plateTruk, color);
                        actionResult = Redirect($"{Config.BaseReqvesteUrl}/Equipment/Trucks");
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

        [Route("Trailer/Remove")]
        public IActionResult RemoveTrailer(string id)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.RemoveTrailer(id);
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Equipment/Trailers");
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
        [Route("CreateTrailer")]
        public IActionResult CreateTrailer()
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    actionResult = View("CreateTraler");
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
        [Route("CreateTrailer")]
        public IActionResult CreateTrailer(string name, string year, string make, string howLong, string vin, string owner, string color, string plate, string exp, string annualIns)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {

                    managerDispatch.CreateTrailer(name, year, make, howLong, vin, owner, color, plate, exp, annualIns);
                    actionResult = Redirect($"{Config.BaseReqvesteUrl}/Equipment/Trailers");
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
        [Route("SaveFile")]
        public string AddFile(IFormFile uploadedFile, string id)
        {
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvthoTaxi", out key);
                if (managerDispatch.CheckKey(key))
                {
                    if (uploadedFile != null)
                    {
                        string path = $"../Document/Truck/{id}/" + uploadedFile.FileName;
                        Directory.CreateDirectory($"../Document/Truck/{id}");
                        managerDispatch.SavePath(id, path);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            uploadedFile.CopyTo(fileStream);
                        }
                    }
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvthoTaxi"))
                    {
                        Response.Cookies.Delete("KeyAvthoTaxi");
                    }
                }
            }
            catch (Exception e)
            {

            }
            return "true";
        }

        [HttpGet]
        [Route("Document")]
        public async Task<IActionResult> Get(string id)
        {
            FileStream stream = null;
            try
            {
                string docPath = managerDispatch.GetDocument(id);
                stream = new FileStream(docPath, FileMode.Open);
            }
            catch (Exception)
            {
                stream = null;
            }
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}