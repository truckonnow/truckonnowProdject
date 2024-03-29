﻿using MDispatch.NewElement.ToastNotify;
using MDispatch.Service.Tasks;
using MDispatch.View.GlobalDialogView;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Rg.Plugins.Popup.Services;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.Service.Net
{
    public class Utils
    {
        private static bool isAlRedy = false;

        [Obsolete]
        public static async Task CheckNet(bool isInspection = false)
        {
            var sync = SynchronizationContext.Current;
            IRestResponse response = null;
            string content = null;
            try
            {
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/Net", Method.GET);
                request.AddHeader("Accept", "application/json");
                client.Timeout = 5000;
                response = client.Execute(request);
                content = response.Content;
              
                    if (content == "" || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        if (App.isNetwork)
                        {
                            TaskManager.isWorkTask = false;
                            App.isNetwork = false;
                            if (App.isStart)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.PushAsync(new Errror("Not Network", null));
                            });
                            }
                            else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                DependencyService.Get<IToast>().ShowMessage("Not Network");
                            });
                            }
                        }
                        else if (!isAlRedy)
                        {
                            isAlRedy = true;
                        if (App.isStart)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.PushAsync(new Errror("Not Network", null));
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                DependencyService.Get<IToast>().ShowMessage("Not Network");
                            });
                        }
                            RefreshIsAlRed();
                        }
                    }
                    else
                    {
                        bool isCheck = false;
                        string description = null;
                        GetData(content, ref isCheck, ref description);
                    if (!isCheck)
                    {
                        if (App.isNetwork)
                        {
                            TaskManager.isWorkTask = false;
                            App.isNetwork = false;
                            if (App.isStart)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await PopupNavigation.PushAsync(new Errror(description, null));
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    DependencyService.Get<IToast>().ShowMessage(description);
                                });
                            }
                        }
                        else if (!isAlRedy)
                        {
                            TaskManager.isWorkTask = false;
                            isAlRedy = true;
                            if (App.isStart)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await PopupNavigation.PushAsync(new Errror(description, null));
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    DependencyService.Get<IToast>().ShowMessage(description);
                                });
                            }
                            RefreshIsAlRed();
                        }
                    }
                    else
                    {
                        if (!TaskManager.isWorkTask)
                        {
                            TaskManager.CommandToDo("CheckTask");
                        }

                        TaskManager.isWorkTask = true;
                        App.isNetwork = true;
                    }
                    }
            }
            catch (Exception e)
            {
                App.isNetwork = false;
            }
        }

        private static void RefreshIsAlRed()
        {
            Task.Run(() => 
            {
                Thread.Sleep(5000);
                isAlRedy = false;
            });
        }

        private static void GetData(string respJsonStr, ref bool isCheck, ref string description)
        {
            if(respJsonStr[0] == '!')
            {
                isCheck = false;
                description = "Technical work on the service";
                return;
            }
            respJsonStr = respJsonStr.Replace("\\", "");
            respJsonStr = respJsonStr.Remove(0, 1);
            respJsonStr = respJsonStr.Remove(respJsonStr.Length - 1);
            var responseAppS = JObject.Parse(respJsonStr);
            string status = responseAppS.Value<string>("Status");
            if (status == "success")
            {
                isCheck = Convert.ToBoolean((responseAppS.
                        SelectToken("ResponseStr").ToString()));
                description = JsonConvert.DeserializeObject<string>(responseAppS.
                        SelectToken("Description").ToString());
            }
            else
            {
                isCheck = false;
                description = "Not Network";
            }
        }
    }
}