using ApiMobaileServise.Attribute;
using ApiMobaileServise.BackgraundService.Queue;
using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using ApiMobaileServise.Servise.ModelInspertionDriver;
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
                ITransportVehicle transportVehicle = null;
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    int indexPhoto = managerMobileApi.GetIndexPhoto(token);
                        transportVehicle = managerMobileApi.GetPaternTrailerInspectionDriverByTokenDriver(token);
                    
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", await managerMobileApi.ChechToDayInspaction(token), indexPhoto, transportVehicle));
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
        [CompressGzip(IsCompresReqvest = true, ParamUnZip = "inspectionDriverStr")]
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
        [CompressGzip(IsCompresReqvest = true, ParamUnZip = "photoJson")]
        public string SaveInspactionDriver(string token, string idDriver, string photoJson, int indexPhoto, string typeTransportVehicle)
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
                    QueueWorkInspectionDriver.queues.Add($"SaveDriverInspection&,&{idDriver}&,&{photoJson}&,&{indexPhoto}&,&{typeTransportVehicle}");
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
        public string SetTralerAndTruck(string token, string plateTruck, string plateTrailer, string nowCheck)
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
                    bool isValidPlate = managerMobileApi.SetTralerAndTruck(token, plateTrailer, plateTruck, nowCheck);
                    ITransportVehicle transportVehicle = null;
                    if(nowCheck == "Truck" && isValidPlate)
                    {
                        transportVehicle = managerMobileApi.GetPaternTruckInspectionDriver(plateTruck);
                    }
                    else if(nowCheck == "Trailer" && isValidPlate)
                    {
                        transportVehicle = managerMobileApi.GetPaternTrailerInspectionDriver(plateTrailer);
                    }
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", isValidPlate, transportVehicle));
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

        [HttpGet]
        [Route("Test")]
        [CompressGzip(IsCompresRespons = true)]
        public string Test()
        {
            string respons = null;
            respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", new Shipping()
            {
                VehiclwInformations = new System.Collections.Generic.List<VehiclwInformation>
                {
                    new VehiclwInformation()
                    {
                        PhotoInspections = new System.Collections.Generic.List<PhotoInspection>()
                        {
                            new PhotoInspection()
                            {
                                Photos = new System.Collections.Generic.List<Photo>()
                                {
                                    new Photo()
                                    {
                                        Base64 = "sadf axscpjsdo fisdfu idhsfuiydsghufyds uidsugugfu8ygsdufygdsytfysdefguydsgfdiew309i0398r98 uyeedhsiuhfciudshfiuhd",
                                        Height =25,
                                        path ="sssss",
                                        Width =25,
                                        Id = 1
                                    }
                                }
                            }
                        }
                    },
                    new VehiclwInformation()
                    {
                        PhotoInspections = new System.Collections.Generic.List<PhotoInspection>()
                        {
                            new PhotoInspection()
                            {
                                Photos = new System.Collections.Generic.List<Photo>()
                                {
                                    new Photo()
                                    {
                                        Base64 = "sadf axscpjsdo fisdfu idhsfuiydsghufyds uidsugugfu8ygsdufygdsytfysdefguydsgfdiew309i0398r98 uyeedhsiuhfciudshfiuhd",
                                        Height =25,
                                        path ="sssss",
                                        Width =25,
                                        Id = 1
                                    }
                                }
                            }
                        }
                    },
                    new VehiclwInformation()
                    {
                        PhotoInspections = new System.Collections.Generic.List<PhotoInspection>()
                        {
                            new PhotoInspection()
                            {
                                Photos = new System.Collections.Generic.List<Photo>()
                                {
                                    new Photo()
                                    {
                                        Base64 = "sadf axscpjsdo fisdfu idhsfuiydsghufyds uidsugugfu8ygsdufygdsytfysdefguydsgfdiew309i0398r98 uyeedhsiuhfciudshfiuhd",
                                        Height =25,
                                        path ="sssss",
                                        Width =25,
                                        Id = 1
                                    }
                                }
                            }
                        }
                    },
                    new VehiclwInformation()
                    {
                        PhotoInspections = new System.Collections.Generic.List<PhotoInspection>()
                        {
                            new PhotoInspection()
                            {
                                Photos = new System.Collections.Generic.List<Photo>()
                                {
                                    new Photo()
                                    {
                                        Base64 = "sadf axscpjsdo fisdfu idhsfuiydsghufyds uidsugugfu8ygsdufygdsytfysdefguydsgfdiew309i0398r98 uyeedhsiuhfciudshfiuhd",
                                        Height =25,
                                        path ="sssss",
                                        Width =25,
                                        Id = 1
                                    }
                                }
                            }
                        }
                    },
                    new VehiclwInformation()
                    {
                        PhotoInspections = new System.Collections.Generic.List<PhotoInspection>()
                        {
                            new PhotoInspection()
                            {
                                Photos = new System.Collections.Generic.List<Photo>()
                                {
                                    new Photo()
                                    {
                                        Base64 = "sadf axscpjsdo fisdfu idhsfuiydsghufyds uidsugugfu8ygsdufygdsytfysdefguydsgfdiew309i0398r98 uyeedhsiuhfciudshfiuhd",
                                        Height =25,
                                        path ="sssss",
                                        Width =25,
                                        Id = 1
                                    }
                                }
                            }
                        }
                    },
                    new VehiclwInformation()
                    {
                        PhotoInspections = new System.Collections.Generic.List<PhotoInspection>()
                        {
                            new PhotoInspection()
                            {
                                Photos = new System.Collections.Generic.List<Photo>()
                                {
                                    new Photo()
                                    {
                                        Base64 = "sadf axscpjsdo fisdfu idhsfuiydsghufyds uidsugugfu8ygsdufygdsytfysdefguydsgfdiew309i0398r98 uyeedhsiuhfciudshfiuhd",
                                        Height =25,
                                        path ="sssss",
                                        Width =25,
                                        Id = 1
                                    }
                                }
                            }
                        }
                    }
                }
            }));
            return respons;
        }
    }
}