using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using MDispatch.NewElement.FilleReder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace MDispatch.Service
{
    public class GoogleApi
    {
        private GoogleCredential credential = null;

        private async void Avtorization()
        {
            credential = GoogleCredential.FromJson(await DependencyService.Get<IFileWorker>().LoadTextAsync("Truckonnow-8fb90a135d42.json"));
        }

        internal int DetectPlate(byte[] res, List<string> plateTrucks, List<string> plateTrailers, string type, ref string plate)
        {
            Avtorization();
            string content = null;
            try
            {
                credential.CreateScoped(ImageAnnotatorClient.DefaultScopes);
                var channel = new Grpc.Core.Channel(
                    ImageAnnotatorClient.DefaultEndpoint.ToString(),
                    credential.ToChannelCredentials());
                var client = ImageAnnotatorClient.Create(channel);
                var image = Google.Cloud.Vision.V1.Image.FromBytes(res);
                var response = client.DetectText(image);
                if (type == "truck")
                {
                    plate = SelectTextTruck(response, plateTrucks); 
                }
            }
            catch (Exception)
            {
                return 4;
            }
            return 3;
        }

        private string SelectTextTruck(IReadOnlyList<EntityAnnotation> entityAnnotations, List<string> plateTrucks)
        {
            string numPlateTmp = "";
            string plate = null;
            foreach (EntityAnnotation text in entityAnnotations)
            {
                if (plateTrucks.FirstOrDefault(t => t == text.Description) != null)
                {
                    numPlateTmp = plateTrucks.FirstOrDefault(t => t == text.Description);
                    break;
                }
                else if (plate != null && plate.Contains(text.Description))
                {
                    numPlateTmp += text.Description;
                }
                else if (plate == null && plateTrucks.FirstOrDefault(t => t.Contains(text.Description)) != null)
                {
                    numPlateTmp += text.Description;
                    plate = plateTrucks.FirstOrDefault(t => t.Contains(text.Description));
                }
                if (numPlateTmp.Length >= 6)
                {
                    if (plate != null && plate == numPlateTmp)
                    {
                        break;
                    }
                    else if (plate.Remove(plate.Length - 3) == numPlateTmp || plate.Remove(plate.Length - 2) == numPlateTmp || plate.Remove(plate.Length - 1) == numPlateTmp)
                    {
                        //sqlCommandApiMobil.SetPlateTruck(truck.Id, idDriver);
                        numPlateTmp = plate;
                        break;
                    }
                    else if (numPlateTmp.Length > 7)
                    {
                        plate = null;
                        numPlateTmp = "";
                    }
                }

            }
            return numPlateTmp;
        }
    }
}