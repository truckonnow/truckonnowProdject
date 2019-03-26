using MDispatch.ViewModels.AskPhoto;

namespace MDispatch.ViewModels.InspectionMV.Models
{
    public class CarPickUp : ICar
    {
        public string typeIndex { get; set; } = "PickUp";
        public int CountCarImg { get; set; }

        public string GetNameLayout(int inderxPhotoInspektion)
        {
            throw new System.NotImplementedException();
        }

        public async void OrintableScreen(int inderxPhotoInspektion)
        {
            throw new System.NotImplementedException();
        }

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
                        indecCar = 26;
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
                        indecCar = 33;
                        break;
                    }
                case 9:
                    {
                        indecCar = 20;
                        break;
                    }
                case 10:
                    {
                        indecCar = 21;
                        break;
                    }
                case 11:
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
    }
}