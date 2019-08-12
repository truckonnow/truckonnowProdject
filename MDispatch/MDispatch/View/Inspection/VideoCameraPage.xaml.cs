using MDispatch.ViewModels.InspectionMV.Servise.Paymmant;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCameraPage : NewElement.CustomVideoCam.VideoCameraPage
    {
        private CashPaymmant cashPaymmant = null;

        public VideoCameraPage(CashPaymmant cashPaymmant)
        {
            this.cashPaymmant = cashPaymmant;
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
        }

        private async void VideoCameraPage_OnPhotoResult(NewElement.PhotoResultEventArgs result)
        {
            await Navigation.PopAsync();
            if (!result.Success)
                return;
            cashPaymmant.SaveVidopRecount(result.Result);
        }
    }
}