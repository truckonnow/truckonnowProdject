using MDispatch.NewElement;
using MDispatch.View.Inspection.PickedUp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.Delyvery.CameraPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraAdditionalPhoto : NewElement.CameraPage
    {
        private AskForUserDelyvery askForUserDelyvery = null;
        private PageAddDamageFoUser pageAddDamageFoUser = null;

        public CameraAdditionalPhoto (AskForUserDelyvery askForUserDelyvery, PageAddDamageFoUser pageAddDamageFoUser)
		{
            this.pageAddDamageFoUser = pageAddDamageFoUser;
            this.askForUserDelyvery = askForUserDelyvery;
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            DependencyService.Get<IOrientationHandler>().ForceLandscape();
        }

        private async void CameraPage_OnPhotoResult(NewElement.PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            askForUserDelyvery.AddPhotoAdditional(result.Image);
            pageAddDamageFoUser.stateSelect = 0;
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            await Navigation.PopAsync(true);
        }

        protected override bool OnBackButtonPressed()
        {
            pageAddDamageFoUser.stateSelect = 0;
            OnBackButtonPressedAsync();
            return base.OnBackButtonPressed();
        }

        private async void OnBackButtonPressedAsync()
        {
            await Navigation.PopAsync(true);
        }
    }
}