using MDispatch.NewElement;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public class MotorcycleSport : IVehicle
    {
        public string TypeIndex { get; set; } = "Sportbike";
        public int CountCarImg { get; set; } = 19;

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
                        indecCar = 3;
                        break;
                    }
                case 3:
                    {
                        indecCar = -2;
                        break;
                    }
                case 4:
                    {
                        indecCar = 4;
                        break;
                    }
                case 5:
                    {
                        indecCar = 7;
                        break;
                    }
                case 6:
                    {
                        indecCar = 6;
                        break;
                    }
                case 7:
                    {
                        indecCar = 8;
                        break;
                    }
                case 8:
                    {
                        indecCar = 12;
                        break;
                    }
                case 9:
                    {
                        indecCar = -3;
                        break;
                    }
                case 13:
                    {
                        indecCar = 13;
                        break;
                    }
                case 14:
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
            if (inderxPhotoInspektion == 1)
            {
                nameLayout = "Dashboard sportbike";
            }
            else if (inderxPhotoInspektion == 2)
            {
                nameLayout = "Dashboard and sportbike windshield";
            }
            else if (inderxPhotoInspektion == 3)
            {
                nameLayout = "Front of the sport bike on the right";
            }
            else if (inderxPhotoInspektion == 4)
            {
                nameLayout = "Rear view mirror sport bike on the right";
            }
            else if (inderxPhotoInspektion == 5)
            {
                nameLayout = "Front wheel of a sport bike on the right";
            }
            else if (inderxPhotoInspektion == 6)
            {
                nameLayout = "Sport bike headlights";
            }
            else if (inderxPhotoInspektion == 7)
            {
                nameLayout = "Sportbike windshield";
            }
            else if (inderxPhotoInspektion == 8)
            {
                nameLayout = "Front sport bike";
            }
            else if (inderxPhotoInspektion == 9)
            {
                nameLayout = "Right front headlight sport bike";
            }
            else if (inderxPhotoInspektion == 10)
            {
                nameLayout = "Left front headlight sport bike";
            }
            else if (inderxPhotoInspektion == 11)
            {
                nameLayout = "Front wheel of a sport bike on the left";
            }
            else if (inderxPhotoInspektion == 12)
            {
                nameLayout = "Front of the sport bike on the left";
            }
            else if (inderxPhotoInspektion == 13)
            {
                nameLayout = "Rear view mirror sport bike on the left";
            }
            else if (inderxPhotoInspektion == 14)
            {
                nameLayout = "Whole side sports bike on the left";
            }
            else if (inderxPhotoInspektion == 15)
            {
                nameLayout = "----------";
            }
            else if (inderxPhotoInspektion == 16)
            {
                nameLayout = "The back of the sports bike on the left";
            }
            else if (inderxPhotoInspektion == 17)
            {
                nameLayout = "Rear wheel sports bike left";
            }
            else if (inderxPhotoInspektion == 18)
            {
                nameLayout = "Rear wheel sports bike right";
            }
            else if (inderxPhotoInspektion == 19)
            {
                nameLayout = "The back of the sports bike on the right";
            }
            else if (inderxPhotoInspektion == -1)
            {
                nameLayout = "Rear belt mount vehicle on the driver's side";
            }
            else if (inderxPhotoInspektion == -2)
            {
                nameLayout = "Front belt mount vehicle on the driver's side";
            }
            else if (inderxPhotoInspektion == -3)
            {
                nameLayout = "Front belt mount vehicle on the passenger side";
            }
            else if (inderxPhotoInspektion == -4)
            {
                nameLayout = "Rear belt mount vehicle on the passenger side";
            }
            return nameLayout;
        }

        public async Task OrintableScreen(int inderxPhotoInspektion)
        {
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
        }
    }
}