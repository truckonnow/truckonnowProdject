using DaoModels.DAO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebDispacher.Notyfy
{
    public class ManagerNotify
    {
        public void SendNotyfy(Driver driver, Shipping shipping)
        {
            string body = null;
            foreach(var vech in shipping.VehiclwInformations)
            {
                body += $"\n{vech.Year} {vech.Make} {vech.Model}";
            }
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAACa2vxR0:APA91bGTTHJgDmirQgd92-snbn5eixwi-sEPufe8fpl6EojstTcNNMjRnod7nAdUOw0C6InZvWOvom1xlRiWbojN7ObxGTeEPhjBtZ53ac2RLzIVuZc9_AdEkuix-vlul_ylJV7_ctEK"));
            tRequest.Headers.Add(string.Format("Sender: id={0}", "41568683293"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = driver.TokenShope,
                //to = "/topics/LOL",
                content_available = true,
                notification = new
                {
                    click_action = "No Action",
                    body = $"Added new order,{body}",
                    title = $"New Load Order Id: {shipping.idOrder}",
                },
            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                //using (WebResponse tResponse = tRequest.GetResponse())
                //{
                //    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                //    {
                //        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                //            {
                //                String sResponseFromServer = tReader.ReadToEnd();
                //            }
                //    }
                //}
            }
        }
    }
}
