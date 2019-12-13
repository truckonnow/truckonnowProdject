using MDispatch.Models;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection.Delyvery;
using MDispatch.View.Inspection.PickedUp;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Paymmant
{
    public class CashPaymmant : IPaymmant
    {
        public bool IsAskPaymmant { get; set; }
        public AskForUserDelyvery AskForUserDelyvery { get; set; }
        public LiabilityAndInsurance LiabilityAndInsurance { get; set; }
        StackLayout stackLayout = null;

        [Obsolete]
        public StackLayout GetStackLayout()
        {
            Entry entry = new Entry();
            Button button = new Button();
            FlexLayout flexLayout = new FlexLayout();
            entry.Keyboard = Keyboard.Numeric;
            entry.Placeholder = "$";
            entry.TextChanged += EntryTextChange;
            button.Text = "I am paid";
            button.BackgroundColor = Color.BlueViolet;
            button.TextColor = Color.White;
            button.Clicked += ClickBtn;
            stackLayout = new StackLayout();
            stackLayout.Children.Add(entry);
            flexLayout.Children.Add(button);
            stackLayout.Children.Add(flexLayout);
            return stackLayout; 
        }

        bool isReCount = false;
        public void SaveVidopRecount(byte[] resRecord)
        {
            Video video = new Video();
            video.VideoBase64 = Convert.ToBase64String(resRecord);
            if (AskForUserDelyvery != null)
            {
                video.path = $"../Video/{AskForUserDelyvery.askForUsersDelyveryMW.VehiclwInformation.Id}/RecountPay.mp4";
                AskForUserDelyvery.askForUsersDelyveryMW.VideoRecount = video;
            }
            else
            {
                video.path = $"../Video/{LiabilityAndInsurance.liabilityAndInsuranceMV.IdVech}/RecountPay.mp4";
                LiabilityAndInsurance.liabilityAndInsuranceMV.VideoRecount = video;
            }
            ((FlexLayout)stackLayout.Children[1]).Children.RemoveAt(1);
            stackLayout.Children.RemoveAt(2);
            isReCount = true;
            CheckIsAskPaymmant();
        }

        bool isIamPay = false;
        [Obsolete]
        private async void ClickBtn(object sender, EventArgs e)
        {
            if(((Entry)stackLayout.Children[0]).Text != null && ((Entry)stackLayout.Children[0]).Text.Length > 0)
            {
                isIamPay = true;
                stackLayout.IsEnabled = false;
                await PopupNavigation.PushAsync(new Errror($"Give money for delivery to the driver {((Entry)stackLayout.Children[0]).Text}", null));
            }
            else
            {
                isIamPay = false;
                await PopupNavigation.PushAsync(new Errror("You must enter the amount of payment for delivery", null));
            }
            CheckIsAskPaymmant();
        }

        private void EntryTextChange(object sender, TextChangedEventArgs e)
        {
            if (AskForUserDelyvery != null)
            {
                AskForUserDelyvery.askForUsersDelyveryMW.AskForUserDelyveryM.CountPay = e.NewTextValue;
            }
            else
            {
                LiabilityAndInsurance.liabilityAndInsuranceMV.CountPay = e.NewTextValue;
            }
        }

        public CashPaymmant(object page)
        {
            AskForUserDelyvery = (page as AskForUserDelyvery);
            if(AskForUserDelyvery == null)
            {
                LiabilityAndInsurance = (LiabilityAndInsurance)page;
            }
        }

        private void CheckIsAskPaymmant()
        {
            if(isIamPay)
            {
                IsAskPaymmant = true;
            }
            else
            {
                IsAskPaymmant = false;
            }
        }
    }
}
