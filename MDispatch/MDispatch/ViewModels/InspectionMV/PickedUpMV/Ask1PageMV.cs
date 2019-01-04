using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.Inspection.PickedUp;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV
{
    public class Ask1PageMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;

        public Ask1PageMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shipping, INavigation navigation, InitDasbordDelegate initDasbordDelegate)
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

        private Ask1 ask1 = null;
        public Ask1 Ask1
        {
            get => ask1;
            set => SetProperty(ref ask1, value);
        }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        public async void SaveAsk()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.AskWork("SaveAsk1", token, VehiclwInformation.Id, Ask1, ref description);
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
                await Navigation.PushAsync(new AskForUser(managerDispatchMob, VehiclwInformation, Shipping, initDasbordDelegate));
                await PopupNavigation.PushAsync(new TempPageHint1());

            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}
