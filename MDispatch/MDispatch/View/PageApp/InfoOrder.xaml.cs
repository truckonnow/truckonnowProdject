using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoOrder : ContentPage
    {
        private InfoOrderMV infoOrderMV = null;

        public InfoOrder(ManagerDispatchMob managerDispatchMob, Shipping shipping, InitDasbordDelegate initDasbordDelegate)
        {
            this.infoOrderMV = new InfoOrderMV(managerDispatchMob, shipping, initDasbordDelegate) { Navigation = this.Navigation} ;
            InitializeComponent();
            BindingContext = this.infoOrderMV;
            InitElement();
        }

        private async void InitElement()
        {
            await Task.Run(() => Thread.Sleep(100));
            await Task.WhenAll(
                bloc1.TranslateTo(0, 0, 500, Easing.SpringOut),
                bloc2.TranslateTo(0, 0, 500, Easing.SpringOut),
                bloc3.TranslateTo(0, 0, 500, Easing.SpringOut),
                bloc4.TranslateTo(0, 0, 500, Easing.SpringOut),
                bloc5.TranslateTo(0, 0, 500, Easing.SpringOut)
                );
        }

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            listVehic.HeightRequest = Convert.ToInt32(infoOrderMV.Shipping.VehiclwInformations.Count * 120);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Button button = ((Button)sender);
            string id = button.FindByName<Label>("idL").Text;
            infoOrderMV.ToVehicleDetails(infoOrderMV.Shipping.VehiclwInformations.Find(v => v.Id == id));
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            if(infoOrderMV.Shipping.CurrentStatus == "Assigned")
            {
                infoOrderMV.ToStartInspection();
            }
            else if(infoOrderMV.Shipping.CurrentStatus == "Picked up")
            {
                infoOrderMV.ToStartInspectionDelyvery();
            }
        }
    }
}