using MDispatch.Models;
using MDispatch.NewElement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickUp.CameraPageFolder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraSeatBelts : CameraPage
    {
        private Ask1Page ask1Page = null;
        private List<Photo> photos = new List<Photo>();
        List<byte[]> imagesByte = new List<byte[]>();

        public CameraSeatBelts (Ask1Page ask1Page)
		{
            this.ask1Page = ask1Page;
			InitializeComponent ();
            titlePhoto.Text = $"safety belt {photos.Count + 1}";
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            if(photos.Count >= 3)
            {
                await Navigation.PopAsync(true);
                Photo photo1 = new Photo();
                photo1.Base64 = Convert.ToBase64String(result.Result);
                photo1.path = $"../Photo/{ask1Page.ask1PageMV.VehiclwInformation.Id}/PikedUp/CameraSeatBelts/{photos.Count + 1}.jpg";
                photos.Add(photo1);
                imagesByte.Add(result.Result);
                ask1Page.AddPhotoSeatBelts(photos, imagesByte);
                return;
            }
            Photo photo = new Photo();
            photo.Base64 = Convert.ToBase64String(result.Result);
            photo.path = $"../Photo/{ask1Page.ask1PageMV.VehiclwInformation.Id}/PikedUp/CameraSeatBelts/{photos.Count + 1}.jpg";
            photos.Add(photo);
            imagesByte.Add(result.Result);
            titlePhoto.Text = $"safety belt {photos.Count + 1}";
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}