﻿using MDispatch.Models;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.VidgetFolder.View;
using MDispatch.View.GlobalDialogView;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading;
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

        private UnTimeOfInspection unTimeOfInspection = null;
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
                    UnTimeOfInspection = new UnTimeOfInspection(description);
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
            IsRefr = false;
        }

        private async void GoToInspectionDrive()
        {
            await Navigation.PushAsync(new FullPhotoTruck(managerDispatchMob, UnTimeOfInspection.IdDriver));
        }
    }
}