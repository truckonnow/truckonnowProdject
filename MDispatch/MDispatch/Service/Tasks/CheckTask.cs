using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using RestSharp;
using System.Collections.Generic;

namespace MDispatch.Service.Tasks
{
    public class CheckTask : ITask
    {
        public void StartTask(params object[] task)
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            List<int> idTasks = GetTaskLoads();
            foreach(int idTask in idTasks)
            {
                string allTaskLoad = CrossSettings.Current.GetValueOrDefault("allTaskLoad", "");
                string workLoad = CrossSettings.Current.GetValueOrDefault("workLoad", "");
                if(allTaskLoad.Contains(idTask.ToString()))
                {
                    if (!workLoad.Contains(idTask.ToString()))
                    {
                        CrossSettings.Current.AddOrUpdateValue("workLoad", workLoad + idTask.ToString() + ",");
                    }
                    TaskManager.CommandToDo("SavePhoto", true, token, idTask.ToString());
                }
                else
                {
                    //remove Task
                    continue;
                }
            }
        }

        private List<int> GetTaskLoads()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            IRestResponse response = null;
            string content = null;
            RestClient client = new RestClient(Config.BaseReqvesteUrl);
            RestRequest request = new RestRequest("api.Task/CheckTask", Method.POST);
            client.Timeout = 60000;
            request.AddHeader("Accept", "application/json");
            request.AddParameter("token", token);
            response = client.Execute(request);
            content = response.Content;
            List<int> res = GetData(content);
            if(res != null && res.Count != 0)
            {
                return res;
            }
            else
            {
                //Waite connect Network
                return new List<int>();
            }
        }

        private List<int> GetData(string respJsonStr)
        {
            List<int> res = null;
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            if (status == "success")
            {
                res = JsonConvert.DeserializeObject<List<int>>(responseAppS.
                        SelectToken("ResponseStr").ToString());
            }
            return res;
        }
    }
}