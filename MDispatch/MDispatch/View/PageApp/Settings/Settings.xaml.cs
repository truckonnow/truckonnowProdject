using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV.Settings;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Device.OpenUri(new Uri($"http://truckonnow.com/Doc/{CrossSettings.Current.GetValueOrDefault("IdDriver", "0")}"));
        }
    }
}