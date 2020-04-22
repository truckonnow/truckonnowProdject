using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.Delyvery;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.AskPhoto;
using MDispatch.ViewModels.InspectionMV.Servise.Models;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV.DelyveryMV
{
    public class AskDelyveryMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        public InitDasbordDelegate initDasbordDelegate = null;
        private GetVechicleDelegate getVechicleDelegate = null;
        private GetShiping getShiping = null;

        public AskDelyveryMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, INavigation navigation, GetShiping getShiping,
            InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate, string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            this.getVechicleDelegate = getVechicleDelegate;
            this.getShiping = getShiping;
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            IdShip = idShip;
            OnDeliveryToCarrier = onDeliveryToCarrier;
            TotalPaymentToCarrier = totalPaymentToCarrier;
        }

        public string IdShip { get; set; }
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

        [System.Obsolete]
        public async void SaveAsk()
        {
            bool isNavigationMany = false;
            if (Navigation.NavigationStack.Count > 2)
            {
                await PopupNavigation.PushAsync(new LoadPage());
                isNavigationMany = true;
            }
            IVehicle car = GetTypeCar(VehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await CheckVechicleAndGoToResultPage();
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.AskWork("AskDelyvery", token, VehiclwInformation.Id, askDelyvery, ref description);
                    initDasbordDelegate.Invoke();
                });
                if (state == 2)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (Navigation.NavigationStack.Count > 1)
                    {
                        await Navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror(description, Navigation));
                }
                else if (state == 3)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    Navigation.RemovePage(Navigation.NavigationStack[1]);
                    DependencyService.Get<IToast>().ShowMessage("Answers to questions saved");
                }
                else if (state == 4)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (Navigation.NavigationStack.Count > 1)
                    {
                        await Navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
                }
            }
            else
            {
                if (Navigation.NavigationStack.Count > 1)
                {
                    await Navigation.PopAsync();
                }
            }
        }

        [System.Obsolete]
        private async Task CheckVechicleAndGoToResultPage()
        {
            List<VehiclwInformation> vehiclwInformation1s = getVechicleDelegate.Invoke();
            int indexCurrentVechecle = vehiclwInformation1s.FindIndex(v => v == VehiclwInformation);
            if (vehiclwInformation1s.Count - 1 == indexCurrentVechecle)
            {
                await PopupNavigation.PushAsync(new TempDialogPage());
                await Navigation.PushAsync(new ClientStart(managerDispatchMob, IdShip, initDasbordDelegate, OnDeliveryToCarrier, TotalPaymentToCarrier, vehiclwInformation1s[0], getShiping, getVechicleDelegate, false));
            }
            else
            {
                await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Deliveri", vehiclwInformation1s[indexCurrentVechecle + 1]));
                await Navigation.PushAsync(new AskPageDelyvery(managerDispatchMob, vehiclwInformation1s[indexCurrentVechecle + 1], IdShip, initDasbordDelegate, getVechicleDelegate, OnDeliveryToCarrier, TotalPaymentToCarrier, getShiping), true);
            }
        }

        private IVehicle GetTypeCar(string typeCar)
        {
            IVehicle car = null;
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
                case "Sedan":
                    {
                        car = new CarSedan();
                        break;
                    }
            }
            return car;
        }
    }
}