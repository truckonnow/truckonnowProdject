using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.PageApp;
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

        public ClientStart(ManagerDispatchMob managerDispatchMob, string idShip, InitDasbordDelegate initDasbordDelegate, string onDeliveryToCarrier, string totalPaymentToCarrier, GetShiping getShiping, GetVechicleDelegate getVechicleDelegate)
        {
            askForUserDelyvery = new AskForUserDelyvery(managerDispatchMob, idShip, initDasbordDelegate, onDeliveryToCarrier, totalPaymentToCarrier, getShiping, getVechicleDelegate);
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(askForUserDelyvery);
        }
    }
}