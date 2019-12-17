using MDispatch.NewElement;
using MDispatch.ViewModels.AskPhoto;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Models
{
    public class CarPickUp : ICar
    {
        public string typeIndex { get; set; } = "Pick Up";
        public int CountCarImg { get; set; } = 35;

        public string GetNameLayout(int inderxPhotoInspektion)
        {
            string nameLayout = "";
            if(inderxPhotoInspektion == 1)
            {
                nameLayout = "Pickup dashboard";
            }
            else if(inderxPhotoInspektion == 2)
            {
                nameLayout = "Pickup Salon";
            }
            else if (inderxPhotoInspektion == 3)
            {
                nameLayout = "Driver seat";
            }
            else if (inderxPhotoInspektion == 4)
            {
                nameLayout = "Driver seat";
            }
            else if (inderxPhotoInspektion == 5)
            {
                nameLayout = "Front driver door";
            }
            else if (inderxPhotoInspektion == 6)
            {
                nameLayout = "Front of the pickUp, driver's side";
            }
            else if (inderxPhotoInspektion == 7)
            {
                nameLayout = "Rear-view mirror of a pickup driver's side";
            }
            else if (inderxPhotoInspektion == 8)
            {
                nameLayout = "Rear-view mirror of a pickup driver's side";
            }
            else if (inderxPhotoInspektion == 9)
            {
                nameLayout = "Front of the pickUp, driver's side";
            }
            else if (inderxPhotoInspektion == 10)
            {
                nameLayout = "The front wheel of a pickup driver's side";
            }
            else if (inderxPhotoInspektion == 11)
            {
                nameLayout = "The right side of the front bumper of the pickup";
            }
            else if (inderxPhotoInspektion == 12)
            {
                nameLayout = "Right front headlight of a pickup bumper";
            }
            else if (inderxPhotoInspektion == 13)
            {
                nameLayout = "The center side of the front bumper of the pickup";
            }
            else if (inderxPhotoInspektion == 14)
            {
                nameLayout = "The left side of the front bumper of the pickup";
            }
            else if (inderxPhotoInspektion == 15)
            {
                nameLayout = "Left front headlight of a pickup bumper";
            }
            else if (inderxPhotoInspektion == 16)
            {
                nameLayout = "Hood of a pickup";
            }
            else if (inderxPhotoInspektion == 17)
            {
                nameLayout = "Windshield pickup";
            }
            else if (inderxPhotoInspektion == 18)
            {
                nameLayout = "Pickup roof";
            }
            else if (inderxPhotoInspektion == 19)
            {
                nameLayout = "Front of the Pickup on the passenger side";
            }
            else if (inderxPhotoInspektion == 20)
            {
                nameLayout = "The front wheel of a pickup passenger side";
            }
            else if (inderxPhotoInspektion == 21)
            {
                nameLayout = "Front of the Pickup on the passenger side";
            }
            else if (inderxPhotoInspektion == 22)
            {
                nameLayout = "Rear-view mirror of a pickup passenger side";
            }
            else if (inderxPhotoInspektion == 23)
            {
                nameLayout = "Rear-view mirror of a pickup passenger side";
            }
            else if (inderxPhotoInspektion == 24)
            {
                nameLayout = "Front passenger door";
            }
            else if (inderxPhotoInspektion == 25)
            {
                nameLayout = "Rear passenger door";
            }
            else if (inderxPhotoInspektion == 26)
            {
                nameLayout = "Rear of the Pickup on the passenger side";
            }
            else if (inderxPhotoInspektion == 27)
            {
                nameLayout = "The rear wheel of a pickup passenger side";
            }
            else if (inderxPhotoInspektion == 28)
            {
                nameLayout = "Rear of the Pickup on the passenger side";
            }
            else if (inderxPhotoInspektion == 29)
            {
                nameLayout = "The right side of the rear bumper";
            }
            else if (inderxPhotoInspektion == 30)
            {
                nameLayout = "The centr side of the rear bumper";
            }
            else if (inderxPhotoInspektion == 31)
            {
                nameLayout = "The left side of the rear bumper";
            }
            else if (inderxPhotoInspektion == 32)
            {
                nameLayout = "Rear of the Pickup on the driver's side";
            }
            else if (inderxPhotoInspektion == 33)
            {
                nameLayout = "The rear wheel of a pickup driver's side";
            }
            else if (inderxPhotoInspektion == 34)
            {
                nameLayout = "Rear of the Pickup on the driver's side";
            }
            else if (inderxPhotoInspektion == 35)
            {
                nameLayout = "Rear driver's door";
            }
            return nameLayout;
        }

        public async Task OrintableScreen(int inderxPhotoInspektion)
        {
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
            //if (inderxPhotoInspektion == 2 || inderxPhotoInspektion == 3 || inderxPhotoInspektion == 6 || inderxPhotoInspektion == 7 || inderxPhotoInspektion == 8 || inderxPhotoInspektion == 11 || inderxPhotoInspektion == 12 || inderxPhotoInspektion == 13 || inderxPhotoInspektion == 16
            //    || inderxPhotoInspektion == 17 || inderxPhotoInspektion == 18 || inderxPhotoInspektion == 20 || inderxPhotoInspektion == 21 || inderxPhotoInspektion == 22 || inderxPhotoInspektion == 23 || inderxPhotoInspektion == 24 || inderxPhotoInspektion == 28
            //    || inderxPhotoInspektion == 29 || inderxPhotoInspektion == 30 || inderxPhotoInspektion == 31 || inderxPhotoInspektion == 34 || inderxPhotoInspektion == 35)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForcePortrait();
            //}
            //else if (inderxPhotoInspektion == 1 || inderxPhotoInspektion == 4 || inderxPhotoInspektion == 5 || inderxPhotoInspektion == 6 || inderxPhotoInspektion == 9 || inderxPhotoInspektion == 10 || inderxPhotoInspektion == 14 || inderxPhotoInspektion == 15
            //    || inderxPhotoInspektion == 19 || inderxPhotoInspektion == 25 || inderxPhotoInspektion == 26 || inderxPhotoInspektion == 27 || inderxPhotoInspektion == 32 || inderxPhotoInspektion == 33)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForceLandscape();
            //}
        }

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
                        indecCar = 7;
                        break;
                    }
                case 3:
                    {
                        indecCar = 17;
                        break;
                    }
                case 4:
                    {
                        indecCar = 11;
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
                        indecCar = 24;
                        break;
                    }
                case 8:
                    {
                        indecCar = 22;
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
                        indecCar = 7;
                        break;
                    }
                case 2:
                    {
                        indecCar = 32;
                        break;
                    }
                case 3:
                    {
                        indecCar = 9;
                        break;
                    }
                case 4:
                    {
                        indecCar = 10;
                        break;
                    }
                case 5:
                    {
                        indecCar = 24;
                        break;
                    }
                case 6:
                    {
                        indecCar = 28;
                        break;
                    }
                case 7:
                    {
                        indecCar = 29;
                        break;
                    }
                case 8:
                    {
                        indecCar = 30;
                        break;
                    }
                case 9:
                    {
                        indecCar = 12;
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
                        indecCar = 22;
                        break;
                    }
                case 13:
                    {
                        indecCar = 2;
                        break;
                    }
                case 14:
                    {
                        indecCar = 3;
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
    }
}