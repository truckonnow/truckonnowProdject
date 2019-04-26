using DaoModels.DAO.Models;
using Newtonsoft.Json;
using Parser.DAO;
using Parser.Servise;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parser.Servise1
{
    public class ManagerInspactionDriver
    {
        private WebRequest tRequest = null;
        private SqlCommandParser sqlCommandParser = null;

        public ManagerInspactionDriver()
        {
            sqlCommandParser = new SqlCommandParser();
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAACa2vxR0:APA91bGTTHJgDmirQgd92-snbn5eixwi-sEPufe8fpl6EojstTcNNMjRnod7nAdUOw0C6InZvWOvom1xlRiWbojN7ObxGTeEPhjBtZ53ac2RLzIVuZc9_AdEkuix-vlul_ylJV7_ctEK"));
            tRequest.Headers.Add(string.Format("Sender: id={0}", "41568683293"));
            tRequest.ContentType = "application/json";
            WorkerInspactionDriver();
        }

        private void WorkerInspactionDriver()
        {
            LogEr.Logerr("Info", "Start Servse WorkParser Inspaction", "WorkerInspactionDriver", DateTime.Now.ToShortTimeString());
            int horseInmMiliSeconds = 60000 * 60;
            Task.Run(() =>
            {
                while (true)
                {
                    List<Driver> drivers = sqlCommandParser.GetDriverInDb();
                    if (CheckTime() && drivers != null)
                    {
                        RefreshInspectionTimeDriver(drivers);
                    }
                    else if (CheckTimeZeroTime() && drivers != null)
                    {
                        RefreshInspectionTodayTimeDriver(drivers);
                    }
                    Thread.Sleep(horseInmMiliSeconds);
                }
            }).GetAwaiter().GetResult();
        }

        private void RefreshInspectionTimeDriver(List<Driver> drivers)
        {
            foreach (var driver in drivers)
            {
                if (!driver.IsInspectionToDayDriver)
                {
                    sqlCommandParser.RefreshInspectionToDayDriverInDb(driver.Id);
                    SendNotyfyInspactionDrive(driver.TokenShope, "Truck Inspection", "Immediately go truck inspection or else you will not be able to continue working");
                }
            }
        }

        private void RefreshInspectionTodayTimeDriver(List<Driver> drivers)
        {
            foreach(var driver in drivers)
            {
                sqlCommandParser.RefreshInspectionToDayDriverInDb(driver.Id);
                SendNotyfyInspactionDrive(driver.TokenShope, "Truck Inspection", "You can pass the truck inspection now");
            }
        }

        private bool CheckTime()
        {
            bool isTime = false;
            if(12 < DateTime.Now.Hour && 14 > DateTime.Now.Hour)
            {
                isTime = true;
            }
            return isTime;
        }

        private bool CheckTimeZeroTime()
        {
            bool isTime = false;
            if (0 < DateTime.Now.Hour && 2 > DateTime.Now.Hour)
            {
                isTime = true;
            }
            return isTime;
        }

        public void SendNotyfyInspactionDrive(string tokenShope, string title, string body)
        {
            if (tokenShope != null && tokenShope != "")
            {
                var payload = new
                {
                    to = tokenShope,
                    content_available = true,
                    notification = new
                    {
                        click_action = "Driver",
                        body = body,
                        title = title
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
