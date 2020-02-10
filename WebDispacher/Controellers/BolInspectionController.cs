using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        public IActionResult GoToViewTruckDoc(string idDriver)
        {
            IActionResult actionResult = null;
            ViewData["hidden"] = "hidden";

            Truck truck = managerDispatch.GetTruck(idDriver);
            ViewBag.Truck = truck;

            Trailer trailer = managerDispatch.GetTrailer(idDriver);
            ViewBag.Trailer = trailer;

            ViewBag.TruckDoc = managerDispatch.GetTruckDoc((truck != null ? truck.Id : 0).ToString());
            ViewBag.TrailerDoc = managerDispatch.GetTraileDoc((trailer != null ? trailer.Id : 0).ToString());

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