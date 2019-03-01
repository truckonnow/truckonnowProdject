using MDispatch.Models;
using MDispatch.NewElement;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp.CameraPageFolder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraPthotoInTrack : CameraPage
    {
        private Ask1Page ask1Page = null;
        private List<Photo> photos = new List<Photo>();
        private List<byte[]> imagesByte = new List<byte[]>();
        private int indexPhoto = 29;
        private string nameVech = null;

        public CameraPthotoInTrack (Ask1Page ask1Page, string nameVech)
		{
			InitializeComponent ();
            this.ask1Page = ask1Page;
            this.nameVech = nameVech;
            NavigationPage.SetHasNavigationBar(this, false);
            titlePhoto.Text = indexPhoto.ToString();
            paternPhoto.Source = $"{nameVech}{indexPhoto}.png";
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;

            SetTitle();
            if (indexPhoto == 28)
            {
                await Navigation.PopAsync(true);
                Photo photo1 = new Photo();
                photo1.Base64 = JsonConvert.SerializeObject(result.Image);
                photo1.path = $"../Photo/{ask1Page.ask1PageMV.VehiclwInformation.Id}/PikedUp/CameraTrack/{photos.Count + 1}.Jpeg";
                photos.Add(photo1);
                imagesByte.Add(result.Image);
                ask1Page.AddPhotoInTrack(photos, imagesByte);
                return;
            }
            else if(indexPhoto == 31)
            {
                indexPhoto = 24;
                paternPhoto.Source = $"{nameVech}{indexPhoto}.png";
                titlePhoto.Text = $"Left rear view mirror (Front part) - {indexPhoto.ToString()}";
            }
            else if(indexPhoto == 25)
            {
                indexPhoto = 28;
                paternPhoto.Source = $"{nameVech}{indexPhoto}.png";
                titlePhoto.Text = $"Vehicle hood - {indexPhoto.ToString()}";
            }
            else
            {
                indexPhoto++;
                paternPhoto.Source = $"{nameVech}{indexPhoto}.png";
                titlePhoto.Text = $"Vehicle hood - {indexPhoto.ToString()}";
            }

            Photo photo = new Photo();
            photo.Base64 = JsonConvert.SerializeObject(result.Image);
            photo.path = $"../Photo/{ask1Page.ask1PageMV.VehiclwInformation.Id}/PikedUp/CameraTrack/{photos.Count + 1}.Jpeg";
            photos.Add(photo);
            imagesByte.Add(result.Image);
        }


        private void SetTitle()
        {
            if (indexPhoto == 29)
            {

            }
        }
    }
}