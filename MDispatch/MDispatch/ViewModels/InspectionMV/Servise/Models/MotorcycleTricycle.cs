using MDispatch.NewElement;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public class MotorcycleTricycle : IVehicle
    {
        public string TypeIndex { get; set; } = "Tricycle";
        public string TypeVehicle { get; set; } = "motorcycle";
        public int CountCarImg { get; set; } = 23;

        public int GetIndexCar(int countPhoto)
        {
            int indecCar = 0;
            switch (countPhoto)
            {
                default:
                    {
                        indecCar = 0;
                        break;
                    }

            }
            return indecCar;
        }

        public string GetNameLayout(int inderxPhotoInspektion)
        {
            string nameLayout = "";
            switch (inderxPhotoInspektion)
            {
                
                case -1: nameLayout = "Rear belt mount vehicle on the driver's side"; break;
                case -2: nameLayout = "Front belt mount vehicle on the driver's side"; break;
                case -3: nameLayout = "Front belt mount vehicle on the passenger side"; break;
                case -4: nameLayout = "Rear belt mount vehicle on the passenger side"; break;
            }
            return nameLayout;
        }

        public async Task OrintableScreen(int inderxPhotoInspektion)
        {
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
        }
    }
}