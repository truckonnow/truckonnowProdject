using DaoModels.DAO.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Grpc.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMobaileServise.Servise.GoogleApi
{
    public class DetectTrailers : IDetect
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
            List<Trailer> trailers = sqlCommandApiMobil.GetTrailers();
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
                Trailer trailer = null;
                foreach (EntityAnnotation text in response)
                {
                    if (trailers.FirstOrDefault(t => t.Plate == text.Description) != null)
                    {
                        trailer = trailers.FirstOrDefault(t => t.Plate == text.Description);
                        sqlCommandApiMobil.SetPlateTrailer(trailer.Id, idDriver);
                        break;
                    }
                    else if (trailer != null && trailer.Plate.Contains(text.Description))
                    {
                        numPlateTmp += text.Description;
                    }
                    else if (trailer == null && trailers.FirstOrDefault(t => t.Plate.Contains(text.Description)) != null)
                    {
                        numPlateTmp += text.Description;
                        trailer = trailers.FirstOrDefault(t => t.Plate.Contains(text.Description));
                    }
                    if (numPlateTmp.Length >= 6)
                    {
                        if (trailer != null && trailer.Plate == numPlateTmp)
                        {
                            sqlCommandApiMobil.SetPlateTrailer(trailer.Id, idDriver);
                            numPlateTmp = "";
                            break;
                        }
                        else if (trailer.Plate.Remove(trailer.Plate.Length - 3) == numPlateTmp || trailer.Plate.Remove(trailer.Plate.Length - 2) == numPlateTmp || trailer.Plate.Remove(trailer.Plate.Length - 1) == numPlateTmp)
                        {
                            sqlCommandApiMobil.SetPlateTrailer(trailer.Id, idDriver);
                            numPlateTmp = "";
                            break;
                        }
                        else if (numPlateTmp.Length > 7)
                        {
                            trailer = null;
                            numPlateTmp = "";
                        }
                    }

                }

            }

            return true;
        }
    }
}
