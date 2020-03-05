using MDispatch.NewElement;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public class BikeSport : IVehicle
    {
        public string TypeIndex { get; set; } = "Sportbike";
        public int CountCarImg { get; set; } = 19;

        public int GetIndexCar(int countPhoto)
        {
            int indecCar = 0;
            return indecCar;
        }

        public string GetNameLayout(int inderxPhotoInspektion)
        {
            string nameLayout = "";
            return nameLayout;
        }

        public async Task OrintableScreen(int inderxPhotoInspektion)
        {
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
        }
    }
}