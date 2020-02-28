using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV.Settings;
using Plugin.Settings;
using System;
using Xamarin.Forms;
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
            BindingContext = settingsMV;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri($"http://192.168.31.44/Doc/{CrossSettings.Current.GetValueOrDefault("IdDriver", "0")}"));
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri($"http://192.168.31.44/Doc?truckPlate={settingsMV.PlateTruck1}&trailerPlate={settingsMV.PlateTrailer1}"));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            settingsMV.OutAccount();
        }
    }
}