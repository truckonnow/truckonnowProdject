using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}