using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
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

        private async void SaveDelivery()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.OrderOneWork("Save", Shipping.Id, token, Shipping.idOrder, Shipping.NameD, Shipping.ContactNameD, Shipping.AddresD,
                    Shipping.CityD, Shipping.StateD, Shipping.ZipD, Shipping.PhoneD, Shipping.EmailD, "Delivery", ref description);
            });
            await PopupNavigation.PopAsync(true);
            await Navigationn.PopAsync(true);
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

                //Feedback = "";
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}