using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebDispacher.Service;

namespace WebDispacher.Controellers
{
    public class BolInspectionController : Controller
    {
        ManagerDispatch managerDispatch = new ManagerDispatch();

        [Route("Photo/BOL/{idVech}")]
        public IActionResult GetPhotoInspection(int idVech)
        {
            IActionResult actionResult = null; 
            ViewData["hidden"] = "hidden";
            Shipping shipping = managerDispatch.GetShipingCurrentVehiclwIn(idVech.ToString());
            VehiclwInformation vehiclwInformation = shipping.VehiclwInformations.FirstOrDefault(v => v.Id == idVech);
            if(shipping != null)
            {
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                ViewBag.Shipp = shipping;
                ViewBag.Vehiclw = vehiclwInformation;
                actionResult = View("InspectionVech");
            }
            return actionResult;
        }

        [Route("Doc/{idDriver}")]
        public async Task<IActionResult> GoToViewTruckDoc(string idDriver)
        {
            IActionResult actionResult = null;
            ViewData["hidden"] = "hidden";

            Truck truck = null;
            Trailer trailer = null;

            await Task.WhenAll(
            Task.Run(async() => 
            {
                truck = await managerDispatch.GetTruck(idDriver); 
                ViewBag.Truck = truck;
                ViewBag.TruckDoc = await managerDispatch.GetTruckDoc((truck != null ? truck.Id : 0).ToString());
            }),
            Task.Run(async() =>
            {
                trailer = await managerDispatch.GetTrailer(idDriver);
                ViewBag.Trailer = trailer;
                ViewBag.TrailerDoc = await managerDispatch.GetTraileDoc((trailer != null ? trailer.Id : 0).ToString());
            }));

            actionResult = View($"DocDriver");
            return actionResult;
        }

        [Route("Doc")]
        [HttpGet]
        public async Task<IActionResult> GoToViewTruckDoc(string truckPlate, string trailerPlate)
        {
            IActionResult actionResult = null;
            ViewData["hidden"] = "hidden";

            Truck truck = null;
            Trailer trailer = null;

            await Task.WhenAll(
            Task.Run(async () =>
            {
                truck = await managerDispatch.GetTruckByPlate(truckPlate);
                ViewBag.Truck = truck;
                ViewBag.TruckDoc = await managerDispatch.GetTruckDoc((truck != null ? truck.Id : 0).ToString());
            }),
            Task.Run(async () =>
            {
                trailer = await managerDispatch.GetTrailerkByPlate(trailerPlate);
                ViewBag.Trailer = trailer;
                ViewBag.TrailerDoc = await managerDispatch.GetTraileDoc((trailer != null ? trailer.Id : 0).ToString());
            }));

            actionResult = View($"DocDriver");
            return actionResult;
        }

        private List<Photo> SortPhotoInspections(List<Photo> photos)
        {
            List<Photo> photos1 = new List<Photo>();
            foreach (var photo in photos)
            {
                byte[] image = Convert.FromBase64String(photo.Base64);
                var ms = new MemoryStream(image);
                Image img = Image.FromStream(ms);
            }
            return photos1;
        }
    }
}