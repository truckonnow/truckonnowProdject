using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPagePhoto : ContentPage
    {
        private FullPagePhotoMV fullPagePhotoMV = null;
        private string pngPaternPhoto = null;

        public FullPagePhoto(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shipping, string pngPaternPhoto, string typeCar, int photoIndex)
        {
            this.pngPaternPhoto = pngPaternPhoto;
            fullPagePhotoMV = new FullPagePhotoMV(managerDispatchMob, vehiclwInformation, shipping, typeCar, photoIndex, Navigation);
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

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if(fullPagePhotoMV.AllSourseImage != null && fullPagePhotoMV.AllSourseImage.Count != 0)
            {
                fullPagePhotoMV.SavePhoto();
            }
            else
            {

            }
        }
    }
}