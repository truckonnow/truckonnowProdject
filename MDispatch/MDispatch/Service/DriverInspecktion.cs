using MDispatch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace MDispatch.Service
{
    public class DriverInspecktion
    {
        public int CheckInspectionDriver(string token, ref string description, ref bool isInspection, ref int indexPhoto, ref List<string> plateTruck, ref List<string> plateTrailer)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Driver/CheckInspectionDriver", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description, ref isInspection, ref indexPhoto, ref plateTruck, ref plateTrailer);
            }
        }

        public int SaveInspactionDriver(string token, ref string description, string idDriver, Photo photo, int indexPhoto)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string photoJson = JsonConvert.SerializeObject(photo);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Driver/SaveInspactionDriver", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idDriver", idDriver);
                request.AddParameter("photoJson", photoJson);
                request.AddParameter("indexPhoto", indexPhoto);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int SetInspectionDriver(string token, ref string description, InspectionDriver inspectionDriver, string idDriver)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string inspectionDriverjson = JsonConvert.SerializeObject(inspectionDriver);
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Driver/SetInspectionDriver", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idDriver", idDriver);
                request.AddParameter("inspectionDriverStr", inspectionDriverjson);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        public int UpdateInspectionDriver(string token, ref string description, string idDriver)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Driver/UpdateInspectionDriver", Method.POST);
                client.Timeout = 10000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idDriver", idDriver);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref description);
            }
        }

        internal int SetPlate(string token, string plateTruck, string plateTrailer, ref string description, ref bool isPlate)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Driver/PlateTrackAndPlate", Method.POST);
                client.Timeout = 10000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("plateTruck", plateTruck);
                request.AddParameter("plateTrailer", plateTrailer);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref isPlate, ref description);
            }
        }

        internal int GetLastInspaction(string token, string idDriver, ref string latsInspection, ref string plateTruck, ref string plateTrailer, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Driver/LastInspaction", Method.POST);
                client.Timeout = 10000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("idDriver", idDriver);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref latsInspection, ref plateTruck, ref plateTrailer, ref description);
            }
        }

        internal int CheckPlate(string token, ref string description, ref string plateTruckAndTrailer)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Driver/CheckPlateTrackAndPlate", Method.POST);
                client.Timeout = 10000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
                return 4;
            }
            if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 4;
            }
            else
            {
                return GetData(content, ref plateTruckAndTrailer, ref description);
            }
        }

        private int GetData(string respJsonStr, ref bool isPlate, ref string description)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            description = responseAppS.Value<string>("Description");
            if (status == "success")
            {
                isPlate = Convert.ToBoolean(responseAppS.Value<bool>("ResponseStr"));
                return 3;
            }
            else
            {
                return 2;
            }
        }

        private int GetData(string respJsonStr, ref string latsInspection, ref string plateTruck, ref string plateTrailer, ref string description)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            description = responseAppS.Value<string>("Description");
            if (status == "success")
            {
                string res = responseAppS.Value<string>("ResponseStr");
                if(res != null)
                {
                    string[] arrRes = res.Split(',');
                    if(arrRes.Length == 3)
                    {
                        latsInspection = arrRes[0];
                        plateTruck = arrRes[1];
                        plateTrailer = arrRes[2];
                    }
                }
                return 3;
            }
            else
            {
                return 2;
            }
        }

        private int GetData(string respJsonStr, ref string plateTruckAndTrailer, ref string description)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            description = responseAppS.Value<string>("Description");
            if (status == "success")
            {
                plateTruckAndTrailer = responseAppS.Value<string>("ResponseStr").ToString();
                return 3;
            }
            else
            {
                return 2;
            }
        }

        private int GetData(string respJsonStr, ref string description)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            description = responseAppS.Value<string>("Description");
            if (status == "success")
            {
                return 3;
            }
            else
            {
                return 2;
            }
        }

        private int GetData(string respJsonStr, ref string description, ref bool isInspection, ref int indexPhoto, ref List<string> plateTruck, ref List<string> plateTrailer)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            description = responseAppS.Value<string>("Description");
            if (status == "success")
            {
                isInspection = Convert.ToBoolean(responseAppS.Value<bool>("ResponseStr"));
                indexPhoto = Convert.ToInt32(responseAppS.Value<int>("ResponseStr1"));
                plateTruck = JsonConvert.DeserializeObject<List<string>>(responseAppS.SelectToken("ResponseStr2").ToString());
                plateTrailer = JsonConvert.DeserializeObject<List<string>>(responseAppS.SelectToken("ResponseStr3").ToString());
                return 3;
            }
            else
            {
                return 2;
            }
        }

        private string UnCompress(string dataStr)
        {
            dataStr = dataStr.Replace("\"", "");
            byte[] data = Convert.FromBase64String(dataStr);
            string res = null;
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);

                res = Encoding.UTF8.GetString(resultStream.ToArray());
            }
            return res;
        }
    }
}