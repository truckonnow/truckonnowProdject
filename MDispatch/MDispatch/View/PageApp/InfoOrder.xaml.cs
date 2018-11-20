using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV;
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
            listVehic.HeightRequest = 100 + Convert.ToInt32(infoOrderMV.Shipping.VehiclwInformations.Count * 135);
        }
    }
}