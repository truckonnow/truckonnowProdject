using System;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using RestSharp;

namespace MDispatch.Service.TimeSync
{
    public class Untils
    {

        private static Timer timer = null;
        private static bool isUpdate { get; set; }

        public static void Start()
        {
            if (!isUpdate)
            {
                timer = new Timer(new TimerCallback(SyncServer), null, 0, 60000);
                isUpdate = true;
            }
        }

        private static void SyncServer(object state)
        {
            DateTime dateTime = new DateTime();
            IRestResponse response = null;
            string content = null;
            try
            {
                string token = CrossSettings.Current.GetValueOrDefault("Token", "");
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Sync", Method.POST);
                request.AddHeader("Accept", "application/json");
                client.Timeout = 10000;
                request.AddParameter("token", token);
                response = client.Execute(request);
                content = response.Content;
                GetData(content, ref dateTime);
                string timeZpneCount = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault().MaxOffset.ToString();
                dateTime.AddHours(Convert.ToInt32(timeZpneCount));
            }
            catch (Exception)
            {
            }
        }

        public static void Stop()
        {
            if(isUpdate)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                isUpdate = false;
            }
        }

        private static void GetData(string respJsonStr, ref DateTime dateTime)
        {
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            if (status == "success")
            {
                dateTime = JsonConvert.DeserializeObject<DateTime>(responseAppS.
                        SelectToken("ResponseStr").ToString());
            }
        }
    }
}