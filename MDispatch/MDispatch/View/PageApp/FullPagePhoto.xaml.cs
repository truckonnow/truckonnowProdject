using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPagePhoto : ContentPage
    {
        private FullPagePhotoMV fullPagePhotoMV = null;
        public FullPagePhoto(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation)
        {
            fullPagePhotoMV = new FullPagePhotoMV(managerDispatchMob, vehiclwInformation);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = fullPagePhotoMV;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CameraPagePhoto(fullPagePhotoMV));
        }
    }
}