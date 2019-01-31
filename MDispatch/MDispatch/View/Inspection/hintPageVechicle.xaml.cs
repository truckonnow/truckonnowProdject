﻿using MDispatch.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HintPageVechicle : PopupPage
    {
        public HintPageVechicle(string hintText, VehiclwInformation vehiclwInformation)
        {
            InitializeComponent();
            lHint.Text = hintText;
            lMake.Text = vehiclwInformation.Make;
            lModel.Text = vehiclwInformation.Model;
            lYear.Text = vehiclwInformation.Year;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}