using DaoModels.DAO.Models;
using Newtonsoft.Json;
using Parser.DAO;
using Parser.Servise;
using System;
using System.Collections.Generic;
using System.IO;
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

        private void InitReqvest()
        {
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAACa2vxR0:APA91bGTTHJgDmirQgd92-snbn5eixwi-sEPufe8fpl6EojstTcNNMjRnod7nAdUOw0C6InZvWOvom1xlRiWbojN7ObxGTeEPhjBtZ53ac2RLzIVuZc9_AdEkuix-vlul_ylJV7_ctEK"));
            tRequest.Headers.Add(string.Format("Sender: id={0}", "41568683293"));
            tRequest.ContentType = "application/json";
        }

        private void WorkerInspactionDriver()
        {
            LogEr.Logerr("Info1", "Start Servse WorkParser Inspaction", "WorkerInspactionDriver", DateTime.Now.ToShortTimeString());
            int horseInmMiliSeconds = 60000 * 60;
            Task.Run(() =>
            {
                while (true)
                {
                    List<Driver> drivers = sqlCommandParser.GetDriverInDb();
                    if (CheckTodayTime() && drivers != null)
                    {
                        RefreshInspectionTodayTimeDriver(drivers);
                    }
                    if (CheckTime() && drivers != null)
                    {
                        RefreshInspectionTimeDriver(drivers);
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
                    LogEr.Logerr("Info1", $"check on the driver \"{driver.Id}\" to pass inspection", "RefreshInspectionTimeDriver", DateTime.Now.ToShortTimeString());
                    sqlCommandParser.RefreshInspectionDriverInDb(driver.Id);
                    SendNotyfyInspactionDrive(driver.TokenShope, "Truck Inspection", "Immediately go truck inspection or else you will not be able to continue working");
                }
            }
        }

        private void RefreshInspectionTodayTimeDriver(List<Driver> drivers)
        {
            foreach(var driver in drivers)
            {
                LogEr.Logerr("Info1", $"driver status change \"{driver.Id}\"", "RefreshInspectionTodayTimeDriver", DateTime.Now.ToShortTimeString());
                sqlCommandParser.RefreshInspectionToDayDriverInDb(driver.Id);
                SendNotyfyInspactionDrive(driver.TokenShope, "Truck Inspection", "You can pass the truck inspection now");
            }
        }

        private bool CheckTime()
        {
            bool isTime = false;
            if(11 <= DateTime.Now.Hour && 12 > DateTime.Now.Hour)
            {
                LogEr.Logerr("Info1", $"Time 11 <= DateTime.Now.Hour && 12 > DateTime.Now.Hour", "CheckTime", DateTime.Now.ToShortTimeString());
                isTime = true;
            }
            return isTime;
        }

        private bool CheckTodayTime()
        {
            bool isTime = false;
            if (5 <= DateTime.Now.Hour && 6 > DateTime.Now.Hour)
            {
                LogEr.Logerr("Info1", $"Time 5 <= DateTime.Now.Hour && 6 > DateTime.Now.Hour", "CheckTodayTime", DateTime.Now.ToShortTimeString());
                isTime = true;
            }
            return isTime;
        }

        public void SendNotyfyInspactionDrive(string tokenShope, string title, string body)
        {
            if (tokenShope != null && tokenShope != "")
            {

                LogEr.Logerr("Info1", $"Send notyfy {body}", "SendNotyfyInspactionDrive", DateTime.Now.ToShortTimeString());
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
            InitReqvest();
        }
    }
}