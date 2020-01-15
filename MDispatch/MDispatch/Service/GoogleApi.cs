using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace MDispatch.Service
{
    public class GoogleApi
    {

        internal int DetectPlate(string token, string image, string idDriver, string type, ref string plate)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("api.Vision/plate", Method.POST);
                client.Timeout = 60000;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("token", token);
                request.AddParameter("image", image);
                request.AddParameter("idDriver", idDriver);
                request.AddParameter("type", type);
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
                return GetData(content, ref plate);
            }
        }

        private int GetData(string respJsonStr, ref string plate)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            if (status == "success")
            {
                plate = responseAppS
                    .Value<string>("ResponseStr");
                return 3;
            }
            else
            {
                return 2;
            }
        }
    }
}