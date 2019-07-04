using MDispatch.Models;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View.A_R;
using MDispatch.View.GlobalDialogView;
using MDispatch.ViewModels.TAbbMV.DialogAsk;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
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
        public DelegateCommand GoToInspectionDriveCommand { get; set; }
        public InitDasbordDelegate initDasbordDelegate;

        public ActiveMV(ManagerDispatchMob managerDispatchMob, INavigation navigation)
        {
            Navigation = navigation;
            Shippings = new List<Shipping>();
            initDasbordDelegate = Init;
            this.managerDispatchMob = managerDispatchMob;
            RefreshCommand = new DelegateCommand(Init);
            UnTimeOfInspection = new UnTimeOfInspection();
            GoToInspectionDriveCommand = new DelegateCommand(GoToInspectionDrive);
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

        private UnTimeOfInspection unTimeOfInspection = new UnTimeOfInspection();
        public UnTimeOfInspection UnTimeOfInspection
        {
            get => unTimeOfInspection;
            set => SetProperty(ref unTimeOfInspection, value);
        }

        [Obsolete]
        public async void Init()
        {
            IsRefr = true;
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            List<Shipping> shippings = null;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.OrderWork("OrderGet", token, ref description, ref shippings);
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    Shippings = shippings;
                    await Task.Run(() =>
                    {
                        UnTimeOfInspection = new UnTimeOfInspection(description);
                        if (!UnTimeOfInspection.ISMaybiInspection)
                        {
                            Device.BeginInvokeOnMainThread(async () => await PopupNavigation.PushAsync(new AskHint(this)));
                        }
                    });
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
            IsRefr = false;
        }

        [Obsolete]
        public async void GoToInspectionDrive()
        {
            IsRefr = true;
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            bool isInspection = default;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.DriverWork("CheckInspeacktion", token, ref description, ref isInspection);
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    if (isInspection)
                    {
                        Init();
                        await PopupNavigation.PushAsync(new Errror("You have already passed inspection today", null));
                    }
                    else
                    {
                        await Navigation.PushAsync(new Vidget.View.CameraPage(managerDispatchMob, UnTimeOfInspection.IdDriver, 1, initDasbordDelegate));
                    }
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
            IsRefr = false;
        }

        public async void OutAccount()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            bool isInspection = default;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.A_RWork("Clear", null, null, ref description, ref token);
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror("Error", null));
                }
                else if (state == 3)
                {
                    CrossSettings.Current.Remove("Token");
                    App.isAvtorization = false;
                    App.Current.MainPage = new NavigationPage(new Avtorization());
                    IsRefr = true;
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
            IsRefr = false;
        }
    }
}