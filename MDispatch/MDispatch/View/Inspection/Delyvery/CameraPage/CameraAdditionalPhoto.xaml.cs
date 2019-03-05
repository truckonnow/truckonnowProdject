using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.Delyvery.CameraPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraAdditionalPhoto : NewElement.CameraPage
    {
        private AskForUserDelyvery askForUserDelyvery = null;
        private int countPhoto = 0;

        public CameraAdditionalPhoto (AskForUserDelyvery askForUserDelyvery)
		{
            this.askForUserDelyvery = askForUserDelyvery;
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void CameraPage_OnPhotoResult(NewElement.PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            askForUserDelyvery.AddPhotoAdditional(result.Image);
            if(countPhoto == 10)
            {
                await Navigation.PopAsync();
                return;
            }
            countPhoto++;
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}