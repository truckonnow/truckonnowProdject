using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.Service
{
    public class Ask
    {
        public int Save(string token, string id, ref string description, ref List<Photo> photos)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient("http://192.168.0.103:8888");
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("id", id);
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
