using MDispatch.NewElement;
using MDispatch.Service;
using MDispatch.Vidget.VM;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MDispatch.Vidget.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : MDispatch.NewElement.CameraPage
    {
        private FullPhotoTruckVM fullPhotoTruckVM = null;

        public CameraPage(ManagerDispatchMob managerDispatchMob, string idDriver, int indexCurrent, InitDasbordDelegate initDasbordDelegate)
        {
            fullPhotoTruckVM = new FullPhotoTruckVM(managerDispatchMob, idDriver, indexCurrent, Navigation, initDasbordDelegate);
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
            BindingContext = fullPhotoTruckVM;
            paternPhoto.Source = $"Hint{indexCurrent}.jpg";
            InitElement();
        }

        private  void InitElement()
        {
            TimerCallback tm = new TimerCallback(Transparency);
            Timer timer = new Timer(tm, null, 3000, Timeout.Infinite);
        }

        private void Transparency(object state)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await paternPhoto.FadeTo(0.6, 500);
            });
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            await Navigation.PopToRootAsync();
        }

        [System.Obsolete]
        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
            {
                return;
            }
            await fullPhotoTruckVM.AddPhoto(result.Result);
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            BackToRootPage();
            return base.OnBackButtonPressed();
        }

        private async void BackToRootPage()
        {
            if (Navigation.NavigationStack.Count > 3)
            {
                await Navigation.PopToRootAsync();
            }
        }
    }
}