using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV
{
    public class EditDeliveryInformationMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public DelegateCommand SavePikedUpCommand { get; set; }
        public INavigation Navigationn { get; set; }

        public EditDeliveryInformationMV(ManagerDispatchMob managerDispatchMob, Shipping shipping)
        {
            this.managerDispatchMob = managerDispatchMob;
            Shipping = shipping;
            SavePikedUpCommand = new DelegateCommand(SaveDelivery);
        }

        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        [System.Obsolete]
        private async void SaveDelivery()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.OrderOneWork("Save", Shipping.Id, token, Shipping.idOrder, Shipping.NameD, Shipping.ContactNameD, Shipping.AddresD,
                    Shipping.CityD, Shipping.StateD, Shipping.ZipD, Shipping.PhoneD, Shipping.EmailD, "Delivery", ref description);
                });
                await PopupNavigation.PopAsync(true);
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
                    DependencyService.Get<IToast>().ShowMessage("Information about delivery saved");
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigationn));
                }
            }
        }
    }
}