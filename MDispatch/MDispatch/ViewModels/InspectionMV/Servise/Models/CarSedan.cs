using MDispatch.NewElement;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public class CarSedan : IVehicle
    {
        public string TypeIndex { get; set; } = "Sedan";
        public int CountCarImg { get; set; } = 33;

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
                        indecCar = 6;
                        break;
                    }
                case 4:
                    {
                        indecCar = -1;
                        break;
                    }
                case 5:
                    {
                        indecCar = 16;
                        break;
                    }
                case 6:
                    {
                        indecCar = 10;
                        break;
                    }
                case 7:
                    {
                        indecCar = 12;
                        break;
                    }
                case 8:
                    {
                        indecCar = 13;
                        break;
                    }
                case 9:
                    {
                        indecCar = -3;
                        break;
                    }
                case 10:
                    {
                        indecCar = 20;
                        break;
                    }
                case 11:
                    {
                        indecCar = 21;
                        break;
                    }
                case 12:
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
                nameLayout = "Vehicle(Sedan) dashboard";
            }
            else if (inderxPhotoInspektion == 2)
            {
                nameLayout = "Vehicle(Sedan) driver's seat";
            }
            else if (inderxPhotoInspektion == 3)
            {
                nameLayout = "Vehicle(Sedan) interior";
            }
            else if (inderxPhotoInspektion == 4)
            {
                nameLayout = "Driving door from inside the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 5)
            {
                nameLayout = "Front door on the driver’s side of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 6)
            {
                nameLayout = "Rear view mirror on the driver’s side of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 7)
            {
                nameLayout = "Rear view mirror on the driver’s side of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 8)
            {
                nameLayout = "Front of the vehicle(Sedan), driver's side";
            }
            else if (inderxPhotoInspektion == 9)
            {
                nameLayout = "Front wheel of the vehicle(Sedan), driver's side";
            }
            else if (inderxPhotoInspektion == 10)
            {
                nameLayout = "Right side of the front bumper of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 11)
            {
                nameLayout = "Right front headlight of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 12)
            {
                nameLayout = "Center side of the front bumper of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 13)
            {
                nameLayout = "Left side of the front bumper of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 14)
            {
                nameLayout = "Left front headlight of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 15)
            {
                nameLayout = "Vehicle(Sedan) hood";
            }
            else if (inderxPhotoInspektion == 16)
            {
                nameLayout = "Vehicle(Sedan) Windshield";
            }
            else if (inderxPhotoInspektion == 17)
            {
                nameLayout = "----------(Sedan)";
            }
            else if (inderxPhotoInspektion == 18)
            {
                nameLayout = "Front of the vehicle(Sedan), passenger side";
            }
            else if (inderxPhotoInspektion == 19)
            {
                nameLayout = "Front wheel of the vehicle(Sedan), passenger side";
            }
            else if (inderxPhotoInspektion == 20)
            {
                nameLayout = "Front door on the passenger side of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 21)
            {
                nameLayout = "Rear view mirror on the passenger side of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 22)
            {
                nameLayout = "Rear view mirror on the passenger side of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 23)
            {
                nameLayout = "The rear door of the vehicle(Sedan) on the passenger side";
            }
            else if (inderxPhotoInspektion == 24)
            {
                nameLayout = "Rear wheel of the vehicle(Sedan), passenger side";
            }
            else if (inderxPhotoInspektion == 25)
            {
                nameLayout = "Rear of the vehicle(Sedan) on the passenger side";
            }
            else if (inderxPhotoInspektion == 26)
            {
                nameLayout = "All part of the vehicle(Sedan) on the passenger side";
            }
            else if (inderxPhotoInspektion == 27)
            {
                nameLayout = "The right side of the rear bumper of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 28)
            {
                nameLayout = "The center side of the rear bumper of the vehicle(Sedan)";
            }
            else if (inderxPhotoInspektion == 29)
            {
                nameLayout = "The left side of the rear bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 30)
            {
                nameLayout = "All part of the vehicle(Sedan) on the driver's side";
            }
            else if (inderxPhotoInspektion == 31)
            {
                nameLayout = "Rear of the vehicle(Sedan) on the driver's side";
            }
            else if (inderxPhotoInspektion == 32)
            {
                nameLayout = "Rear wheel of the vehicle(Sedan), driver's side";
            }
            else if (inderxPhotoInspektion == 33)
            {
                nameLayout = "The rear door of the vehicle(Sedan) on the driver's side";
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
            //if (inderxPhotoInspektion == 2 || inderxPhotoInspektion == 3 || inderxPhotoInspektion == 4 || inderxPhotoInspektion == 5 || inderxPhotoInspektion == 6 || inderxPhotoInspektion == 7  || inderxPhotoInspektion == 25 
            //    || inderxPhotoInspektion == 26 || inderxPhotoInspektion == 27 || inderxPhotoInspektion == 32 || inderxPhotoInspektion == 20 || inderxPhotoInspektion == 22)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForcePortrait();
            //}
            //else if (inderxPhotoInspektion == 1 || inderxPhotoInspektion == 8 || inderxPhotoInspektion == 9 || inderxPhotoInspektion == 10 || inderxPhotoInspektion == 11 || inderxPhotoInspektion == 12 || inderxPhotoInspektion == 13 || inderxPhotoInspektion == 14
            //    || inderxPhotoInspektion == 15 || inderxPhotoInspektion == 16 || inderxPhotoInspektion == 17 || inderxPhotoInspektion == 18 || inderxPhotoInspektion == 19  || inderxPhotoInspektion == 23
            //    || inderxPhotoInspektion == 24 || inderxPhotoInspektion == 28 || inderxPhotoInspektion == 29 || inderxPhotoInspektion == 30 || inderxPhotoInspektion == 31 || inderxPhotoInspektion == 33 || inderxPhotoInspektion == 21)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForceLandscape();
            //}
        }
    }
}