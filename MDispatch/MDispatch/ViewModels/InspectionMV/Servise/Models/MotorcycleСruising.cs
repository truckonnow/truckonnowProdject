using MDispatch.NewElement;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public class MotorcycleСruising : IVehicle
    {
        public string TypeIndex { get; set; } = "Сruisingmotorcycle";
        public int CountCarImg { get; set; } = 11;
        public string TypeVehicle { get; set; } = "motorcycle";

        public int GetIndexCar(int countPhoto)
        {
            int indecCar = 0;
            switch (countPhoto)
            {
                case 1:
                    {
                        indecCar = -1;
                        break;
                    }
                case 2:
                    {
                        indecCar = 5;
                        break;
                    }
                case 3:
                    {
                        indecCar = 4;
                        break;
                    }
                case 4:
                    {
                        indecCar = -2;
                        break;
                    }
                case 5:
                    {
                        indecCar = 10;
                        break;
                    }
                case 6:
                    {
                        indecCar = -3;
                        break;
                    }
                case 7:
                    {
                        indecCar = 9;
                        break;
                    }
                case 8:
                    {
                        indecCar = -4;
                        break;
                    }
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
            switch(inderxPhotoInspektion)
            {
                case 1: nameLayout = "Vehicle(Cruise Motorcycle) Dashboard"; break;
                case 2: nameLayout = "Vehicle(Cruise Motorcycle) Petrol Tank "; break;
                case 3: nameLayout = "All Right Side of the Vehicle(Cruise Motorcycle)"; break;
                case 4: nameLayout = "Front wheel on the right of the vehicle(Cruise motorcycle)"; break;
                case 5: nameLayout = "Central Right of the Vehicle(Cruise Motorcycle)"; break;
                case 6: nameLayout = "Rear Wheel of Vehicle(Cruise Motorcycle) Right "; break;
                case 7: nameLayout = "Vehicle(Cruise motorcycle) at the rear"; break;
                case 8: nameLayout = "Rear Wheel of Vehicle(Cruise Motorcycle) Left"; break;
                case 9: nameLayout = "Central Left of the Vehicle(Cruise Motorcycle)"; break;
                case 10: nameLayout = "Front wheel on the left of the vehicle (Cruise motorcycle)"; break;
                case 11: nameLayout = "All Left Side of the Vehicle(Cruise Motorcycle)"; break;
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