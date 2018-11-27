﻿using MDispatch.Models;
using MDispatch.Service;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
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
                "Other",
                "COD",
                "COP",
                "QuickPay",
                "Comcheck",
                "5 days",
                "7 days",
                "10 days",
                "15 days",
                "20 days",
                "30 days",
                "45 days",
                "CKOD",
                "ACH"
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
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.OrderOneWork("Save", Shipping.Id, token, "Payment", Shipping.PriceListed, Shipping.OnDeliveryToCarrier, ref description);
            });
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
