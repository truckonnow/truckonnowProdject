using MDispatch.NewElement;
using MDispatch.ViewModels.PageAppMV;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraPagePhoto : CameraPage
    {
        private FullPagePhotoMV fullPagePhotoMV = null;
        private string pngPaternPhoto = null;

        public CameraPagePhoto(FullPagePhotoMV fullPagePhotoMV, string pngPaternPhoto)
		{
            this.pngPaternPhoto = pngPaternPhoto;
            this.fullPagePhotoMV = fullPagePhotoMV;
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            paternPhoto.Source = pngPaternPhoto;
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            await Navigation.PopAsync(true);
            if (!result.Success)
                return;
            fullPagePhotoMV.AddNewFotoSourse(result.Image);
            fullPagePhotoMV.SetPhoto(result.Image);
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