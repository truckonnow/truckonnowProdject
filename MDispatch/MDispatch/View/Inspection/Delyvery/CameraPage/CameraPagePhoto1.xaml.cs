using MDispatch.NewElement;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using MDispatch.ViewModels.PageAppMV;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraPagePhoto1 : CameraPage
    {
        private string pngPaternPhoto = null;
        private FullPagePhotoDelyvery fullPagePhotoDelyvery = null;
        private PageAddDamage1 pageAddDamage1 = null;

        public CameraPagePhoto1(string pngPaternPhoto, FullPagePhotoDelyvery fullPagePhotoDelyvery, string typeCamera = null, PageAddDamage1 pageAddDamage1 = null)
        {
            this.TypeCamera = typeCamera;
            this.pageAddDamage1 = pageAddDamage1;
            this.fullPagePhotoDelyvery = fullPagePhotoDelyvery;
            this.pngPaternPhoto = pngPaternPhoto;
            InitializeComponent ();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
            paternPhoto.Source = pngPaternPhoto;
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            fullPagePhotoDelyvery.fullPagePhotoDelyveryMV.AddNewFotoSourse(result.Result);
            fullPagePhotoDelyvery.fullPagePhotoDelyveryMV.SetPhoto(result.Result);
            fullPagePhotoDelyvery.SetbtnVisable();
            if (pageAddDamage1 != null)
            {
                pageAddDamage1.stateSelect = 0;
            }
            await Navigation.PopAsync(true);
        }


        protected override void OnDisappearing()
        {
            if (pageAddDamage1 != null)
            {
                pageAddDamage1.stateSelect = 0;
            }
            base.OnDisappearing();
        }

        private void StackLayout_SizeChanged(object sender, System.EventArgs e)
        {
            double onePercentheigth = Xamarin.Forms.Application.Current.MainPage.Height / 100;
            double onePercentwidth = Xamarin.Forms.Application.Current.MainPage.Width / 100;
            paternPhoto.HeightRequest = onePercentheigth * 100;
            paternPhoto.WidthRequest = onePercentwidth * 100;
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void CameraPage_OnPhotoinspectionResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            fullPagePhotoDelyvery.fullPagePhotoDelyveryMV.AddNewFotoSourse(result.Result);
            fullPagePhotoDelyvery.fullPagePhotoDelyveryMV.SetPhoto(result.Result);
            fullPagePhotoDelyvery.SetbtnVisable();
            if (pageAddDamage1 != null)
            {
                pageAddDamage1.stateSelect = 0;
            }
            await Navigation.PopAsync(true);
            fullPagePhotoDelyvery.fullPagePhotoDelyveryMV.SavePhoto();
        }
    }
}