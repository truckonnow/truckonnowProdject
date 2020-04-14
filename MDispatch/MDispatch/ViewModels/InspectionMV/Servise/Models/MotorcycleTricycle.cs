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
                case 1: nameLayout = "Vehicle(Tricycle) dashboard"; break;
                case 2: nameLayout = "Vehicle(Tricycle) driver seat"; break;
                case 3: nameLayout = "Vehicle(Tricycle) interior"; break;
                case 4: nameLayout = "The central part of the vehicle(Tricycle) on the driver’s side"; break;
                case 5: nameLayout = "Rear-view mirror of the vehicle(Tricycle) on the driver side"; break;
                case 6: nameLayout = "Front of the vehicle(Tricycle), driver's side"; break;
                case 7: nameLayout = "The front wheel of the vehicle(Tricycle) the driver's side"; break;
                case 8: nameLayout = "The right side of the front bumper of the vehicle(Tricycle)"; break;
                case 9: nameLayout = "The central side of the front bumper of the vehicle(Tricycle)"; break;
                case 10: nameLayout = "The central side of the front bumper of the vehicle(Tricycle)"; break;
                case 11: nameLayout = "The left side of the front bumper of the vehicle(Tricycle)"; break;
                case 12: nameLayout = "The entire lower part of the front bumper of the vehicle(Tricycle)"; break;
                case 13: nameLayout = "Front of the vehicle(Tricycle), passenger side"; break;
                case 14: nameLayout = "The front wheel of the vehicle(Tricycle) the passenger side"; break;
                case 15: nameLayout = "The front wheel of the vehicle(Tricycle) the passenger side"; break;
                case 16: nameLayout = "The central part of the vehicle(Tricycle) on the passenger side"; break;
                case 17: nameLayout = "Rear of the vehicle(Tricycle) on the passenger side"; break;
                case 18: nameLayout = "The rear wheel of the vehicle(Tricycle) from the passenger side"; break;
                case 19: nameLayout = "Vehicle(Tricycle)"; break;
                case 20: nameLayout = "Vehicle(Tricycle) Rear Wheel Hitch Place"; break;
                case 21: nameLayout = "Vehicle(Tricycle) Rear Wheel Hitch Place"; break;
                case 22: nameLayout = "The rear wheel of the vehicle(Tricycle) from the driver’s side"; break;
                case 23: nameLayout = "Rear of the vehicle(Tricycle) on the driver’s side"; break;

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