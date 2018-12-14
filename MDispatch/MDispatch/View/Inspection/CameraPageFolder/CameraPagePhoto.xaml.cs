using MDispatch.NewElement;
using MDispatch.ViewModels.PageAppMV;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            fullPagePhotoMV.AddNewFotoSourse(result.Image);
            fullPagePhotoMV.SetPhoto(result.Image);
        }
    }
}