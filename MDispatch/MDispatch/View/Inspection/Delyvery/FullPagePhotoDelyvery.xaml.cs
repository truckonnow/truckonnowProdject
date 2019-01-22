using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPagePhotoDelyvery : ContentPage
    {
        private FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV = null;
        private string pngPaternPhoto = null;

        public FullPagePhotoDelyvery(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, string pngPaternPhoto, string typeCar, int photoIndex, InitDasbordDelegate initDasbordDelegate)
        {
            this.pngPaternPhoto = pngPaternPhoto;
            fullPagePhotoDelyveryMV = new FullPagePhotoDelyveryMV(managerDispatchMob, vehiclwInformation, idShip, typeCar, photoIndex, Navigation, initDasbordDelegate);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = fullPagePhotoDelyveryMV;
            paternPhoto.Source = pngPaternPhoto;
            if(fullPagePhotoDelyveryMV.Car.typeIndex != null && fullPagePhotoDelyveryMV.Car.typeIndex != "")
            {
                NameSelectPhoto.Text = $"{fullPagePhotoDelyveryMV.Car.typeIndex} - {photoIndex}";
            }
            else
            {
                NameSelectPhoto.Text = "--------------------";
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraPagePhoto1(fullPagePhotoDelyveryMV, pngPaternPhoto));
        }

        private async void MessagesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            fullPagePhotoDelyveryMV.SourseImage = (ImageSource)e.SelectedItem;
            if((ImageSource)Photos.SelectedItem != fullPagePhotoDelyveryMV.AllSourseImage[0])
            {
                paternPhoto.Source = "";
            }
            else
            {
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
    }
}