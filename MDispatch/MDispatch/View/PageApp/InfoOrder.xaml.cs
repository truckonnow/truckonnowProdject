using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV;
using System;
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
        }

        private void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            listVehic.HeightRequest = 80 + Convert.ToInt32(infoOrderMV.Shipping.VehiclwInformations.Count * 125);
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