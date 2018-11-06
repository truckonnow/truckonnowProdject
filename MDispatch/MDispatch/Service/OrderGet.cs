using MDispatch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.Service
{
    public class OrderGet
    {
        public int ActiveOreder(string token, string status, ref string description, ref List<Shipping> shippings)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient("http://192.168.8.101:8888");
                RestRequest request = new RestRequest("Mobile/ActiveOreder", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("status", status);
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
                return GetData(content, ref description, ref shippings);
            }
        }


        private int GetData(string respJsonStr, ref string description, ref List<Shipping> shippings)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            if (status == "success")
            {
                shippings = JsonConvert.DeserializeObject<List<Shipping>>(responseAppS.
                        SelectToken("ResponseStr").ToString());
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
