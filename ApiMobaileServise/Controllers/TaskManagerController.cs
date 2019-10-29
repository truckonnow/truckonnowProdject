using ApiMobaileServise.Models;
using ApiMobaileServise.Servise;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace ApiMobaileServise.Controllers
{
    [Route("api.Task")]
    public class TaskManagerController
    {
        private ManagerMobileApi managerMobileApi = new ManagerMobileApi();

        [HttpPost]
        [Route("CheckTask")]
        public string CheckTask(string token)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", managerMobileApi.CheckTask(token)));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("StartTask")]
        public string StartTask(string token, string nameMethod, string optionalParameter)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", managerMobileApi.StartTask(nameMethod, optionalParameter, token)));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("LoadTask")]
        public string LoadTask(string token, string idTask, string byteBase64)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", managerMobileApi.LoadTask(idTask, byteBase64)));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception e)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("EndTask")]
        public string EndTask(string token, string idTask, string nameMethod)
        {
            string respons = null;
            if (token == null || token == "")
            {
                return JsonConvert.SerializeObject(new ResponseAppS("failed", "1", null));
            }
            try
            {
                bool isToken = managerMobileApi.CheckToken(token);
                if (isToken)
                {
                    managerMobileApi.EndTask(idTask, nameMethod);
                    respons = JsonConvert.SerializeObject(new ResponseAppS("success", "", "3"));
                }
                else
                {
                    respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "2", null));
                }
            }
            catch (Exception)
            {
                respons = JsonConvert.SerializeObject(new ResponseAppS("failed", "Technical work on the service", null));
            }
            return respons;
        }

        [HttpPost]
        [Route("RemoveTask")]
        public void RemoveTask()
        {

        }
    }
}
