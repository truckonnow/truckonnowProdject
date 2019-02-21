using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CopyLibaryAndInsurance : PopupPage
    {
        LiabilityAndInsuranceMV liabilityAndInsuranceMV = null;

        public CopyLibaryAndInsurance (LiabilityAndInsuranceMV liabilityAndInsuranceMV)
		{
            this.liabilityAndInsuranceMV = liabilityAndInsuranceMV;
			InitializeComponent ();
            BindingContext = this.liabilityAndInsuranceMV;
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            blockEmaile.IsVisible = true;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            liabilityAndInsuranceMV.GoEvaluationAndSurvey();
        }
    }
}