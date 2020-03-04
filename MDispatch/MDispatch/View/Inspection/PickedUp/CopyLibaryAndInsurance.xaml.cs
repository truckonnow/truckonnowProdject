using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

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

        [Obsolete]
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            liabilityAndInsuranceMV.GoEvaluationAndSurvey();

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                emailE.TextColor = Color.Default;
                return true;
            }
            catch
            {

                emailE.TextColor = Color.Red;
                return false;
            }
        }

        private void EmailE_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            IsValidEmail(e.NewTextValue);
        }

        [Obsolete]
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            if(IsValidEmail(emailE.Text))
            {
                liabilityAndInsuranceMV.GoEvaluationAndSurvey();
                await liabilityAndInsuranceMV.SendLiabilityAndInsuranceEmaile();

            }
        }
    }
}