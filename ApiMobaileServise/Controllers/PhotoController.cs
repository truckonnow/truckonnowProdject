using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ApiMobaileServise.Controllers
{
    [Route("Mobile")]
    public class PhotoController : Controller
    {
        ManagerMobileApi ManagerMobileApi = new ManagerMobileApi();

        [HttpPost]
        [Route("Photo/SavePhoto")]
        public string SavePhoto(string token, string id, string photoJson)
        {
            string respons = null;
            byte[] photoInArrayByte = JsonConvert.DeserializeObject<byte[]>(photoJson);
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = ManagerMobileApi.CheckToken(token);
                if (isToken)
                {
                    string numberPhoto = ManagerMobileApi.GetNamePhoto(id);
                    CreatePhotoFolderVehiclw("ForVehiclwInformation");
                    ManagerMobileApi.SavePhoto(id, $"PhotoCars/ForVehiclwInformation/{id}{numberPhoto}.png");
                    using (var imageFile = new FileStream($"PhotoCars/ForVehiclwInformation/{id}{numberPhoto}.png", FileMode.Create))
                    {
                        imageFile.Write(photoInArrayByte, 0, photoInArrayByte.Length);
                        imageFile.Flush();
                    }
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

        private void CreatePhotoFolderVehiclw(string typePhotoCreate)
        {
            if (!Directory.Exists($"PhotoCars/{typePhotoCreate}"))
            {
                Directory.CreateDirectory($"PhotoCars/{typePhotoCreate}");
            }
        }

    }
}