using MDispatch.NewElement;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using MDispatch.ViewModels.PageAppMV;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraPagePhoto1 : CameraPage
    {
        private FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV = null;
        private string pngPaternPhoto = null;
        private FullPagePhotoDelyvery fullPagePhotoDelyvery = null;

        public CameraPagePhoto1(FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV, string pngPaternPhoto, FullPagePhotoDelyvery fullPagePhotoDelyvery)
		{
            this.fullPagePhotoDelyvery = fullPagePhotoDelyvery;
            this.pngPaternPhoto = pngPaternPhoto;
            this.fullPagePhotoDelyveryMV = fullPagePhotoDelyveryMV;
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            paternPhoto.Source = pngPaternPhoto;
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            await Navigation.PopAsync(true);
            if (!result.Success)
                return;
            fullPagePhotoDelyveryMV.AddNewFotoSourse(result.Image);
            fullPagePhotoDelyveryMV.SetPhoto(result.Image);
            fullPagePhotoDelyvery.SetbtnVisable();
        }

        private void StackLayout_SizeChanged(object sender, System.EventArgs e)
        {
            double onePercentheigth = Application.Current.MainPage.Height / 100;
            double onePercentwidth = Application.Current.MainPage.Width / 100;
            paternPhoto.HeightRequest = onePercentheigth * 100;
            paternPhoto.WidthRequest = onePercentwidth * 100;
        }
    }
}