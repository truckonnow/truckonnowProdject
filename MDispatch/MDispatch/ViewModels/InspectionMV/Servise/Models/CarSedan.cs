using MDispatch.NewElement;
using MDispatch.ViewModels.AskPhoto;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public class CarSedan : ICar
    {
        public string typeIndex { get; set; } = "Sedan";
        public int CountCarImg { get; set; } = 33;

        public int GetIndexCar(int countPhoto)
        {
            int indecCar = 0;
            switch (countPhoto)
            {
                case 1:
                    {
                        indecCar = 7;
                        break;
                    }
                case 2:
                    {
                        indecCar = 16;
                        break;
                    }
                case 3:
                    {
                        indecCar = 19;
                        break;
                    }
                case 4:
                    {
                        indecCar = 23;
                        break;
                    }
                case 5:
                    {
                        indecCar = 22;
                        break;
                    }
                case 6:
                    {
                        indecCar = 21;
                        break;
                    }
                case 7:
                    {
                        indecCar = 20;
                        break;
                    }
                case 8:
                    {
                        indecCar = 2;
                        break;
                    }
                case 9:
                    {
                        indecCar = 15;
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
                        indecCar = 4;
                        break;
                    }
                case 2:
                    {
                        indecCar = 3;
                        break;
                    }
                case 3:
                    {
                        indecCar = 2;
                        break;
                    }
                case 4:
                    {
                        indecCar = 1;
                        break;
                    }
                case 5:
                    {
                        indecCar = 19;
                        break;
                    }

                case 6:
                    {
                        indecCar = 23;
                        break;
                    }
                case 7:
                    {
                        indecCar = 20;
                        break;
                    }
                case 8:
                    {
                        indecCar = 21;
                        break;
                    }
                case 9:
                    {
                        indecCar = 22;
                        break;
                    }
                case 10:
                    {
                        indecCar = 5;
                        break;
                    }
                case 11:
                    {
                        indecCar = 6;
                        break;
                    }
                case 12:
                    {
                        indecCar = 7;
                        break;
                    }
                case 13:
                    {
                        indecCar = 8;
                        break;
                    }
                case 14:
                    {
                        indecCar = 25;
                        break;
                    }
                case 15:
                    {
                        indecCar = 26;
                        break;
                    }
                case 16:
                    {
                        indecCar = 27;
                        break;
                    }
                case 17:
                    {
                        indecCar = 33;
                        break;
                    }
                case 18:
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
                nameLayout = "Front left side of the vehicle";
            }
            else if (inderxPhotoInspektion == 2)
            {
                nameLayout = "Front left vehicle door";
            }
            else if (inderxPhotoInspektion == 3)
            {
                nameLayout = "Rear left vehicle door";
            }
            else if (inderxPhotoInspektion == 4)
            {
                nameLayout = "Rear left of the vehicle";
            }
            else if (inderxPhotoInspektion == 5)
            {
                nameLayout = "Rear right side of the vehicle";
            }
            else if (inderxPhotoInspektion == 6)
            {
                nameLayout = "Rear right door of the vehicle";
            }
            else if (inderxPhotoInspektion == 7)
            {
                nameLayout = "Front right door of the vehicle";
            }
            else if (inderxPhotoInspektion == 8)
            {
                nameLayout = "Front right side of the vehicle";
            }
            else if (inderxPhotoInspektion == 9)
            {
                nameLayout = "Front right wheel of the vehicle";
            }
            else if (inderxPhotoInspektion == 10)
            {
                nameLayout = "Rear right wheel of the vehicle";
            }
            else if (inderxPhotoInspektion == 11)
            {
                nameLayout = "Rear left wheel of the vehicle";
            }
            else if (inderxPhotoInspektion == 12)
            {
                nameLayout = "Front left wheel of the vehicle";
            }
            else if (inderxPhotoInspektion == 13)
            {
                nameLayout = "Right headlight of the vehicle";
            }
            else if (inderxPhotoInspektion == 14)
            {
                nameLayout = "Left headlight of the vehicle";
            }
            else if (inderxPhotoInspektion == 15)
            {
                nameLayout = "Right rearview mirror vehicle";
            }
            else if (inderxPhotoInspektion == 16)
            {
                nameLayout = "Left rearview mirror of the vehicle";
            }
            else if (inderxPhotoInspektion == 17)
            {
                nameLayout = "Left rearview mirror of the vehicle (From the bottom)";
            }
            else if (inderxPhotoInspektion == 18)
            {
                nameLayout = "Right rearview mirror of the vehicle (From the bottom)";
            }
            else if (inderxPhotoInspektion == 19)
            {
                nameLayout = "Vehicle front windshield";
            }
            else if (inderxPhotoInspektion == 20)
            {
                nameLayout = "The right side of the front bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 21)
            {
                nameLayout = "The center side of the front bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 22)
            {
                nameLayout = "The left side of the front bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 23)
            {
                nameLayout = "Vehicle hood";
            }
            else if (inderxPhotoInspektion == 24)
            {
                nameLayout = "Roof behind the vehicle";
            }
            else if (inderxPhotoInspektion == 25)
            {
                nameLayout = "The left side of the rear bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 26)
            {
                nameLayout = "The center side of the rear bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 27)
            {
                nameLayout = "The right side of the rear bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 28)
            {
                nameLayout = "Vehicle dashboard";
            }
            else if (inderxPhotoInspektion == 29)
            {
                nameLayout = "Rear left corner of the vehicle";
            }
            else if (inderxPhotoInspektion == 30)
            {
                nameLayout = "Rear right corner of the vehicle";
            }
            else if (inderxPhotoInspektion == 31)
            {
                nameLayout = "Driver's door from inside the vehiclee";
            }
            else if (inderxPhotoInspektion == 32)
            {
                nameLayout = "The driver's seat of the vehicle";
            }
            else if (inderxPhotoInspektion == 33)
            {
                nameLayout = "Vehicle interior";
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