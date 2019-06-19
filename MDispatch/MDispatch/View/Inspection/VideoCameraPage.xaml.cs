using MDispatch.ViewModels.InspectionMV.Servise.Paymmant;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCameraPage : NewElement.CustomVideoCam.VideoCameraPage
    {
        private CashPaymmant cashPaymmant = null;

        public VideoCameraPage(CashPaymmant cashPaymmant)
        {
            this.cashPaymmant = cashPaymmant;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void VideoCameraPage_OnPhotoResult(NewElement.PhotoResultEventArgs result)
        {
            await Navigation.PopAsync();
            if (!result.Success)
                return;
            cashPaymmant.SaveVidopRecount(result.Result);
        }
    }
}