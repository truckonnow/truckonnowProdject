using MDispatch.NewElement;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp.CameraPageFolder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPartsBeen : CameraPage
    {
        Ask2Page ask2Page = null;

        public CameraPartsBeen(Ask2Page ask2Page)
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
            this.ask2Page = ask2Page;
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            ask2Page.AddPhotoPartsBeen(result.Result);
            await Navigation.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}