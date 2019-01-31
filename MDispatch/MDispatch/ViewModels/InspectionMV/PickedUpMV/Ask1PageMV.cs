using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.AskPhoto;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.PickedUp;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
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
        private GetVechicleDelegate getVechicleDelegate = null;

        public Ask1PageMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, INavigation navigation, InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate)
        {
            this.getVechicleDelegate = getVechicleDelegate;
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            IdShip = idShip;
        }

        public string IdShip { get; set; }

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
                CheckVechicleAndGoToResultPage();
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }

        private async void CheckVechicleAndGoToResultPage()
        {
            List<VehiclwInformation> vehiclwInformation1s = getVechicleDelegate.Invoke();
            int indexCurrentVechecle = vehiclwInformation1s.FindIndex(v => v == VehiclwInformation);
            if(vehiclwInformation1s.Count-1 == indexCurrentVechecle)
            {
                await Navigation.PushAsync(new AskForUser(managerDispatchMob, VehiclwInformation, IdShip, initDasbordDelegate));
                Navigation.RemovePage(Navigation.NavigationStack[2]);
                await PopupNavigation.PushAsync(new TempPageHint1());
            }
            else
            {
                await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation1s[indexCurrentVechecle + 1]));
                await Navigation.PushAsync(new AskPage(managerDispatchMob, vehiclwInformation1s[indexCurrentVechecle+1], IdShip, initDasbordDelegate, getVechicleDelegate), true);
                Navigation.RemovePage(Navigation.NavigationStack[2]);
            }
        }
    }
}
