using MDispatch.NewElement;
using MDispatch.Vidget.VM;
using Xamarin.Forms.Xaml;

namespace MDispatch.Vidget.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : MDispatch.NewElement.CameraPage
    {
        private FullPhotoTruckVM fullPhotoTruckVM = null;

        public CameraPage(FullPhotoTruckVM fullPhotoTruckVM)
        {
            this.fullPhotoTruckVM = fullPhotoTruckVM;
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            if (!result.Success)
            {
                return;
            }
            fullPhotoTruckVM.AddPhoto(result.Image);
            await Navigation.PopAsync(true);
        }
    }
}