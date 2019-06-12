using MDispatch.NewElement;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.AskPhoto.CameraPageFolder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraItems : CameraPage
    {
        AskPage askPage = null;

        public CameraItems (AskPage askPage)
		{
            this.askPage = askPage;
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            askPage.AddPhotoItems(result.Image);
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}