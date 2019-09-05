using MDispatch.NewElement;
using MDispatch.ViewModels.AskPhoto;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Models
{
    public class CarSuv : ICar
    {
        public string typeIndex { get; set; } = "Suv"; 
        public int CountCarImg { get; set; } = 36;

        public int GetIndexCar(int countPhoto)
        {
            int indecCar = 0;
            switch (countPhoto)
            {
                case 1:
                    {
                        indecCar = 27;
                        break;
                    }
                case 2:
                    {
                        indecCar = 1;
                        break;
                    }
                case 3:
                    {
                        indecCar = 4;
                        break;
                    }
                case 4:
                    {
                        indecCar = 2;
                        break;
                    }
                case 5:
                    {
                        indecCar = 3;
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
                        indecCar = 5;
                        break;
                    }
                case 2:
                    {
                        indecCar = 23;
                        break;
                    }
                case 3:
                    {
                        indecCar = 6;
                        break;
                    }
                case 4:
                    {
                        indecCar = 28;
                        break;
                    }
                case 5:
                    {
                        indecCar = 12;
                        break;
                    }
                case 6:
                    {
                        indecCar = 13;
                        break;
                    }
                case 7:
                    {
                        indecCar = 14;
                        break;
                    }
                case 8:
                    {
                        indecCar = 15;
                        break;
                    }
                case 9:
                    {
                        indecCar = 33;
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
                nameLayout = "Vehicle hood";
            }
            else if (inderxPhotoInspektion == 2)
            {
                nameLayout = "Left headlight of the vehicle";
            }
            else if (inderxPhotoInspektion == 3)
            {
                nameLayout = "Right headlight of the vehicle";
            }
            else if (inderxPhotoInspektion == 4)
            {
                nameLayout = "Vehicle windshield";
            }
            else if (inderxPhotoInspektion == 5)
            {
                nameLayout = "All right part of the vehicle";
            }
            else if (inderxPhotoInspektion == 6)
            {
                nameLayout = "All left part of the vehicle";
            }
            else if (inderxPhotoInspektion == 7)
            {
                nameLayout = "Front left side of the vehicle";
            }
            else if (inderxPhotoInspektion == 8)
            {
                nameLayout = "Front left side of the vehicle";
            }
            else if (inderxPhotoInspektion == 9)
            {
                nameLayout = "Front left door of the vehicle";
            }
            else if (inderxPhotoInspektion == 10)
            {
                nameLayout = "Rear left door of the vehicle";
            }
            else if (inderxPhotoInspektion == 11)
            {
                nameLayout = "Rear left side of the vehicle";
            }
            else if (inderxPhotoInspektion == 12)
            {
                nameLayout = "Suv";
            }
            else if (inderxPhotoInspektion == 13)
            {
                nameLayout = "Suv";
            }
            else if (inderxPhotoInspektion == 14)
            {
                nameLayout = "Suv";
            }
            else if (inderxPhotoInspektion == 15)
            {
                nameLayout = "Rear right side of the vehicle";
            }
            else if (inderxPhotoInspektion == 16)
            {
                nameLayout = "Rear right door of the vehicle";
            }
            else if (inderxPhotoInspektion == 17)
            {
                nameLayout = "Front right door of the vehicle";
            }
            else if (inderxPhotoInspektion == 18)
            {
                nameLayout = "Front right side of the vehicle";
            }
            else if (inderxPhotoInspektion == 19)
            {
                nameLayout = "Front right side of the vehicle";
            }
            else if (inderxPhotoInspektion == 20)
            {
                nameLayout = "The left side of the front bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 21)
            {
                nameLayout = "The center of the front bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 22)
            {
                nameLayout = "The right side of the front bumper of the vehicle";
            }
            else if (inderxPhotoInspektion == 23)
            {
                nameLayout = "All front part of the vehicle (bumper, headlights and windshield)";
            }
            else if (inderxPhotoInspektion == 24)
            {
                nameLayout = "Left rearview mirror of the vehicle (rear)";
            }
            else if (inderxPhotoInspektion == 25)
            {
                nameLayout = "Rith rearview mirror of the vehicle (rear)";
            }
            else if (inderxPhotoInspektion == 26)
            {
                nameLayout = "Right rearview mirror of the vehicle (Bottom)";
            }
            else if (inderxPhotoInspektion == 27)
            {
                nameLayout = "Left rearview mirror of the vehicle (Bottom)";
            }
            else if (inderxPhotoInspektion == 28)
            {
                nameLayout = "Vehicle rear window";
            }
            else if (inderxPhotoInspektion == 29)
            {
                nameLayout = "Right rear wheel of the vehicle";
            }
            else if (inderxPhotoInspektion == 30)
            {
                nameLayout = "Right front wheel of the vehicle";
            }
            else if (inderxPhotoInspektion == 31)
            {
                nameLayout = "Left front wheel of the vehicle";
            }
            else if (inderxPhotoInspektion == 32)
            {
                nameLayout = "Left rear wheel of the vehicle";
            }
            else if (inderxPhotoInspektion == 33)
            {
                nameLayout = "Vehicle interior";
            }
            else if (inderxPhotoInspektion == 34)
            {
                nameLayout = "Vehicle interior";
            }
            else if (inderxPhotoInspektion == 35)
            {
                nameLayout = "Front door from the inside by the driver of the vehicle";
            }
            else if (inderxPhotoInspektion == 36)
            {
                nameLayout = "Vehicle dashboard";
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