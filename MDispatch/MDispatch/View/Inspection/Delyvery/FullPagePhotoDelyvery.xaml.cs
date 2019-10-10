using MDispatch.Models;
using MDispatch.NewElement;
using MDispatch.NewElement.ResIzeImage;
using MDispatch.Service;
using MDispatch.View.Inspection;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using MDispatch.ViewModels.InspectionMV.Servise.Retake;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPagePhotoDelyvery : ContentPage
    {
        public FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV = null;
        private string pngPaternPhoto = null;

        public FullPagePhotoDelyvery(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, string pngPaternPhoto,
            string typeCar, int photoIndex, InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate, string nameLayoute, 
            string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            this.pngPaternPhoto = pngPaternPhoto;
            fullPagePhotoDelyveryMV = new FullPagePhotoDelyveryMV(managerDispatchMob, vehiclwInformation, idShip, typeCar, photoIndex, Navigation, initDasbordDelegate, getVechicleDelegate, onDeliveryToCarrier, totalPaymentToCarrier);
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);  
            BindingContext = fullPagePhotoDelyveryMV;
            paternPhoto.Source = pngPaternPhoto;
            dmla.IsVisible = false;
            if (fullPagePhotoDelyveryMV.Car.typeIndex != null && fullPagePhotoDelyveryMV.Car.typeIndex != "")
            {
                NameSelectPhoto.Text = $"{nameLayoute} - {photoIndex}/{fullPagePhotoDelyveryMV.Car.CountCarImg}";
            }
            else
            {
                NameSelectPhoto.Text = "--------------------";
            }
        }

        public void SetbtnVisable()
        {
            if (fullPagePhotoDelyveryMV.AllSourseImage != null && fullPagePhotoDelyveryMV.AllSourseImage.Count != 0)
            {
                Photos.SelectedItem = fullPagePhotoDelyveryMV.AllSourseImage[0];
                btnNext.HorizontalOptions = LayoutOptions.End;
                btnNext.IsVisible = true;
                btnAddPhoto.IsVisible = false;
                btnDamage.IsVisible = true;
                btnRetake.IsVisible = true;
            }
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraPagePhoto1(pngPaternPhoto, this));
        }

        private async void MessagesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            fullPagePhotoDelyveryMV.SourseImage = (ImageSource)e.SelectedItem;
            if((ImageSource)Photos.SelectedItem != fullPagePhotoDelyveryMV.AllSourseImage[0])
            {
                dmla.IsVisible = false;
                paternPhoto.Source = "";
                btnDamage.IsVisible = false;
            }
            else
            {
                btnDamage.IsVisible = true;
                dmla.IsVisible = true;
                paternPhoto.Source = pngPaternPhoto;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if(fullPagePhotoDelyveryMV.AllSourseImage != null && fullPagePhotoDelyveryMV.AllSourseImage.Count != 0)
            {
                fullPagePhotoDelyveryMV.SavePhoto();
            }
            else
            {
               // await PopupNavigation.
            }
        }

        internal void AddDamagCurrentLayut(Xamarin.Forms.View view)
        {
            ((ImgResize)view).OneTabAction += SelectImageSourse;
            dmla.Children.Add(view);
        }

        private void SelectImageSourse(object sender)
        {
            ImageSource imageSource = fullPagePhotoDelyveryMV.SelectPhotoForDamage((Image)sender);
            if (imageSource != null)
            {
                Photos.SelectedItem = imageSource;
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            if (fullPagePhotoDelyveryMV.PhotoInspection != null && fullPagePhotoDelyveryMV.AllSourseImage.FindIndex(a => a == fullPagePhotoDelyveryMV.SourseImage) == 0)
            {
                await Navigation.PushAsync(new Inspection.PickedUp.PageAddDamage1(fullPagePhotoDelyveryMV, this, dmla.Children.ToList()));
            }
        }

        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            return base.OnBackButtonPressed();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IOrientationHandler>().ForceSensor();
            await Navigation.PopAsync();
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            RetakeFullPageDelivery retakeFullPageDelivery = null;
            StreamImageSource streamImageSource = (StreamImageSource)fullPagePhotoDelyveryMV.SourseImage;
            System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            byte[] bytes = ms.ToArray();
            retakeFullPageDelivery = new RetakeFullPageDelivery(fullPagePhotoDelyveryMV, bytes);
            await Navigation.PushAsync(new RetakePage(retakeFullPageDelivery));
        }
    }
}