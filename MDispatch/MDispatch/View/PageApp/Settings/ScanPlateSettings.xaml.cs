using MDispatch.NewElement;
using MDispatch.ViewModels.PageAppMV.Settings;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPlateSettings : CameraPage
    {
        private SettingsMV settingsMV = null;
        private string typeDetect = null;

        public ScanPlateSettings(SettingsMV settingsMV, string typeDetect)
        {
            this.typeDetect = typeDetect;
            this.settingsMV = settingsMV;
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
               .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
        }

        [Obsolete]
        private async void CameraPage_OnScan(PhotoResultEventArgs result)
        {
            if (!result.Success)
            {
                return;
            }
            settingsMV.DetectText(result.Result, typeDetect);
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            await Navigation.PopModalAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            await Navigation.PopModalAsync();
        }
    }
}