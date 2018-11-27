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
            //string strm = "R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7";
            await Navigation.PopAsync(true);
            if (!result.Success)
                return;
            //using (var imageFile = new FileStream(filepath, FileMode.Create))
            //{
            //    imageFile.Write(bytess, 0, bytess.Length);
            //    imageFile.Flush();
            //}

            fullPagePhotoMV.SourseImage = ImageSource.FromStream(() => new MemoryStream(result.Image));
        }

        public void GetImageFromByteArray(byte[] byteArray)
        {
            string imgName = "my_mage_name.bmp";

            byte[] imgByteArray = Convert.FromBase64String("your_base64_string");
        }
    }
}