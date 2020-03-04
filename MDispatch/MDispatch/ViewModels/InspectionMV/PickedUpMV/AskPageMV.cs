using MDispatch.Models;
using MDispatch.NewElement;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.PageApp;
using Newtonsoft.Json;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
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
        private GetVechicleDelegate getVechicleDelegate = null;

        public AskPageMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, INavigation navigation, InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate,
             string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            this.getVechicleDelegate = getVechicleDelegate;
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            IdShip = idShip;
            OnDeliveryToCarrier = onDeliveryToCarrier;
            TotalPaymentToCarrier = totalPaymentToCarrier;
            IdShip = idShip;
            Ask = new Ask();
            Init();
        }

        private void Init()
        {
            if(vehiclwInformation.Type == "Coupe" || vehiclwInformation.Type == "SUV" || vehiclwInformation.Type == "Pickeup" || vehiclwInformation.Type == "Sedan")
            {
                Ask.TypeVehicle = VehiclwInformation.Type;
            }
        }

        public string IdShip { get; set; }
        public string OnDeliveryToCarrier { get; set; }
        public string TotalPaymentToCarrier { get; set; }

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

        public void ResetAskPhotoItem(byte[] oldRes, byte[] newRetake)
        {
            string base64 = JsonConvert.SerializeObject(newRetake);
            Photo photo = Ask.Any_personal_or_additional_items_with_or_in_vehicle.FirstOrDefault(a => a.Base64 == Convert.ToBase64String(oldRes));
            if (photo != null)
            {
                photo.Base64 = base64;
            }
        }


        [System.Obsolete]
        public async void SaveAsk(string indexTypeCar)
        {
            bool isNavigationMany = false;
            if (Navigation.NavigationStack.Count > 2)
            {
                await PopupNavigation.PushAsync(new LoadPage());
                isNavigationMany = true;
            }
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            FullPagePhoto fullPagePhoto = new FullPagePhoto(managerDispatchMob, VehiclwInformation, IdShip, $"{indexTypeCar}1.png", indexTypeCar, 1, initDasbordDelegate, getVechicleDelegate, "", OnDeliveryToCarrier, TotalPaymentToCarrier);
            await Navigation.PushAsync(fullPagePhoto);
            await Navigation.PushAsync(new CameraPagePhoto($"{indexTypeCar}1.png", fullPagePhoto, "PhotoIspection"));
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {

                await Task.Run(() =>
                {
                    state = managerDispatchMob.AskWork("SaveAsk", token, VehiclwInformation.Id, Ask, ref description);
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
    }
}