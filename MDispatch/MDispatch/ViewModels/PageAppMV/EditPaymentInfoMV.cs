using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV
{
    public class EditPaymentInfoMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public DelegateCommand SavePaymentUpCommand { get; set; }
        public INavigation Navigationn { get; set; }

        public EditPaymentInfoMV(ManagerDispatchMob managerDispatchMob, Shipping shipping)
        {
            this.managerDispatchMob = managerDispatchMob;
            Shipping = shipping;
            SavePaymentUpCommand = new DelegateCommand(SavePayments);
            SorseDropDown = new string[]
            {
                "COD",
                "COP",
                "2 days",
                "5 days",
                "7 days",
                "10 days",
                "15 days",
                "20 days",
                "30 days",
                "45 days",
            };
        }

        private string[] sorseDropDown = null;
        public string[] SorseDropDown
        {
            get => sorseDropDown;
            set => SetProperty(ref sorseDropDown, value);
        }

        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        private async void SavePayments()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.OrderOneWork("Save", Shipping.Id, token, "Payment", Shipping.PriceListed, Shipping.TotalPaymentToCarrier, ref description);
            });
            await PopupNavigation.PopAsync(true);
            await Navigationn.PopAsync(true);
            if (state == 1)
            {
                await PopupNavigation.PushAsync(new Errror("Not Network", Navigationn));
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, Navigationn));
            }
            else if (state == 3)
            {
                DependencyService.Get<IToast>().ShowMessage("Information about Paymmant saved");
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigationn));
            }
        }
    }
}