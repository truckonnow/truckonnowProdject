using MDispatch.NewElement;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public class CarCoupe : IVehicle
    {
        public string TypeIndex { get; set; } = "Coupe";
        public int CountCarImg { get; set; } = 39;

        public int GetIndexCar(int countPhoto)
        {
            int indecCar = 0;
            switch(countPhoto)
            {
                case 1:
                    {
                        indecCar = -1;
                        break;
                    }
                case 2:
                    {
                        indecCar = 7;
                        break;
                    }
                case 3:
                    {
                        indecCar = 8;
                        break;
                    }
                case 4:
                    {
                        indecCar = -2;
                        break;
                    }
                case 5:
                    {
                        indecCar = 21;
                        break;
                    }
                case 6:
                    {
                        indecCar = 15;
                        break;
                    }
                case 7:
                    {
                        indecCar = -3;
                        break;
                    }
                case 8:
                    {
                        indecCar = 27;
                        break;
                    }
                case 9:
                    {
                        indecCar = 28;
                        break;
                    }
                case 10:
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
            if(inderxPhotoInspektion == 1)
            {
                nameLayout = "Vehicle dashboard";
            }
            else if(inderxPhotoInspektion == 2)
            {
                nameLayout = "Driver seat";
            }
            else if (inderxPhotoInspektion == 3)
            {
                nameLayout = "Driver side kit";
            }
            else if (inderxPhotoInspektion == 4)
            {
                nameLayout = "Driver's door";
            }
            else if (inderxPhotoInspektion == 5)
            {
                nameLayout = "Driver side kit";
            }
            else if (inderxPhotoInspektion == 6)
            {
                nameLayout = "Coupe";
            }
            else if (inderxPhotoInspektion == 7)
            {
                nameLayout = "Driver's door";
            }
            else if (inderxPhotoInspektion == 8)
            {
                nameLayout = "Rearview Mirror Driver's Side";
            }
            else if (inderxPhotoInspektion == 9)
            {
                nameLayout = "Rearview Mirror Driver's Sidee";
            }
            else if (inderxPhotoInspektion == 10)
            {
                nameLayout = "Front of the compartment, driver's side";
            }
            else if (inderxPhotoInspektion == 11)
            {
                nameLayout = "Front front wheel driver's side";
            }
            else if (inderxPhotoInspektion == 12)
            {
                nameLayout = "Front bumper, driver's side";
            }
            else if (inderxPhotoInspektion == 13)
            {
                nameLayout = "Front headlight, driver's side";
            }
            else if (inderxPhotoInspektion == 14)
            {
                nameLayout = "Front headlight, driver's side";
            }
            else if (inderxPhotoInspektion == 15)
            {
                nameLayout = "Front bumper";
            }
            else if (inderxPhotoInspektion == 16)
            {
                nameLayout = "The entire front of the coupe";
            }
            else if (inderxPhotoInspektion == 17)
            {
                nameLayout = "Right side of the front bumper";
            }
            else if (inderxPhotoInspektion == 18)
            {
                nameLayout = "Central side of the front bumper";
            }
            else if (inderxPhotoInspektion == 19)
            {
                nameLayout = "Left side of the front bumper";
            }
            else if (inderxPhotoInspektion == 20)
            {
                nameLayout = "Сoupe hood";
            }
            else if (inderxPhotoInspektion == 21)
            {
                nameLayout = "Windshield Coupe";
            }
            else if (inderxPhotoInspektion == 22)
            {
                nameLayout = "Front Headlight on the passenger side";
            }
            else if (inderxPhotoInspektion == 23)
            {
                nameLayout = "Front Headlight on the passenger side";
            }
            else if (inderxPhotoInspektion == 24)
            {
                nameLayout = "Front bumper, passenger side";
            }
            else if (inderxPhotoInspektion == 25)
            {
                nameLayout = "Front front wheel passenger side";
            }
            else if (inderxPhotoInspektion == 26)
            {
                nameLayout = "Front of the compartment, passenger side";
            }
            else if (inderxPhotoInspektion == 27)
            {
                nameLayout = "Passenger door";
            }
            else if (inderxPhotoInspektion == 28)
            {
                nameLayout = "Rearview Mirror Driver's Side";
            }
            else if (inderxPhotoInspektion == 29)
            {
                nameLayout = "Right rear view mirror (Front part)";
            }
            else if (inderxPhotoInspektion == 30)
            {
                nameLayout = "Rear of passenger compartment";
            }
            else if (inderxPhotoInspektion == 31)
            {
                nameLayout = "Rear front wheel passenger side";
            }
            else if (inderxPhotoInspektion == 32)
            {
                nameLayout = "The entire passenger side of the compartment";
            }
            else if (inderxPhotoInspektion == 33)
            {
                nameLayout = "Rear Headlight, driver's side";
            }
            else if (inderxPhotoInspektion == 34)
            {
                nameLayout = "Rear front wheel passenger side";
            }
            else if (inderxPhotoInspektion == 35)
            {
                nameLayout = "The entire rear of the coupe";
            }
            else if (inderxPhotoInspektion == 36)
            {
                nameLayout = "Rear Headlight, passenger side";
            }
            else if (inderxPhotoInspektion == 37)
            {
                nameLayout = "Rear window coupe";
            }
            else if (inderxPhotoInspektion == 38)
            {
                nameLayout = "Rear of passenger compartment";
            }
            else if (inderxPhotoInspektion == 39)
            {
                nameLayout = "The whole driver’s side of the coupe";
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