using MDispatch.Models;
using MDispatch.Service;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection.Delyvery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientStart : ContentPage
    {

        public ClientStart(ManagerDispatchMob managerDispatchMob, string idShip, InitDasbordDelegate initDasbordDelegate, string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
        }
    }
}