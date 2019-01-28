using MDispatch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace MDispatch.Service
{
    public class Inspection
    {
        public int ReCurentStatus(string token, string id, ref string description, string status)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient("http://192.168.0.100:8888");
                RestRequest request = new RestRequest("Mobile/ReCurentStatus", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("idShip", id);
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
                return GetData(content, ref description);
            }
        }

        public int GetShipping(string token, string id, ref string description, ref Shipping shipping)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient("http://192.168.0.100:8888");
                RestRequest request = new RestRequest("Mobile/Shipping", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("idShip", id);
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
                return GetData(content, ref description, ref shipping);
            }
        }

        public int SaveAsk(string token, string id, Ask ask, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(ask);
                RestClient client = new RestClient("http://192.168.0.100:8888");
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
            catch (Exception e)
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

        public int SaveAsk(string token, Feedback feedback, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(feedback);
                RestClient client = new RestClient("http://192.168.0.100:8888");
                RestRequest request = new RestRequest("Mobile/Save/FeedBack", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("jsonStrAsk", strJsonAsk);
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

        public int SaveAsk(string token, string id, Ask1 ask1, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(ask1);
                RestClient client = new RestClient("http://192.168.0.100:8888");
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 2);
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

        public int SaveAsk(string token, string id, AskFromUser askForUser, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(askForUser);
                RestClient client = new RestClient("http://192.168.0.100:8888");
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 3);
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

        public int SaveAsk(string token, string id, AskDelyvery askDelyvery, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(askDelyvery);
                RestClient client = new RestClient("http://192.168.0.100:8888");
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 4);
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

        public int SaveAsk(string token, string id, AskForUserDelyveryM askForUserDelyveryM, ref string description)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string strJsonAsk = JsonConvert.SerializeObject(askForUserDelyveryM);
                RestClient client = new RestClient("http://192.168.0.100:8888");
                RestRequest request = new RestRequest("Mobile/Save/Ansver", Method.POST);
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("token", token);
                request.AddParameter("idVe", id);
                request.AddParameter("jsonStrAsk", strJsonAsk);
                request.AddParameter("type", 5);
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
                if (photoInspection.Damages != null)
                {
                    photoInspection.Damages.ForEach((dm) =>
                    {
                        dm.Image = null;
                    });
                }
                string strPhotoInspection = JsonConvert.SerializeObject(photoInspection);
                RestClient client = new RestClient("http://192.168.0.100:8888");
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

        private int GetData(string respJsonStr, ref string description, ref Shipping shipping)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            description = responseAppS.Value<string>("Description");
            if (status == "success")
            {
                shipping = JsonConvert.DeserializeObject<Shipping>(responseAppS.
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