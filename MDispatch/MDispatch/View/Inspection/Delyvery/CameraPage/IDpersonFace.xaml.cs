using MDispatch.NewElement;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.Delyvery.CameraPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IDpersonFace : NewElement.CameraPage
    {
        public AskPageDelyvery askPageDelyvery = null;

        public IDpersonFace(AskPageDelyvery askPageDelyvery)
        {
            this.askPageDelyvery = askPageDelyvery;
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            askPageDelyvery.AddPhoto(result.Result);
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}