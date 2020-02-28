using ApiMobaileServise.BackgraundService.Queue;
using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using DaoModels.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMobaileServise.Controllers
{
    [Route("Mobile/Driver")]
    public class DriverController : Controller
    {
        ManagerMobileApi managerMobileApi = new ManagerMobileApi();

        [HttpPost]
        [Route("CheckInspectionDriver")]
        public async Task<string> CheckInspecktionDriver(string token)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", await managerMobileApi.ChechToDayInspaction(token), managerMobileApi.GetIndexPhoto(token), managerMobileApi.GetTruck()
                        .Select(x => x.PlateTruk).ToList() , managerMobileApi.GetTrailer()
                        .Select(x => x.Plate).ToList()));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("SetInspectionDriver")]
        public string SetInspectionDriver(string token, string idDriver, string inspectionDriverStr)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    managerMobileApi.SetInspectionDriver(idDriver, inspectionDriverStr);
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", null));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("SaveInspactionDriver")]
        public string SaveInspactionDriver(string token, string idDriver, string photoJson, int indexPhoto)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    QueueWorkInspectionDriver.queues.Add($"SaveDriverInspection&,&{idDriver}&,&{photoJson}&,&{indexPhoto}");
                    QueueWorkInspectionDriver.countQueues++;
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", null));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("UpdateInspectionDriver")]
        public string UpdateInspectionDriver(string token, string idDriver)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    managerMobileApi.UpdateInspectionDriver(idDriver);
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", null));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("PlateTrackAndPlate")]
        public string SetTralerAndTruck(string token, string plateTruck, string plateTrailer)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", managerMobileApi.SetTralerAndTruck(token, plateTrailer, plateTruck), null));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("CheckPlateTrackAndPlate")]
        public string CheckTralerAndTruck(string token)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", managerMobileApi.CheckTralerAndTruck(token), null));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpGet]
        [Route("Document")]
        public async Task<IActionResult> Get(string id)
        {
            FileStream stream = null;
            try
            {
                string docPath = managerMobileApi.GetDocument(id);
                stream = new FileStream("PDF/All/Debytory.pdf", FileMode.Open);
            }
            catch (Exception)
            {
                stream = null;
            }
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpPost]
        [Route("LastInspaction")]
        public string GetLastInspaction(string token, string idDriver)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    string lastInspectionDriver = managerMobileApi.GetLastInspaction(idDriver);
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", lastInspectionDriver));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }
    }
}