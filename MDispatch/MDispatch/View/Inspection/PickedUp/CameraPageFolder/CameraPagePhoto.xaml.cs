using MDispatch.NewElement;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
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
        private FullPagePhoto fullPagePhoto = null;
            
        public CameraPagePhoto(FullPagePhotoMV fullPagePhotoMV, string pngPaternPhoto, FullPagePhoto fullPagePhoto)
		{
            this.fullPagePhoto = fullPagePhoto;
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
            fullPagePhoto.SetbtnVisable();
        }
    }
}