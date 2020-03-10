using MDispatch.NewElement;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public class CarSuv : IVehicle
    {
        public string TypeIndex { get; set; } = "Suv";
        public int CountCarImg { get; set; } = 36;

        public int GetIndexCar(int countPhoto)
        {
            int indecCar = 0;
            switch (countPhoto)
            {
                case 1:
                    {
                        indecCar = 5;
                        break;
                    }
                case 2:
                    {
                        indecCar = 6;
                        break;
                    }
                case 3:
                    {
                        indecCar = 17;
                        break;
                    }
                case 4:
                    {
                        indecCar = 12;
                        break;
                    }
                case 5:
                    {
                        indecCar = 13;
                        break;
                    }
                case 6:
                    {
                        indecCar = 14;
                        break;
                    }
                case 7:
                    {
                        indecCar = 22;
                        break;
                    }
                case 8:
                    {
                        indecCar = 23;
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

        public int GetIndexCarFullPhoto(int countPhoto)
        {
            int indecCar = 0;
            switch (countPhoto)
            {
                case 1:
                    {
                        indecCar = 23;
                        break;
                    }
                case 2:
                    {
                        indecCar = 7;
                        break;
                    }
                case 3:
                    {
                        indecCar = 6;
                        break;
                    }
                case 4:
                    {
                        indecCar = 24;
                        break;
                    }
                case 5:
                    {
                        indecCar = 26;
                        break;
                    }
                case 6:
                    {
                        indecCar = 13;
                        break;
                    }
                case 7:
                    {
                        indecCar = 25;
                        break;
                    }
                case 8:
                    {
                        indecCar = 15;
                        break;
                    }
                case 9:
                    {
                        indecCar = 12;
                        break;
                    }
                case 10:
                    {
                        indecCar = 22;
                        break;
                    }
                case 11:
                    {
                        indecCar = 32;
                        break;
                    }
                case 12:
                    {
                        indecCar = 31;
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
                nameLayout = "Vehicle(SUV) dashboard";
            }
            else if (inderxPhotoInspektion == 2)
            {
                nameLayout = "Vehicle(SUV) interior";
            }
            else if (inderxPhotoInspektion == 3)
            {
                nameLayout = "Vehicle(SUV) interior";
            }
            else if (inderxPhotoInspektion == 4)
            {
                nameLayout = "Driving door from inside the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 5)
            {
                nameLayout = "Front door from inside the vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 6)
            {
                nameLayout = "Rearview mirror vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 7)
            {
                nameLayout = "Rearview mirror vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 8)
            {
                nameLayout = "Front of the vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 9)
            {
                nameLayout = "Front wheel of the vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 10)
            {
                nameLayout = "Front of the vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 11)
            {
                nameLayout = "Right front headlight of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 12)
            {
                nameLayout = "The right side of the front bumper of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 13)
            {
                nameLayout = "The centr side of the front bumper of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 14)
            {
                nameLayout = "The left side of the front bumper of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 15)
            {
                nameLayout = "Left front headlight of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 16)
            {
                nameLayout = "Vehicle(SUV) hood";
            }
            else if (inderxPhotoInspektion == 17)
            {
                nameLayout = "Vehicle(SUV) Windshield";
            }
            else if (inderxPhotoInspektion == 18)
            {
                nameLayout = "The entire front of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 19)
            {
                nameLayout = "Front of the vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 20)
            {
                nameLayout = "Front of the vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 21)
            {
                nameLayout = "Front wheel of the vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 22)
            {
                nameLayout = "Front door from inside the vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 23)
            {
                nameLayout = "Rearview mirror vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 24)
            {
                nameLayout = "Rearview mirror vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 25)
            {
                nameLayout = "Rear door of the vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 26)
            {
                nameLayout = "Rear of the vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 27)
            {
                nameLayout = "Rear wheel of the vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 28)
            {
                nameLayout = "All part of the vehicle(SUV) on the passenger side";
            }
            else if (inderxPhotoInspektion == 29)
            {
                nameLayout = "The right side of the rear bumper of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 30)
            {
                nameLayout = "The center side of the rear bumper of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 31)
            {
                nameLayout = "Vehicle(SUV) Rear Window";
            }
            else if (inderxPhotoInspektion == 32)
            {
                nameLayout = "The left side of the rear bumper of the vehicle(SUV)";
            }
            else if (inderxPhotoInspektion == 33)
            {
                nameLayout = "Rear of the vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 34)
            {
                nameLayout = "Rear wheel of the vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 35)
            {
                nameLayout = "Rear door of the vehicle(SUV) on the driver's side";
            }
            else if (inderxPhotoInspektion == 36)
            {
                nameLayout = "All part of the vehicle(SUV) on the driver's side";
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
            //if (inderxPhotoInspektion == 2 || inderxPhotoInspektion == 3 )
            //{
            //    DependencyService.Get<IOrientationHandler>().ForceSensor();
            //}
            //else if (inderxPhotoInspektion == 7 || inderxPhotoInspektion == 8 || inderxPhotoInspektion == 9 || inderxPhotoInspektion == 10 || inderxPhotoInspektion == 10
            //    || inderxPhotoInspektion == 12 || inderxPhotoInspektion == 13 || inderxPhotoInspektion == 14 || inderxPhotoInspektion == 15 || inderxPhotoInspektion == 16
            //    || inderxPhotoInspektion == 17 || inderxPhotoInspektion == 18 || inderxPhotoInspektion == 19 || inderxPhotoInspektion == 20 || inderxPhotoInspektion == 22
            //    || inderxPhotoInspektion == 23 || inderxPhotoInspektion == 26 || inderxPhotoInspektion == 27 || inderxPhotoInspektion == 33 || inderxPhotoInspektion == 35)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForcePortrait();
            //}
            //else if (inderxPhotoInspektion == 1 || inderxPhotoInspektion == 4 || inderxPhotoInspektion == 5 || inderxPhotoInspektion == 6 || inderxPhotoInspektion == 21
            //    || inderxPhotoInspektion == 24 || inderxPhotoInspektion == 25 || inderxPhotoInspektion == 25 || inderxPhotoInspektion == 28 || inderxPhotoInspektion == 29 
            //    || inderxPhotoInspektion == 30 || inderxPhotoInspektion == 31 || inderxPhotoInspektion == 32 || inderxPhotoInspektion == 34 || inderxPhotoInspektion == 36)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForceLandscape();
            //}
        }
    }
}