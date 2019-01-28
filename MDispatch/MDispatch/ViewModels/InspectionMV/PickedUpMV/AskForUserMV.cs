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

namespace MDispatch.ViewModels.InspectionMV.PickedUpMV
{
    public class AskForUserMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;

        public AskForUserMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, INavigation navigation, InitDasbordDelegate initDasbordDelegate)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            IdShip = idShip;
        }

        public string IdShip { get; set; }

        private AskFromUser askForUser = null;
        public AskFromUser AskForUser
        {
            get => askForUser;
            set => SetProperty(ref askForUser, value);
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
                state = managerDispatchMob.AskWork("AskFromUser", token, VehiclwInformation.Id, AskForUser, ref description);
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
                await Navigation.PushAsync(new LiabilityAndInsurance(managerDispatchMob, VehiclwInformation.Id, IdShip, initDasbordDelegate), true);
                Navigation.RemovePage(Navigation.NavigationStack[2]);
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}