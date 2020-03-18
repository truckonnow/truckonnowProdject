using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV.PickedUpMV
{
    public class Ask2PageMW : BindableBase
    {

        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        public InitDasbordDelegate initDasbordDelegate = null;

        public Ask2PageMW(ManagerDispatchMob managerDispatchMob, string idVech, string idShip, INavigation navigation, InitDasbordDelegate initDasbordDelegate)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            IdShip = idShip;
            IdVech = idVech;
        }

        public string IdShip { get; set; }
        public string IdVech { get; set; }

        private Ask2 ask2 = null;
        public Ask2 Ask2
        {
            get => ask2;
            set => SetProperty(ref ask2, value);
        }   

        [System.Obsolete]
        public async void Continue()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.Recurent(token, IdShip, "Picked up", ref description);
                    initDasbordDelegate.Invoke();
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    await Navigation.PopToRootAsync();
                    DependencyService.Get<IToast>().ShowMessage("Answers to questions saved");
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
        }

        [Obsolete]
        public async void SaveAsk()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.AskWork("SaveAsk2", token, IdShip, Ask2, ref description);
                    initDasbordDelegate.Invoke();
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, Navigation));
                }
                else if (state == 3)
                {
                    Continue();
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
                }
            }
        }
    }
}