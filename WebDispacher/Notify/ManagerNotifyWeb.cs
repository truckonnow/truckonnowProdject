using DaoModels.DAO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WebDispacher.Notify
{
    public class ManagerNotifyWeb
    {
        private WebRequest tRequest = null;

        public ManagerNotifyWeb()
        {
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAACa2vxR0:APA91bGTTHJgDmirQgd92-snbn5eixwi-sEPufe8fpl6EojstTcNNMjRnod7nAdUOw0C6InZvWOvom1xlRiWbojN7ObxGTeEPhjBtZ53ac2RLzIVuZc9_AdEkuix-vlul_ylJV7_ctEK"));
            tRequest.Headers.Add(string.Format("Sender: id={0}", "41568683293"));
            tRequest.ContentType = "application/json";
        }

        public void SendNotyfyAssign(string idShip, string tokenShope, List<VehiclwInformation> vehiclwInformations)
        {
            string body = null;
            if (tokenShope != null && tokenShope != "")
            {
                foreach (var vech in vehiclwInformations)
                {
                    body += $"{vech.Year} {vech.Make} {vech.Model}\n";
                }
                var payload = new
                {
                    to = tokenShope,
                    content_available = true,
                    notification = new
                    {
                        click_action = "Oreder",
                        body = $"{body}",
                        title = $"New Load Order Id: {idShip} Assign",
                    },
                };
                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

        public void SendNotyfyUnassign(string idShip, string tokenShope, List<VehiclwInformation> vehiclwInformations)
        {
            string body = null;
            if (tokenShope != null && tokenShope != "")
            {
                foreach (var vech in vehiclwInformations)
                {
                    body += $"{vech.Year} {vech.Make} {vech.Model}\n";
                }
                var payload = new
                {
                    to = tokenShope,
                    content_available = true,
                    notification = new
                    {
                        click_action = "Oreder",
                        body = $"{body}",
                        title = $"Unassign Load Order Id: {idShip}",
                    },
                };
                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                }
                        }
                    }
                }
            }
        }
    }
}
