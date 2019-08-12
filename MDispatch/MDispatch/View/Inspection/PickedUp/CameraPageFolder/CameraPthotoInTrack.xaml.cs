using MDispatch.Models;
using MDispatch.NewElement;
using MDispatch.ViewModels.AskPhoto;
using MDispatch.ViewModels.InspectionMV.Models;
using MDispatch.ViewModels.InspectionMV.Servise.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp.CameraPageFolder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraPthotoInTrack : CameraPage
    {
        private Ask1Page ask1Page = null;
        private List<Photo> photos = new List<Photo>();
        private List<byte[]> imagesByte = new List<byte[]>();
        private int countPhoto = 1;
        private string nameVech = null;
        private ICar car = null;

        public CameraPthotoInTrack (Ask1Page ask1Page, string nameVech)
		{
            car = GetTypeCar(nameVech.Replace(" ", ""));
            InitializeComponent ();
            this.ask1Page = ask1Page;
            this.nameVech = nameVech;
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
            car.OrintableScreen(car.GetIndexCar(countPhoto));
            paternPhoto.Source = $"{car.typeIndex.Replace(" ", "")}{car.GetIndexCar(countPhoto)}.png";
            titlePhoto.Text = car.GetNameLayout(car.GetIndexCar(countPhoto));
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            Photo photo1 = new Photo();
            photo1.Base64 = JsonConvert.SerializeObject(result.Result);
            photo1.path = $"../Photo/{ask1Page.ask1PageMV.VehiclwInformation.Id}/PikedUp/CameraTrack/{photos.Count + 1}.jpg";
            photos.Add(photo1);
            imagesByte.Add(result.Result);
            countPhoto++;
            if (car.GetIndexCar(countPhoto) == 0)
            {
                await Navigation.PopAsync(true);
                ask1Page.AddPhotoInTrack(photos, imagesByte);
                DependencyService.Get<IOrientationHandler>().ForceSensor();
                return;
            }
            else
            {
                car.OrintableScreen(car.GetIndexCar(countPhoto));
                paternPhoto.Source = $"{car.typeIndex.Replace(" ", "")}{car.GetIndexCar(countPhoto)}.png";
                titlePhoto.Text = car.GetNameLayout(car.GetIndexCar(countPhoto));
            }
        }

        private ICar GetTypeCar(string typeCar)
        {
            ICar car = null;
            switch (typeCar)
            {
                case "PickUp":
                    {
                        car = new CarPickUp();
                        break;
                    }
                case "Coupe":
                    {
                        car = new CarCoupe();
                        break;
                    }
                case "Suv":
                    {
                        car = new CarSuv();
                        break;
                    }
                case "Sedan":
                    {
                        car = new CarSedan();
                        break;
                    }
            }
            return car;
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}