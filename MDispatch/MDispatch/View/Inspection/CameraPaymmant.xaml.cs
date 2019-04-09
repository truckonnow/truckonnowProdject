using MDispatch.NewElement;
using MDispatch.ViewModels.InspectionMV.Servise.Paymmant;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPaymmant : CameraPage
    {
        private IPaymmant paymmant = null;

        public CameraPaymmant(IPaymmant paymmant, string instructionAndNamePaymmant)
        {
            this.paymmant = paymmant;
            InitializeComponent();
            NamaPayment.Text = instructionAndNamePaymmant;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void CameraPage_OnPhotoResult(NewElement.PhotoResultEventArgs result)
        {
            await Navigation.PopAsync();
            if (!result.Success)
                return;
            //paymmant.AddPhoto(result.Image);
        }
    }
}