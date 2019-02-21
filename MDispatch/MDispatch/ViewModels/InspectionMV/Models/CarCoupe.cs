using MDispatch.NewElement;
using MDispatch.ViewModels.AskPhoto;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Models
{
    public class CarCoupe : ICar
    {
        public string typeIndex { get; set; } = "Coupe";

        public void OrintableScreen(int inderxPhotoInspektion)
        {
            if (inderxPhotoInspektion == 12 || inderxPhotoInspektion == 13 || inderxPhotoInspektion == 14 || inderxPhotoInspektion == 15)
            {
                DependencyService.Get<IOrientationHandler>().ForceSensor();
            }
            else if (inderxPhotoInspektion == 1 || inderxPhotoInspektion == 4 || inderxPhotoInspektion == 8 || inderxPhotoInspektion == 19 || inderxPhotoInspektion == 21
                || inderxPhotoInspektion == 22 || inderxPhotoInspektion == 28 || inderxPhotoInspektion == 29 || inderxPhotoInspektion == 29 || inderxPhotoInspektion == 30
                 || inderxPhotoInspektion == 31 || inderxPhotoInspektion == 32 || inderxPhotoInspektion == 33 || inderxPhotoInspektion == 34 || inderxPhotoInspektion == 35
                  || inderxPhotoInspektion == 36 || inderxPhotoInspektion == 37 || inderxPhotoInspektion == 39)
            {
                DependencyService.Get<IOrientationHandler>().ForcePortrait();
            }
            else if (inderxPhotoInspektion == 2 || inderxPhotoInspektion == 3 || inderxPhotoInspektion == 5 || inderxPhotoInspektion == 6 || inderxPhotoInspektion == 7 || inderxPhotoInspektion == 9
                 || inderxPhotoInspektion == 10 || inderxPhotoInspektion == 11 || inderxPhotoInspektion == 16 || inderxPhotoInspektion == 17 || inderxPhotoInspektion == 18 || inderxPhotoInspektion == 19
                  || inderxPhotoInspektion == 23 || inderxPhotoInspektion == 24 || inderxPhotoInspektion == 25 || inderxPhotoInspektion == 26 || inderxPhotoInspektion == 27 || inderxPhotoInspektion == 36
                   || inderxPhotoInspektion == 39)
            {
                DependencyService.Get<IOrientationHandler>().ForceLandscape();
            }
        }
    }
}
