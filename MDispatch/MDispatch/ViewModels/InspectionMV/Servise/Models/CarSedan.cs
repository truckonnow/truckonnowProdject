using MDispatch.NewElement;
using MDispatch.ViewModels.AskPhoto;
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
                nameLayout = "Sedan";
            }
            return nameLayout;
        }

        public void OrintableScreen(int inderxPhotoInspektion)
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
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