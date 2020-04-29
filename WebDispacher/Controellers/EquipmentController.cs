using System;
using System.Collections.Generic;
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
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
        [DisableRequestSizeLimit]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult CreateDriver(string nameTruk, string yera, string make, string model , string typeTruk, string state, string exp, string vin, string owner, string plateTruk, string color, List<IFormFile> registrationDoc, List<IFormFile> ensuresDoc, List<IFormFile> _3Doc)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    
                       managerDispatch.CreateTruk(nameTruk, yera, make, model, typeTruk, state, exp, vin, owner, plateTruk, color, registrationDoc[0], ensuresDoc[0], _3Doc[0]);
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
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
        [DisableRequestSizeLimit]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult CreateTrailer(string name, string typeTrailer, string year, string make, string howLong, string vin, string owner, string color, string plate, string exp, string annualIns, List<IFormFile> registrationDoc, List<IFormFile> ensuresDoc, List<IFormFile> _3Doc)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {

                    managerDispatch.CreateTrailer(name, typeTrailer, year, make, howLong, vin, owner, color, plate, exp, annualIns, registrationDoc[0], ensuresDoc[0], _3Doc[0]);
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> Get(string id)
        {
            FileStream stream = null;
            try
            {
                string docPath = await managerDispatch.GetDocument(id);
                stream = new FileStream(docPath, FileMode.Open);
            }
            catch (Exception)
            {
                stream = null;
            }
            return new FileStreamResult(stream, "application/pdf");
        }

        [Route("Truck/Doc")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> GoToViewTruckDoc(string id)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.TruckDoc = await managerDispatch.GetTruckDoc(id);
                    ViewBag.TruckId = id;
                    actionResult = View($"DocTruck");
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
            catch (Exception e)
            {

            }
            return actionResult;
        }

        [Route("Trailer/Doc")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public async Task<IActionResult> GoToViewTraileDoc(string id)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    ViewBag.TrailerDoc = await managerDispatch.GetTraileDoc(id);
                    ViewBag.TrailerId = id;
                    actionResult = View($"DocTrailer");
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
            catch (Exception e)
            {

            }
            return actionResult;
        }

        [Route("Truck/SaveDoc")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public void SaveDoc(IFormFile uploadedFile, string nameDoc, string id)
        {
            //IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.SaveDocTruck(uploadedFile, nameDoc, id);
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    //actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception e)
            {

            }
            //return actionResult;
        }

        [Route("Trailer/SaveDoc")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public void SaveDoc1(IFormFile uploadedFile, string nameDoc, string id)
        {
            //IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.SaveDocTrailer(uploadedFile, nameDoc, id);
                }
                else
                {
                    if (Request.Cookies.ContainsKey("KeyAvtho"))
                    {
                        Response.Cookies.Delete("KeyAvtho");
                    }
                    //actionResult = Redirect(Config.BaseReqvesteUrl);
                }
            }
            catch (Exception e)
            {

            }
            //return actionResult;
        }

        [Route("RemoveDoc")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult RemoveDoc(string idDock, string id, string type)
        {
            IActionResult actionResult = null;
            try
            {
                string key = null;
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                Request.Cookies.TryGetValue("KeyAvtho", out key);
                if (managerDispatch.CheckKey(key))
                {
                    managerDispatch.RemoveDoc(idDock);
                    actionResult = Redirect($"{type}/Doc?id={id}");
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
            catch (Exception e)
            {

            }
            return actionResult;
        }



        [Route("GetDock")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult GetDock(string docPath, string type)
        {
            IActionResult actionResult = null;
            var imageFileStream = System.IO.File.OpenRead(docPath);
            actionResult = File(imageFileStream, type);
            return actionResult;
        }

        [Route("GetDockPDF")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult GetDockPDF(string docPath)
        {
            IActionResult actionResult = null;
            var imageFileStream = System.IO.File.OpenRead(docPath);
            actionResult = File(imageFileStream, "application/pdf");
            return actionResult;
        }
    }
}