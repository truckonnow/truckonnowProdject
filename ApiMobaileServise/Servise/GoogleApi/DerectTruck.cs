using System.Collections.Generic;
using System.Drawing;
using ApiMobaileServise.Servise;
using DaoModels.DAO.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.IO;
using Google.Api.Gax.Grpc;

namespace ApiMobaileServise.Servise.GoogleApi
{
    public class DerectTruck : IDetect
    {
        private SqlCommandApiMobile sqlCommandApiMobil = null;
        private GoogleCredential credential = null;

        public void AuchGoole(SqlCommandApiMobile sqlCommandApiMobil)
        {
            try
            {
                this.sqlCommandApiMobil = sqlCommandApiMobil;
                credential = GoogleCredential.FromFile("../AuchConfig/Truckonnow-38b8427a812c.json");
            }
            catch(Exception e)
            {
                File.WriteAllText("GoogleCredential.txt", e.Message);
            }
        }

        public void DetectText(params object[] parames)
        {
            File.WriteAllText("1.txt", "1");
            try
            {
                var timeout = new TimeSpan(0, 0, 10);
                CallSettings callSettings = CallSettings.FromCallTiming(CallTiming.FromTimeout(timeout));
                List<Truck> trucks = sqlCommandApiMobil.GetTruck();
                string path = (string)parames[1];
                string idDriver = (string)parames[0];
                credential.CreateScoped(ImageAnnotatorClient.DefaultScopes);
                var channel = new Grpc.Core.Channel(
                    ImageAnnotatorClient.DefaultEndpoint.ToString(),
                    credential.ToChannelCredentials());
                var client = ImageAnnotatorClient.Create(channel, new ImageAnnotatorSettings() { CallSettings = callSettings });
                var image = Google.Cloud.Vision.V1.Image.FromFile(path);
                var response = client.DetectText(image);
                var response3 = client.DetectLocalizedObjects(image);
                File.WriteAllText("2.txt", "2");

                foreach (var localizedObject in response3)
                {
                    string numPlateTmp = "";
                    Truck truck = null;
                    foreach (EntityAnnotation text in response)
                    {
                        if (trucks.FirstOrDefault(t => t.PlateTruk == text.Description) != null)
                        {
                            truck = trucks.FirstOrDefault(t => t.PlateTruk == text.Description);
                            sqlCommandApiMobil.SetPlateTruck(truck.Id, idDriver);
                            break;
                        }
                        else if (truck != null && truck.PlateTruk.Contains(text.Description))
                        {
                            numPlateTmp += text.Description;
                        }
                        else if (truck == null && trucks.FirstOrDefault(t => t.PlateTruk.Contains(text.Description)) != null)
                        {
                            numPlateTmp += text.Description;
                            truck = trucks.FirstOrDefault(t => t.PlateTruk.Contains(text.Description));
                        }
                        if (numPlateTmp.Length >= 6)
                        {
                            if (truck != null && truck.PlateTruk == numPlateTmp)
                            {
                                sqlCommandApiMobil.SetPlateTruck(truck.Id, idDriver);
                                numPlateTmp = "";
                                break;
                            }
                            else if (truck.PlateTruk.Remove(truck.PlateTruk.Length - 3) == numPlateTmp || truck.PlateTruk.Remove(truck.PlateTruk.Length - 2) == numPlateTmp || truck.PlateTruk.Remove(truck.PlateTruk.Length - 1) == numPlateTmp)
                            {
                                sqlCommandApiMobil.SetPlateTruck(truck.Id, idDriver);
                                numPlateTmp = "";
                                break;
                            }
                            else if (numPlateTmp.Length > 7)
                            {
                                truck = null;
                                numPlateTmp = "";
                            }
                        }

                    }

                }
            }
            catch (Exception e)
            {
                File.WriteAllText("cc.txt", e.Message);
            }

            //return true;
        }
    }
}