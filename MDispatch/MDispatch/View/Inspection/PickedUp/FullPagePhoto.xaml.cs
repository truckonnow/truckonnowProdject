using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPagePhoto : ContentPage
    {
        private FullPagePhotoMV fullPagePhotoMV = null;
        private string pngPaternPhoto = null;

        public FullPagePhoto(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, string pngPaternPhoto, string typeCar, int photoIndex, InitDasbordDelegate initDasbordDelegate)
        {
            this.pngPaternPhoto = pngPaternPhoto;
            fullPagePhotoMV = new FullPagePhotoMV(managerDispatchMob, vehiclwInformation, idShip, typeCar, photoIndex, Navigation, initDasbordDelegate);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = fullPagePhotoMV;
            paternPhoto.Source = pngPaternPhoto;
            if(fullPagePhotoMV.Car.typeIndex != null && fullPagePhotoMV.Car.typeIndex != "")
            {
                NameSelectPhoto.Text = $"{fullPagePhotoMV.Car.typeIndex} - {photoIndex}";
            }
            else
            {
                NameSelectPhoto.Text = "--------------------";
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraPagePhoto(fullPagePhotoMV, pngPaternPhoto));
        }

        private async void MessagesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            fullPagePhotoMV.SourseImage = (ImageSource)e.SelectedItem;
            if((ImageSource)Photos.SelectedItem != fullPagePhotoMV.AllSourseImage[0])
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
            if(fullPagePhotoMV.AllSourseImage != null && fullPagePhotoMV.AllSourseImage.Count != 0)
            {
                fullPagePhotoMV.SavePhoto();
            }
            else
            {
               // await PopupNavigation.
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageAddDamage(fullPagePhotoMV.SourseImage));
        }
    }
}