using MDispatch.NewElement;
using MDispatch.ViewModels.InspectionMV.Servise.Retake;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RetakePage : CameraPage
    {
        private IRetake retake = null;

        public RetakePage(IRetake retake)
        {
            this.retake = retake;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            await Navigation.PopAsync();
            if (!result.Success)
                return;
            retake.SetRetakePhoto(result.Result);
        }
    }
}