using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.PageApp;
using Rg.Plugins.Popup.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection.Delyvery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientStart : ContentPage
    {
        private AskForUserDelyvery askForUserDelyvery = null;

        public ClientStart(ManagerDispatchMob managerDispatchMob, string idShip, InitDasbordDelegate initDasbordDelegate, string onDeliveryToCarrier, string totalPaymentToCarrier, VehiclwInformation vehiclwInformation, GetShiping getShiping, GetVechicleDelegate getVechicleDelegate, bool isproplem)
        {
            askForUserDelyvery = new AskForUserDelyvery(managerDispatchMob, idShip, initDasbordDelegate, onDeliveryToCarrier, totalPaymentToCarrier, vehiclwInformation, getShiping, getVechicleDelegate, isproplem);
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(askForUserDelyvery);
            Navigation.RemovePage(Navigation.NavigationStack[1]);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new ContactInfo());
        }
    }
}