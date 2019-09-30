using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EvaluationAndSurveyDialog1 : PopupPage
    {
        AskForUsersDelyveryMW askForUsersDelyveryMW = null;
        INavigation Navigation1 = null;

        public EvaluationAndSurveyDialog1 (AskForUsersDelyveryMW askForUsersDelyveryMW)
		{
            this.askForUsersDelyveryMW = askForUsersDelyveryMW;
			InitializeComponent ();
            BindingContext = this.askForUsersDelyveryMW;
		}

        [System.Obsolete]
        private async void Button_Clicked_1(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
            await PopupNavigation.PushAsync(new TempPageHint4());
            if (askForUsersDelyveryMW.Payment == "COD" || askForUsersDelyveryMW.Payment == "COP" || askForUsersDelyveryMW.Payment == "Biling")
            {
                await Navigation.PopToRootAsync(true);
                //askForUsersDelyveryMW.Continue();
            }
            else
            {
                await askForUsersDelyveryMW.Navigation.PushAsync(new CameraPaymmant(askForUsersDelyveryMW, ""));
            }
        }
    }
}