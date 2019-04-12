using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EvaluationAndSurveyDialog : PopupPage
    {
        LiabilityAndInsuranceMV liabilityAndInsuranceMV = null;
        INavigation Navigation1 = null;

        public EvaluationAndSurveyDialog (LiabilityAndInsuranceMV liabilityAndInsuranceMV, INavigation navigation)
		{
            Navigation1 = navigation;
            this.liabilityAndInsuranceMV = liabilityAndInsuranceMV;
			InitializeComponent ();
            BindingContext = this.liabilityAndInsuranceMV;
		}

        private async void Button_Clicked_1(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAllAsync(true);
            if (liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation == "COD" || liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation == "COP" || liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation == "Biling")
            {
                liabilityAndInsuranceMV.Continue();
                await Navigation1.PopToRootAsync();
            }
            else
            {
                await Navigation1.PushAsync(new CameraPaymmant(liabilityAndInsuranceMV, ""));
            }
        }
    }
}