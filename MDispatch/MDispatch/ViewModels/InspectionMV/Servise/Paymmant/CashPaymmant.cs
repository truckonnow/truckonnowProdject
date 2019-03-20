using Android.Provider;
using MDispatch.Models;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.Delyvery;
using MDispatch.View.Inspection.PickedUp;
using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV.Servise.Paymmant
{
    public class CashPaymmant : IPaymmant
    {
        public bool IsAskPaymmant { get; set; }
        public AskForUserDelyvery AskForUserDelyvery { get; set; }
        public LiabilityAndInsurance LiabilityAndInsurance { get; set; }

        private StackLayout block = null;

        public StackLayout GetStackLayout()
        {
            Entry entry = new Entry();
            entry.Placeholder = "$";
            entry.TextChanged += EntryTextChange;
            block = new StackLayout();
            Button button = new Button();
            button.Text = "Take";
            button.BackgroundColor = Color.BlueViolet;
            button.TextColor = Color.White;
            button.Clicked += ClickBtn;
            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(entry);
            stackLayout.Children.Add(button);
            stackLayout.Children.Add(block);
            return stackLayout;
        }

        private async void ClickBtn(object sender, EventArgs e)
        {
            if(AskForUserDelyvery != null)
            {
                await AskForUserDelyvery.Navigation.PushAsync(new CameraPaymmant(this, "Take a picture of what you pay"));
            }
            else
            {
                await LiabilityAndInsurance.Navigation.PushAsync(new CameraPaymmant(this, "Take a picture of what you pay"));
            }
        }

        private bool isEntrt = false;
        private void EntryTextChange(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isEntrt = true;
            }
            else
            {
                isEntrt = false;
            }
            if (AskForUserDelyvery != null)
            {
                AskForUserDelyvery.askForUsersDelyveryMW.AskForUserDelyveryM.CountPay = e.NewTextValue;
            }
            else
            {

            }
            if (isEntrt && isPhoto)
            {
                IsAskPaymmant = true;
            }
            else
            {
                IsAskPaymmant = false;
            }
        }

        private bool isPhoto = false;
        public void AddPhoto(byte[] photo)
        {
            if(block.Children.Count == 1)
            {
                block.Children.RemoveAt(0);
            }
            block.Children.Add(new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(photo)),
                HeightRequest = 40,
                WidthRequest = 40
            });
            AskForUserDelyvery.askForUsersDelyveryMW.AskForUserDelyveryM.PhotoPay = new Photo();
            if (AskForUserDelyvery != null)
            {
                AskForUserDelyvery.askForUsersDelyveryMW.AskForUserDelyveryM.PhotoPay.Base64 = JsonConvert.SerializeObject(photo);
            }
            else
            {

            }
            if (isEntrt && isPhoto)
            {
                IsAskPaymmant = true;
            }
            else
            {
                IsAskPaymmant = false;
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
    }
}
