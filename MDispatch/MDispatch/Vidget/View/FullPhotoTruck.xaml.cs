using MDispatch.NewElement;
using MDispatch.Service;
using MDispatch.Vidget.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.VidgetFolder.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPhotoTruck : ContentPage
    {
        private FullPhotoTruckVM fullPhotoTruckVM = null;

        public FullPhotoTruck(ManagerDispatchMob managerDispatchMob, string idDriver)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            fullPhotoTruckVM = new FullPhotoTruckVM(managerDispatchMob, Navigation, idDriver);
            BindingContext = fullPhotoTruckVM;
            SelectPhoto.Source = fullPhotoTruckVM.ImageSource;
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
    }
}