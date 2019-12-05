﻿using MDispatch.Service;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.TAbbMV;
using MDispatch.ViewModels.TAbbMV.DialogAsk;
using Plugin.Settings;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.TabPage.Tab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivePage : ContentPage
    {
        public ActiveMV activeMV = null;
        private StackLayout SelectStackLayout = null;
        private InitDasbordDelegate initDasbordDelegate = null;
        private Timer timer = null;

        public ActivePage (ManagerDispatchMob managerDispatchMob, INavigation navigation)
		{
            this.activeMV = new ActiveMV(managerDispatchMob, navigation);
			InitializeComponent ();
            BindingContext = this.activeMV;
        }

        private async void ReminderTrackInspaction(object s)
        {
            if (activeMV.UnTimeOfInspection.IsInspection)
            {
                await blockInspactionTrack.RotateTo(3, 50);
                Vibration.Vibrate(30);
                await blockInspactionTrack.RotateTo(-3, 50);
                Vibration.Vibrate(30);
                await blockInspactionTrack.RotateTo(3, 50);
                Vibration.Vibrate(30);
                await blockInspactionTrack.RotateTo(-3, 50);
                Vibration.Vibrate(30);
                await blockInspactionTrack.RotateTo(3, 50);
                Vibration.Vibrate(30);
                await blockInspactionTrack.RotateTo(-3, 50);
                Vibration.Vibrate(30);
                await blockInspactionTrack.RotateTo(3, 50);
                Vibration.Vibrate(30);
                await blockInspactionTrack.RotateTo(-3, 50);
                Vibration.Vibrate(30);
                await blockInspactionTrack.RotateTo(0, 50);
                Vibration.Vibrate(30);
            }
            else
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            StackLayout stackLayout = ((StackLayout)sender).FindByName<StackLayout>("st");

            if (SelectStackLayout != null)
            {
                SelectStackLayout.BackgroundColor = Color.White;
            }
            SelectStackLayout = stackLayout;
            SelectStackLayout.BackgroundColor = Color.FromHex("#f5c8c8");
        }

        [Obsolete]
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (!activeMV.UnTimeOfInspection.ISMaybiInspection)
            {
                await PopupNavigation.PushAsync(new AskHint(activeMV));
            }
            else
            {
                if (SelectStackLayout != null)
                {
                    SelectStackLayout.BackgroundColor = Color.White;
                    SelectStackLayout = null;
                }
                string idOrder = null;
                StackLayout stackLayout = (StackLayout)sender;
                Label idorderL = stackLayout.FindByName<Label>("idOrder");
                if (idorderL != null)
                {
                    idOrder = idorderL.Text;
                }
                else
                {
                    idOrder = stackLayout.Parent.Parent.FindByName<Label>("idOrder").Text;
                }
                await activeMV.Navigation.PushAsync(new InfoOrder(activeMV.managerDispatchMob, activeMV.Shippings.Find(s => s.Id == idOrder), activeMV.initDasbordDelegate));
            }
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped(sender, e);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped_1(sender, e);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            activeMV.OutAccount();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            timer = new Timer(new TimerCallback(ReminderTrackInspaction), null, 5000, 5000);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri($"http://truckonnow.com/Equipment/Document?id={CrossSettings.Current.GetValueOrDefault("IdDriver", "0")}"));
        }
    }
}