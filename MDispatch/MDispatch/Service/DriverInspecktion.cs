using MDispatch.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace MDispatch.Service
{
    public class DriverInspecktion
    {
        public int CheckInspectionDriver(string token, ref string description, ref bool isInspection)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Driver/CheckInspectionDriver", Method.POST);
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
                return GetData(content, ref description, ref isInspection);
            }
        }

        private int GetData(string respJsonStr, ref string description, ref bool isInspection)
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