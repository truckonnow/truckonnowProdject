using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.Service
{
    public  class Photo
    {
        public int SaveTakeNewPhoto(string token, string id, byte[] PhotoInArrayByte, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string photoJson = JsonConvert.SerializeObject(PhotoInArrayByte);
                RestClient client = new RestClient("http://192.168.0.103:8888");
                RestRequest request = new RestRequest("Mobile/Photo/SavePhoto", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("id", id);
                request.AddParameter("photoJson", photoJson);
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
                //return GetData(content, ref description, ref shippings);
            }
            return 3;
        }
    }
}
