using MDispatch.Models;
using MDispatch.Service;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection.PickedUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientStart : ContentPage
    {
        private AskForUser askForUser = null;

        public ClientStart(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, InitDasbordDelegate initDasbordDelegate, string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            askForUser = new AskForUser(managerDispatchMob, vehiclwInformation, idShip, initDasbordDelegate, onDeliveryToCarrier, totalPaymentToCarrier);
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(askForUser);
            Navigation.RemovePage(Navigation.NavigationStack[1]);
        }
    }
}