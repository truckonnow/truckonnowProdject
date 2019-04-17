using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
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

        public AskForUserMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, INavigation navigation, InitDasbordDelegate initDasbordDelegate, string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            IdShip = idShip;
            OnDeliveryToCarrier = onDeliveryToCarrier;
            TotalPaymentToCarrier = totalPaymentToCarrier;
        }

        public string IdShip { get; set; }
        public string OnDeliveryToCarrier { get; set; }
        public string TotalPaymentToCarrier { get; set; }

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
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Navigation.PushAsync(new LiabilityAndInsurance(managerDispatchMob, VehiclwInformation.Id, IdShip, initDasbordDelegate, OnDeliveryToCarrier, TotalPaymentToCarrier), true);
            Navigation.RemovePage(Navigation.NavigationStack[2]);
            await Task.Run(() =>
            {
                state = managerDispatchMob.AskWork("AskFromUser", token, VehiclwInformation.Id, AskForUser, ref description);
                initDasbordDelegate.Invoke();
            });
            if (state == 1)
            {
                await PopupNavigation.PushAsync(new Errror("Not Network", Navigation));
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, Navigation));
            }
            else if (state == 3)
            {
                DependencyService.Get<IToast>().ShowMessage("Answers to questions saved");
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
            }
        }
    }
}