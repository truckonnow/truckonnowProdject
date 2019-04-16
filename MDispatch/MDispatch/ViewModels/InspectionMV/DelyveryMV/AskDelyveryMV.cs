using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.AskPhoto;
using MDispatch.ViewModels.InspectionMV.Models;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV.DelyveryMV
{
    public class AskDelyveryMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;
        private GetVechicleDelegate getVechicleDelegate = null;

        public AskDelyveryMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, INavigation navigation, 
            InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate, string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            this.getVechicleDelegate = getVechicleDelegate;
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            IdShip = idShip;
            OnDeliveryToCarrier = onDeliveryToCarrier;
            TotalPaymentToCarrier = totalPaymentToCarrier;
        }

        private string IdShip { get; set; }
        private string OnDeliveryToCarrier { get; set; }
        private string TotalPaymentToCarrier { get; set; }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        private AskDelyvery askDelyvery = null;
        public AskDelyvery AskDelyvery
        {
            get => askDelyvery;
            set => SetProperty(ref askDelyvery, value);
        }

        public async void SaveAsk()
        {
            ICar car = GetTypeCar(VehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, VehiclwInformation, IdShip, $"{car.typeIndex}{car.GetIndexCarFullPhoto(1)}.png", car.typeIndex, 1, initDasbordDelegate, getVechicleDelegate, car.GetNameLayout(car.GetIndexCarFullPhoto(1)), OnDeliveryToCarrier, TotalPaymentToCarrier));
            Navigation.RemovePage(Navigation.NavigationStack[2]);
            await Task.Run(() =>
            {
                state = managerDispatchMob.AskWork("AskDelyvery", token, VehiclwInformation.Id, askDelyvery, ref description);
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
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
            }
        }

        private ICar GetTypeCar(string typeCar)
        {
            ICar car = null;
            switch (typeCar)
            {
                case "PickUp":
                    {
                        car = new CarPickUp();
                        break;
                    }
                case "Coupe":
                    {
                        car = new CarCoupe();
                        break;
                    }
                case "Suv":
                    {
                        car = new CarSuv();
                        break;
                    }
            }
            return car;
        }
    }
}