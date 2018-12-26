using MDispatch.NewElement;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.CameraPageFolder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CameraDocumentations : CameraPage
    {
        Ask1Page ask1Page = null;

        public CameraDocumentations (Ask1Page ask1Page)
        {
            this.ask1Page = ask1Page;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            await Navigation.PopAsync(true);
            if (!result.Success)
                return;
            ask1Page.AddPhotoDocumentations(result.Image);
        }
    }
}