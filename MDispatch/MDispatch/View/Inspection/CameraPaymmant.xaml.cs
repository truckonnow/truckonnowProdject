using MDispatch.NewElement;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPaymmant : CameraPage
    {
        private object paymmant = null;

        public CameraPaymmant(object paymmant, string instructionAndNamePaymmant)
        {
            this.paymmant = paymmant;
            InitializeComponent();
            NamaPayment.Text = instructionAndNamePaymmant;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void CameraPage_OnPhotoResult(NewElement.PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            if(paymmant is AskForUsersDelyveryMW)
            {
                ((AskForUsersDelyveryMW)paymmant).AddPhoto(result.Image);
            }
            else
            {
                ((LiabilityAndInsuranceMV)paymmant).AddPhoto(result.Image);
            }
        }
    }
}