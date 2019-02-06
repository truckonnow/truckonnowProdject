using MDispatch.NewElement;
using MDispatch.View.Inspection.PickedUp;
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
        private FullPagePhoto fullPagePhoto = null;
        private PageAddDamage pageAddDamage = null;

        public CameraPagePhoto(FullPagePhotoMV fullPagePhotoMV, string pngPaternPhoto, FullPagePhoto fullPagePhoto, PageAddDamage pageAddDamage = null)
		{
            this.pageAddDamage = pageAddDamage;
            this.fullPagePhoto = fullPagePhoto;
            this.fullPagePhotoMV = fullPagePhotoMV;
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            if (pngPaternPhoto != null)
            {
                paternPhoto.Source = pngPaternPhoto;
            }
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            await Navigation.PopAsync(true);
            if (!result.Success)
                return;
            fullPagePhotoMV.AddNewFotoSourse(result.Image);
            fullPagePhotoMV.SetPhoto(result.Image);
            fullPagePhoto.SetbtnVisable();
            if(pageAddDamage != null)
            {
                pageAddDamage.stateSelect = 0;
            }
        }
    }
}