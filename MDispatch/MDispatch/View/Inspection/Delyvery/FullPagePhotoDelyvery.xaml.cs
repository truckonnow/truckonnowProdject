using MDispatch.Models;
using MDispatch.NewElement;
using MDispatch.Service;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using System;
using System.Linq;
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

        public FullPagePhotoDelyvery(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, string pngPaternPhoto,
            string typeCar, int photoIndex, InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate, string nameLayoute)
        {
            this.pngPaternPhoto = pngPaternPhoto;
            fullPagePhotoDelyveryMV = new FullPagePhotoDelyveryMV(managerDispatchMob, vehiclwInformation, idShip, typeCar, photoIndex, Navigation, initDasbordDelegate, getVechicleDelegate);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = fullPagePhotoDelyveryMV;
            paternPhoto.Source = pngPaternPhoto;
            dmla.IsVisible = false;
            if (fullPagePhotoDelyveryMV.Car.typeIndex != null && fullPagePhotoDelyveryMV.Car.typeIndex != "")
            {
                NameSelectPhoto.Text = $"{nameLayoute} - {photoIndex}";
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
                btnNext.IsVisible = true;
                btnAddPhoto.IsVisible = false;
            }
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraPagePhoto1(fullPagePhotoDelyveryMV, pngPaternPhoto, this));
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
            view.GestureRecognizers.Add(new TapGestureRecognizer(SelectImageSourse));
            dmla.Children.Add(view);
        }

        public void SelectImageSourse(Xamarin.Forms.View v, object e)
        {
            ImageSource imageSource = fullPagePhotoDelyveryMV.SelectPhotoForDamage((Image)v);
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
            await Navigation.PopAsync();
        }
    }
}