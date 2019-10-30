using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.Service.Tasks
{
    public class CheckTask : ITask
    {
        public void StartTask(params object[] task)
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            List<Model.Tasks> idTasks = GetTaskLoads();
            foreach(Model.Tasks idTask in idTasks)
            {
                string allTaskLoad = CrossSettings.Current.GetValueOrDefault("allTaskLoad", "");
                string workLoad = CrossSettings.Current.GetValueOrDefault("workLoad", "");
                if(allTaskLoad.Contains(idTask.IdTask.ToString()))
                {
                    if (!workLoad.Contains(idTask.IdTask.ToString()))
                    {
                        CrossSettings.Current.AddOrUpdateValue("workLoad", workLoad + idTask.IdTask.ToString() + ",");
                    }
                    TaskManager.CommandToDo(idTask.Method, 2, token, idTask.IdTask.ToString());
                }
                else
                {
                    //remove Task
                    continue;
                }
            }
            string noStartkLoad = CrossSettings.Current.GetValueOrDefault("noStartkLoad", "");
            string[] noStartkLoads = noStartkLoad.Split(',');
            for(int i = 0; i < noStartkLoads.Length-1; i++)
            {
                string method = CrossSettings.Current.GetValueOrDefault(noStartkLoads[i] + "method", "");
                GoToCommand1(token, 1, method, noStartkLoads[i]);
                string s = noStartkLoad.Replace(noStartkLoads[i] + ",", "");
                CrossSettings.Current.AddOrUpdateValue("noStartkLoad", noStartkLoad.Replace(s, ""));
                CrossSettings.Current.Remove(noStartkLoads[i] + "method");
                CrossSettings.Current.Remove(noStartkLoads[i] + "Param");
                CrossSettings.Current.Remove(noStartkLoads[i]);
            }
            //string noEndkLoad = CrossSettings.Current.GetValueOrDefault("noEndkLoad", "");
            //string[] noEndkLoads = noEndkLoad.Split(',');
            //for (int i = 0; i < noEndkLoads.Length - 1; i++)
            //{
            //    string obj = CrossSettings.Current.GetValueOrDefault(noEndkLoads[i], "");
            //    string vehiclwInformationId = CrossSettings.Current.GetValueOrDefault(noEndkLoads[i] + "Param", "");
            //    string method = CrossSettings.Current.GetValueOrDefault(noEndkLoads[i] + "method", "");
            //    GoToCommand(token, 3, obj, method, vehiclwInformationId);
            //    CrossSettings.Current.AddOrUpdateValue("noEndkLoad", noEndkLoad.Replace(noEndkLoads[i] + ",", ""));
            //}
        }

        private void GoToCommand(string token, int type, string method, string idTaskNo)
        {
            if (method == "SavePhoto")
            {
                string obj = CrossSettings.Current.GetValueOrDefault(idTaskNo, "");
                string vehiclwInformationId = CrossSettings.Current.GetValueOrDefault(idTaskNo + "Param", "");
                byte[] photoInspectionArray = Convert.FromBase64String(obj);
                string photoInspectionjson = Encoding.Default.GetString(photoInspectionArray);
                Models.PhotoInspection photoInspection = JsonConvert.DeserializeObject<Models.PhotoInspection>(photoInspectionjson);
                TaskManager.CommandToDo("SavePhoto", type, token, vehiclwInformationId, photoInspection);
            }
            else if (method == "SaveInspactionDriver")
            {
                string obj = CrossSettings.Current.GetValueOrDefault(idTaskNo, "");
                string[] paramss = CrossSettings.Current.GetValueOrDefault(idTaskNo + "Param", "").Split(',');
                string IdDriver = paramss[0];
                string IndexCurent = paramss[1];
                byte[] photoArray = Convert.FromBase64String(obj);
                string photojson = Encoding.Default.GetString(photoArray);
                Models.Photo photo = JsonConvert.DeserializeObject<Models.Photo>(photojson);
                TaskManager.CommandToDo("SaveInspactionDriver", type, token, IdDriver, photo, IndexCurent);
            }
        }

        private void GoToCommand1(string token, int type, string method, string idTaskNo)
        {
            if(method == "SavePhoto")
            {
                string obj = CrossSettings.Current.GetValueOrDefault(idTaskNo, "");
                string vehiclwInformationId = CrossSettings.Current.GetValueOrDefault(idTaskNo + "Param", "");
                byte[] photoInspectionArray = Convert.FromBase64String(obj);
                string photoInspectionjson = Encoding.Default.GetString(photoInspectionArray);
                Models.PhotoInspection photoInspection = JsonConvert.DeserializeObject<Models.PhotoInspection>(photoInspectionjson);
                TaskManager.CommandToDo("SavePhoto", type, token, vehiclwInformationId, photoInspection);
            }
        }

        private List<Model.Tasks> GetTaskLoads()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            IRestResponse response = null;
            string content = null;
            RestClient client = new RestClient(Config.BaseReqvesteUrl);
            RestRequest request = new RestRequest("api.Task/CheckTask", Method.POST);
            client.Timeout = 10000;
            request.AddHeader("Accept", "application/json");
            request.AddParameter("token", token);
            response = client.Execute(request);
            content = response.Content;
            List<Model.Tasks> res = GetData(content);
            if(res != null && res.Count != 0)
            {
                return res;
            }
            else
            {
                //Waite connect Network
                return new List<Model.Tasks>();
            }
        }

        private List<Model.Tasks> GetData(string respJsonStr)
        {
            List<Model.Tasks> res = null;
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            if (status == "success")
            {
                res = JsonConvert.DeserializeObject<List<Model.Tasks>>(responseAppS.
                        SelectToken("ResponseStr").ToString());
            }
            return res;
        }
    }
}