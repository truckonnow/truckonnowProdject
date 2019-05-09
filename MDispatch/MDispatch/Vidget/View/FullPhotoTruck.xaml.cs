using MDispatch.NewElement;
using MDispatch.Service;
using MDispatch.Vidget.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.VidgetFolder.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPhotoTruck : ContentPage
    {
        private FullPhotoTruckVM fullPhotoTruckVM = null;

        public FullPhotoTruck(ManagerDispatchMob managerDispatchMob, string idDriver, int indexCurrent, InitDasbordDelegate initDasbordDelegate = null)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            fullPhotoTruckVM = new FullPhotoTruckVM(managerDispatchMob, idDriver, indexCurrent, Navigation, initDasbordDelegate);
            BindingContext = fullPhotoTruckVM;
            fullPhotoTruckVM.Source = fullPhotoTruckVM.ImageSource;
        }

        protected override bool OnBackButtonPressed()
        {

            DependencyService.Get<IOrientationHandler>().ForceSensor();
            return base.OnBackButtonPressed();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            await Navigation.PopAsync();
        }

        private async void BtnAddPhoto_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Vidget.View.CameraPage(fullPhotoTruckVM));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {
            fullPhotoTruckVM.Source = fullPhotoTruckVM.ImageSourceTake;
        }

        private void TapGestureRecognizer_Tapped_2(object sender, System.EventArgs e)
        {
            fullPhotoTruckVM.Source = fullPhotoTruckVM.ImageSource;
        }
    }
}