using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.GlobalDialogView;
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
        public InitDasbordDelegate initDasbordDelegate;

        public ActiveMV(ManagerDispatchMob managerDispatchMob, INavigation navigation)
        {
            Navigation = navigation;
            Shippings = new List<Shipping>();
            initDasbordDelegate = Init;
            this.managerDispatchMob = managerDispatchMob;
            RefreshCommand = new DelegateCommand(Init);
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
        
        public async void Init()
        {
            IsRefr = true;
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            List<Shipping> shippings = null;
            await Task.Run(() =>
            {
                state = managerDispatchMob.OrderWork("OrderGet", token, ref description, ref shippings);
            });
            if (state == 1)
            {
                await PopupNavigation.PushAsync(new Errror("Not Network", null));
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, null));
            }
            else if (state == 3)
            {
                Shippings = shippings;
                App.isInspection = Convert.ToBoolean(description);
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
            }
            IsRefr = false;
        }
    }
}