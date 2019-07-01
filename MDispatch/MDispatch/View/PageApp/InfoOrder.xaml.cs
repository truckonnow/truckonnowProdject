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
    public partial class InfoOrder : ContentPage, IAnimatonPage
    {
        private InfoOrderMV infoOrderMV = null;
        private bool isNextPage = false;

        public InfoOrder(ManagerDispatchMob managerDispatchMob, Shipping shipping, InitDasbordDelegate initDasbordDelegate)
        {
            this.infoOrderMV = new InfoOrderMV(managerDispatchMob, shipping, initDasbordDelegate) { Navigation = this.Navigation} ;
            InitializeComponent();
            BindingContext = this.infoOrderMV;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Delay(100).ContinueWith(t => InitElement());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //Task.Delay(500).ContinueWith(t => UnInitElement());
        }

        public async void UnInitElement()
        {
            await Task.WhenAll(
                    ctp.TranslateTo(400, 0, 500, Easing.SpringIn)
                    );
        }

        public async void InitElement()
        {
            if (isNextPage)
            {
                await Task.WhenAll(
                    ctp.TranslateTo(0, 0, 500)
                    );
            }
        }

        public void AnimationNextPage(Page page)
        {
            isNextPage = true;
        }
    }
}