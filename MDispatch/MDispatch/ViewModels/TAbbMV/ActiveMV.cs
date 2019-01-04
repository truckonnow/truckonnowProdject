using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.TAbbMV
{
    public class ActiveMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public InitDasbordDelegate initDasbordDelegate;

        public ActiveMV(ManagerDispatchMob managerDispatchMob, INavigation navigation)
        {
            Navigation = navigation;
            Shippings = new List<Shipping>();
            initDasbordDelegate = Init;
            //VehiclwInformation vehiclwInformation = new VehiclwInformation();
            //vehiclwInformation.Year = "1992";
            //vehiclwInformation.Make = "Porsche";
            //vehiclwInformation.Model = "911";
            //Shipping shipping = new Shipping();
            //shipping.Id = "2222";
            //shipping.VehiclwInformations = new List<VehiclwInformation>();
            //shipping.VehiclwInformations.Add(vehiclwInformation);
            //shipping.CityP = "dallas";
            //shipping.StateP = "TX";
            //shipping.PickupExactly = "10.01.18";
            //shipping.CityD = "north";
            //shipping.StateD = "OH";
            //shipping.DispatchDate = "12.01.18";
            //shipping.OnDeliveryToCarrier = "COP";
            //Shippings.Add(shipping);
            //Shippings.Add(shipping);
            this.managerDispatchMob = managerDispatchMob;
            RefreshCommand = new DelegateCommand(Init);
            Init();
        }

        private List<Shipping> shippings = null;
        public List<Shipping> Shippings
        {
            get => shippings;
            set => SetProperty(ref shippings, value);
        }

        private bool isRefr = false;
        public bool IsRefr
        {
            get => isRefr;
            set => SetProperty(ref isRefr, value);
        } 
        
        public async void Init()
        {
            IsRefr = true;
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            List<Shipping> shippings = null;
            await Task.Run(() =>
            {
                state = managerDispatchMob.OrderWork("OrderGet", token, ref description, ref shippings);
            });
            if (state == 1)
            {
                //FeedBack = "Not Network";
            }
            else if (state == 2)
            {
                //FeedBack = description;
            }
            else if (state == 3)
            {
                Shippings = shippings;
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
            IsRefr = false;
        }
    }
}