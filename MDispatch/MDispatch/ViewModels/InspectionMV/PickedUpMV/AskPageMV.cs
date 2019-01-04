using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.PageApp;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.AskPhoto
{
    public class AskPageMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation  { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;

        public AskPageMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shipping, INavigation navigation, InitDasbordDelegate initDasbordDelegate)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            Shipping = shipping;
        }

        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        private Models.Ask ask = null;
        public Models.Ask Ask
        {
            get => ask;
            set => SetProperty(ref ask, value);
        }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        public async void SaveAsk(string indexTypeCar)
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.AskWork("SaveAsk", token, VehiclwInformation.Id, Ask, ref description);
                initDasbordDelegate.Invoke();
            });
            await PopupNavigation.PopAsync(true);
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
                await Navigation.PushAsync(new FullPagePhoto(managerDispatchMob, VehiclwInformation, Shipping, $"{indexTypeCar}1.png", indexTypeCar, 1, initDasbordDelegate));
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}