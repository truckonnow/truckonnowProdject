using MDispatch.NewElement;
using MDispatch.ViewModels.AskPhoto;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Models
{
    public class CarCoupe : ICar
    {
        public string typeIndex { get; set; } = "Coupe";
        public int CountCarImg { get; set; } = 39;

        public int GetIndexCar(int countPhoto)
        {
            int indecCar = 0;
            switch(countPhoto)
            {
                case 1:
                    {
                        indecCar = 29;
                        break;
                    }
                case 2:
                    {
                        indecCar = 30;
                        break;
                    }
                case 3:
                    {
                        indecCar = 31;
                        break;
                    }
                case 4:
                    {
                        indecCar = 24;
                        break;
                    }
                case 5:
                    {
                        indecCar = 25;
                        break;
                    }
                case 6:
                    {
                        indecCar = 28;
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
                        indecCar = 19;
                        break;
                    }
                case 3:
                    {
                        indecCar = 2;
                        break;
                    }
                case 4:
                    {
                        indecCar = 16;
                        break;
                    }
                case 5:
                    {
                        indecCar = 23;
                        break;
                    }
                case 6:
                    {
                        indecCar = 20;
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
            if(inderxPhotoInspektion == 1)
            {
                nameLayout = "Coupe";
            }
            else if(inderxPhotoInspektion == 2)
            {
                nameLayout = "Entire left side of the vehicle";
            }
            else if (inderxPhotoInspektion == 3)
            {
                nameLayout = "Left front of the vehicle";
            }
            else if (inderxPhotoInspektion == 4)
            {
                nameLayout = "Bumper on the left side";
            }
            else if (inderxPhotoInspektion == 5)
            {
                nameLayout = "Left center of the vehicle";
            }
            else if (inderxPhotoInspektion == 6)
            {
                nameLayout = "Left rear of the vehicle";
            }
            else if (inderxPhotoInspektion == 7)
            {
                nameLayout = "Entire right side of the vehicle";
            }
            else if (inderxPhotoInspektion == 8)
            {
                nameLayout = "Bumper on the right side";
            }
            else if (inderxPhotoInspektion == 9)
            {
                nameLayout = "Right front of the vehicle";
            }
            else if (inderxPhotoInspektion == 10)
            {
                nameLayout = "Right center of the vehicle";
            }
            else if (inderxPhotoInspektion == 11)
            {
                nameLayout = "Right rear of the vehicle";
            }
            else if (inderxPhotoInspektion == 12)
            {
                nameLayout = "Rear left wheel";
            }
            else if (inderxPhotoInspektion == 13)
            {
                nameLayout = "Rear right wheel";
            }
            else if (inderxPhotoInspektion == 14)
            {
                nameLayout = "Front left wheel";
            }
            else if (inderxPhotoInspektion == 15)
            {
                nameLayout = "Front right wheel";
            }
            else if (inderxPhotoInspektion == 16)
            {
                nameLayout = "Vehicle rear (rear bumper with headlights)";
            }
            else if (inderxPhotoInspektion == 17)
            {
                nameLayout = "Left rear vehicle headlight";
            }
            else if (inderxPhotoInspektion == 18)
            {
                nameLayout = "Right rear vehicle headlight";
            }
            else if (inderxPhotoInspektion == 19)
            {
                nameLayout = "Front of the vehicle (front bumper with headlights, hood and windshield)";
            }
            else if (inderxPhotoInspektion == 20)
            {
                nameLayout = "Vehicle front bumper";
            }
            else if (inderxPhotoInspektion == 21)
            {
                nameLayout = "Front left vehicle headlight";
            }
            else if (inderxPhotoInspektion == 22)
            {
                nameLayout = "Front right vehicle headlight";
            }
            else if (inderxPhotoInspektion == 23)
            {
                nameLayout = "Coupe";
            }
            else if (inderxPhotoInspektion == 24)
            {
                nameLayout = "Front left vehicle headlight";
            }
            else if (inderxPhotoInspektion == 25)
            {
                nameLayout = "Front right vehicle headlight";
            }
            else if (inderxPhotoInspektion == 26)
            {
                nameLayout = "Left rear view mirror (Rear part)";
            }
            else if (inderxPhotoInspektion == 27)
            {
                nameLayout = "Right rear view mirror (Rear part)";
            }
            else if (inderxPhotoInspektion == 28)
            {
                nameLayout = "Left rear view mirror (Front part)";
            }
            else if (inderxPhotoInspektion == 29)
            {
                nameLayout = "Right rear view mirror (Front part)";
            }
            else if (inderxPhotoInspektion == 30)
            {
                nameLayout = "Vehicle hood";
            }
            else if (inderxPhotoInspektion == 31)
            {
                nameLayout = "Windshield with vehicle top";
            }
            else if (inderxPhotoInspektion == 32)
            {
                nameLayout = "Coupe";
            }
            else if (inderxPhotoInspektion == 33)
            {
                nameLayout = "Coupe";
            }
            else if (inderxPhotoInspektion == 34)
            {
                nameLayout = "Coupe";
            }
            else if (inderxPhotoInspektion == 35)
            {
                nameLayout = "Rear upper vehicle";
            }
            else if (inderxPhotoInspektion == 36)
            {
                nameLayout = "Dashboard with steering wheel";
            }
            else if (inderxPhotoInspektion == 37)
            {
                nameLayout = "Coupe";
            }
            else if (inderxPhotoInspektion == 38)
            {
                nameLayout = "Coupe";
            }
            else if (inderxPhotoInspektion == 39)
            {
                nameLayout = "Coupe";
            }
            return nameLayout;
        }

        public async Task OrintableScreen(int inderxPhotoInspektion)
        {
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
            //if (inderxPhotoInspektion == 12 || inderxPhotoInspektion == 13 || inderxPhotoInspektion == 14 || inderxPhotoInspektion == 15)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForceSensor();
            //}
            //else if (inderxPhotoInspektion == 1 || inderxPhotoInspektion == 4 || inderxPhotoInspektion == 8 || inderxPhotoInspektion == 19 || inderxPhotoInspektion == 21
            //    || inderxPhotoInspektion == 22 || inderxPhotoInspektion == 28 || inderxPhotoInspektion == 29 || inderxPhotoInspektion == 29 || inderxPhotoInspektion == 30
            //     || inderxPhotoInspektion == 31 || inderxPhotoInspektion == 32 || inderxPhotoInspektion == 33 || inderxPhotoInspektion == 34 || inderxPhotoInspektion == 35
            //      || inderxPhotoInspektion == 37 || inderxPhotoInspektion == 39)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForcePortrait();
            //}
            //else if (inderxPhotoInspektion == 2 || inderxPhotoInspektion == 3 || inderxPhotoInspektion == 5 || inderxPhotoInspektion == 6 || inderxPhotoInspektion == 7 || inderxPhotoInspektion == 9
            //     || inderxPhotoInspektion == 10 || inderxPhotoInspektion == 11 || inderxPhotoInspektion == 16 || inderxPhotoInspektion == 17 || inderxPhotoInspektion == 18 || inderxPhotoInspektion == 20
            //      || inderxPhotoInspektion == 23 || inderxPhotoInspektion == 24 || inderxPhotoInspektion == 25 || inderxPhotoInspektion == 26 || inderxPhotoInspektion == 27 || inderxPhotoInspektion == 36
            //       || inderxPhotoInspektion == 38)
            //{
            //    DependencyService.Get<IOrientationHandler>().ForceLandscape();
            //}
        }
    }
}