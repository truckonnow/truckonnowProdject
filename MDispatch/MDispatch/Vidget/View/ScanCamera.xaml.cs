using MDispatch.Vidget.VM;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.Vidget.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanCamera : MDispatch.NewElement.CameraPage
    {
        private FullPhotoTruckVM fullPhotoTruckVM = null;
        private string typeDetect = null;

        public ScanCamera(FullPhotoTruckVM fullPhotoTruckVM, string typeDetect)
        {
            this.typeDetect = typeDetect;
            this.fullPhotoTruckVM = fullPhotoTruckVM;
            InitializeComponent();
        }

        private async void CameraPage_OnScan(NewElement.PhotoResultEventArgs result)
        {
            if (!result.Success)
            {
                return;
            }
            fullPhotoTruckVM.DetectText(result.Result, typeDetect);
            await Navigation.PopAsync();
            if (fullPhotoTruckVM.IndexCurent == 44)
            {
                await PopupNavigation.PushAsync(new PlateWrite(fullPhotoTruckVM));
            }
            else if(typeDetect == "truck")
            {
                await PopupNavigation.PushAsync(new PlateTruckWrite(fullPhotoTruckVM));
            }
            else if (typeDetect == "trailer")
            {
                //await PopupNavigation.PushAsync(new PlateTruckWrite(fullPhotoTruckVM));
            }
            //fullPhotoTruckVM
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            if (!((fullPhotoTruckVM.PlateTruck != null && fullPhotoTruckVM.PlateTruck != "") && (fullPhotoTruckVM.PlateTrailer != null && fullPhotoTruckVM.PlateTrailer != "")) && fullPhotoTruckVM.IndexCurent == 44)
            {
                await PopupNavigation.PushAsync(new PlateWrite(fullPhotoTruckVM));
            }
            else if (fullPhotoTruckVM.PlateTruck == null || fullPhotoTruckVM.PlateTruck == "")
            {
                await PopupNavigation.PushAsync(new PlateTruckWrite(fullPhotoTruckVM));
            }
            else if (fullPhotoTruckVM.PlateTrailer == null || fullPhotoTruckVM.PlateTrailer == "")
            {
                //await PopupNavigation.PushAsync(new PlateTruckWrite(this));
            }
        }
    }
}