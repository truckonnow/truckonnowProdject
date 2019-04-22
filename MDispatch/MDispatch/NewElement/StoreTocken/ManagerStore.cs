using MDispatch.Service;
using Plugin.Settings;
using RestSharp;
using System;

namespace MDispatch.NewElement.StoreTocken
{
    public class ManagerStore
    {
        public void SendToken(string tokenMarket)
        {
            if (App.isAvtorization && tokenMarket != null && tokenMarket != "")
            {
                string token = CrossSettings.Current.GetValueOrDefault("Token", "");
                IRestResponse response = null;
                string content = null;
                try
                {
                    RestClient client = new RestClient(Config.BaseReqvesteUrl);
                    RestRequest request = new RestRequest("Mobile/TokenSope", Method.POST);
                    client.Timeout = 10000;
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("token", token);
                    request.AddParameter("tokenMarket", tokenMarket);
                    response = client.Execute(request);
                    content = response.Content;
                }
                catch (Exception)
                {
                    
                }
                if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    
                }
                else
                {

                }
            }
        }
    }
}
