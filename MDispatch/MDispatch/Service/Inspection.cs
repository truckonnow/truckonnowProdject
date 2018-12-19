using MDispatch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace MDispatch.Service
{
    public class Inspection
    {
        public int SaveAsk(string token, string id, Ask ask, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(ask);
                RestClient client = new RestClient("http://192.168.0.101:8888");
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 1);
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

        public int SavePhoto(string token, string id, PhotoInspection photoInspection, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strPhotoInspection = JsonConvert.SerializeObject(photoInspection);
                RestClient client = new RestClient("http://192.168.0.101:8888");
                RestRequest request = new RestRequest("Mobile/Save/Photo", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStr", strPhotoInspection);
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
                description = responseAppS
                    .Value<string>("description");
                return 2;
            }
        }
    }
}