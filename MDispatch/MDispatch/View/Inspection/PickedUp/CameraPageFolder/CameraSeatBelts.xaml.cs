using MDispatch.Models;
using MDispatch.NewElement;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickUp.CameraPageFolder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraSeatBelts : CameraPage
    {
        private Ask1Page ask1Page = null;
        private List<Photo> photos = new List<Photo>();

        public CameraSeatBelts (Ask1Page ask1Page)
		{
            this.ask1Page = ask1Page;
			InitializeComponent ();
            titlePhoto.Text = $"{photos.Count+1}";
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            if(photos.Count >= 3)
            {
                await Navigation.PopAsync(true);
                Photo photo1 = new Photo();
                photo1.Base64 = JsonConvert.SerializeObject(result.Image);
                photo1.path = $"Photo/{ask1Page.ask1PageMV.VehiclwInformation.Id}/Ask/CameraSeatBelts/{photos.Count + 1}";
                photos.Add(photo1);
                ask1Page.AddPhotoSeatBelts(photos);
                return;
            }
            Photo photo = new Photo();
            photo.Base64 = JsonConvert.SerializeObject(result.Image);
            photo.path = $"Photo/{ask1Page.ask1PageMV.VehiclwInformation.Id}/Ask/CameraSeatBelts/{photos.Count + 1}";
            photos.Add(photo);
            titlePhoto.Text = $"{photos.Count + 1}";
        }
    }
}