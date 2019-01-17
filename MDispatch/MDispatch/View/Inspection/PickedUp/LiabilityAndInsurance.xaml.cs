using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;
using static MDispatch.ViewModels.TAbbMV.ActiveMV;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LiabilityAndInsurance : ContentPage
	{
        LiabilityAndInsuranceMV liabilityAndInsuranceMV = null;

        public LiabilityAndInsurance (ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, InitDasbordDelegate initDasbordDelegate)
		{
            liabilityAndInsuranceMV = new LiabilityAndInsuranceMV(managerDispatchMob, vehiclwInformation, idShip, Navigation, initDasbordDelegate);
            InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            liabilityAndInsuranceMV.Continue();
        }
    }
}