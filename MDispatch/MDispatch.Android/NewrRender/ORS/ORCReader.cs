using MDispatch.Droid.NewRender.ORC;
using MDispatch.Service.Tasks;
using System.Threading.Tasks;
using Tesseract;
using Tesseract.Droid;


[assembly: Xamarin.Forms.Dependency(typeof(ORCReader))]
namespace MDispatch.Droid.NewRender.ORC
{
    public class ORCReader : IORC
    {
        async Task<string> IORC.ORCWorkDashbordVehicle(byte[] data)
        {
            string textResult = null;
            ITesseractApi api = new TesseractApi(Android.App.Application.Context, AssetsDeployment.OncePerVersion);
            bool initialised = await api.Init("eng");
            if (initialised)
            {
                bool success = await api.SetImage(data);
                if (success)
                {
                    textResult = api.Text;
                }
            }
            return textResult;
        }
    }
}