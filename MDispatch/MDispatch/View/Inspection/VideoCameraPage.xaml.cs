using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using MDispatch.ViewModels.InspectionMV.Servise.Paymmant;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoCameraPage : NewElement.CustomVideoCam.VideoCameraPage
    {
        private object paymmant = null;

        public VideoCameraPage(object paymmant, string instructionAndNamePaymmant)
        {
            this.paymmant = paymmant;
            InitializeComponent();
            On<iOS>().SetPrefersStatusBarHidden(StatusBarHiddenMode.True)
                .SetPreferredStatusBarUpdateAnimation(UIStatusBarAnimation.Fade);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }

        [Obsolete]
        private async void VideoCameraPage_OnPhotoResult(NewElement.PhotoResultEventArgs result)
        {
            if (!result.Success)
                return;
            if (paymmant is AskForUsersDelyveryMW)
            {
                ((AskForUsersDelyveryMW)paymmant).VideoRecount = new Models.Video()
                {
                    path = $"../Video/{((AskForUsersDelyveryMW)paymmant).VehiclwInformation.Id}/RecountPay.mp4",
                    VideoBase64 = Convert.ToBase64String(result.Result)
                };
                await ((AskForUsersDelyveryMW)paymmant).SaveRecountVideo();
            }
            else
            {
                ((LiabilityAndInsuranceMV)paymmant).VideoRecount = new Models.Video()
                {
                    path = $"../Video/{((LiabilityAndInsuranceMV)paymmant).IdVech}/RecountPay.mp4",
                    VideoBase64 = Convert.ToBase64String(result.Result)
                };
                await ((LiabilityAndInsuranceMV)paymmant).SaveRecountVideo();
            }
        }
    }
}