using System.Collections.Generic;
using System.Drawing;
using ApiMobaileServise.Servise;
using DaoModels.DAO.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using System.Linq;

namespace ApiMobaileServise.Servise.GoogleApi
{
    public class DerectTruck : IDetect
    {
        private SqlCommandApiMobile sqlCommandApiMobil = null;
        private GoogleCredential credential = null;

        public void AuchGoole(SqlCommandApiMobile sqlCommandApiMobil)
        {
            this.sqlCommandApiMobil = sqlCommandApiMobil;
            credential = GoogleCredential.FromFile("../../../../AuchConfig/Truckonnow-f3c2a8947784.json");
        }

        public bool DetectText(params object[] parames)
        {
            List<Truck> trucks = sqlCommandApiMobil.GetTruck();
            string path = (string)parames[0];
            string idDriver = (string)parames[1];
            credential.CreateScoped(ImageAnnotatorClient.DefaultScopes);
            var channel = new Grpc.Core.Channel(
                ImageAnnotatorClient.DefaultEndpoint.ToString(),
                credential.ToChannelCredentials());
            var client = ImageAnnotatorClient.Create(channel);
            var image = Google.Cloud.Vision.V1.Image.FromFile(path);
            var response = client.DetectText(image);
            var response3 = client.DetectLocalizedObjects(image);
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

            return true;
        }
    }
}