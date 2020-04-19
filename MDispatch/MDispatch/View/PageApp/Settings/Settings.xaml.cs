using MDispatch.NewElement;
using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV.Settings;
using Plugin.LatestVersion;
using Plugin.Settings;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        private SettingsMV settingsMV = null;

        public Settings(ManagerDispatchMob managerDispatchMob)
        {
            settingsMV = new SettingsMV(managerDispatchMob) { Navigation = this.Navigation };
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = settingsMV;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        [Obsolete]
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri($"{Config.BaseSiteUrl}/Doc/{CrossSettings.Current.GetValueOrDefault("IdDriver", "0")}"));
        }

        [Obsolete]
        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri($"{Config.BaseSiteUrl}/Doc?truckPlate={settingsMV.PlateTruck1}&trailerPlate={settingsMV.PlateTrailer1}"));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            settingsMV.OutAccount();
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await CrossLatestVersion.Current.OpenAppInStore();
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
            await Navigation.PushModalAsync(new ScanPlateSettings(settingsMV, "truck"));
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
            await Navigation.PushModalAsync(new ScanPlateSettings(settingsMV, "trailer"));
        }
    }
}