using MDispatch.NewElement;
using MDispatch.ViewModels.PageAppMV;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace MDispatch.View.PageApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraPagePhoto : CameraPage
    {
        FullPagePhotoMV fullPagePhotoMV = null;

        public CameraPagePhoto(FullPagePhotoMV fullPagePhotoMV)
		{
            this.fullPagePhotoMV = fullPagePhotoMV;
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            await Navigation.PopAsync(true);
            if (!result.Success)
                return;
            fullPagePhotoMV.SetPhoto(result.Image);
            fullPagePhotoMV.SourseImage = ImageSource.FromStream(() => new MemoryStream(result.Image));
        }

        public void GetImageFromByteArray(byte[] byteArray)
        {
            string imgName = "my_mage_name.bmp";

            byte[] imgByteArray = Convert.FromBase64String("your_base64_string");
        }
    }
}