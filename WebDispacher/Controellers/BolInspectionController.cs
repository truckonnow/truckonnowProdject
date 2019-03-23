using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Mvc;
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
            if(shipping != null)
            {
                ViewBag.BaseUrl = Config.BaseReqvesteUrl;
                ViewBag.Shipp = shipping;
                actionResult = View("InspectionVech");
            }
            return actionResult;
        }
    }
}