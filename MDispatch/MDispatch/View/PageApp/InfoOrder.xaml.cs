using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.AskPhoto.DialogPage;
using MDispatch.ViewModels.PageAppMV;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoOrder : ContentPage
    {
        private InfoOrderMV infoOrderMV = null;

        public InfoOrder(ManagerDispatchMob managerDispatchMob, Shipping shipping)
        {
            this.infoOrderMV = new InfoOrderMV(managerDispatchMob, shipping) { Navigation = this.Navigation} ;
            InitializeComponent();
            BindingContext = this.infoOrderMV;
        }

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            listVehic.HeightRequest = 80 + Convert.ToInt32(infoOrderMV.Shipping.VehiclwInformations.Count * 125);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            string id = button.FindByName<Label>("idL").Text;
            infoOrderMV.ToPhotoFull(infoOrderMV.Shipping.VehiclwInformations.Find(v => v.Id == id));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            string id = button.FindByName<Label>("idL").Text;
            infoOrderMV.ToVehicleDetails(infoOrderMV.Shipping.VehiclwInformations.Find(v => v.Id == id));
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            if (infoOrderMV.Shipping.VehiclwInformations.Count < 1)
            {
                await PopupNavigation.PushAsync(new ConfirrmPage(infoOrderMV), true);
            }
            else
            {
                infoOrderMV.ToStartInspection(infoOrderMV.Shipping.VehiclwInformations[0]);
            }

        }
    }
}