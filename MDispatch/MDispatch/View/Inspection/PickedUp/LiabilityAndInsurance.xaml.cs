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

        public LiabilityAndInsurance (ManagerDispatchMob managerDispatchMob, string idVech, string idShip, InitDasbordDelegate initDasbordDelegate)
		{
            liabilityAndInsuranceMV = new LiabilityAndInsuranceMV(managerDispatchMob, idVech, idShip, Navigation, initDasbordDelegate);
            InitializeComponent ();
            BindingContext = liabilityAndInsuranceMV;
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            liabilityAndInsuranceMV.Continue();
        }
    }
}