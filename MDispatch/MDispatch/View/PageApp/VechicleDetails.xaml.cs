using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.PageAppMV.VehicleDetals;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VechicleDetails : ContentPage
    {
        VehicleDetailsMV vehicleDetailsMV = null;

        public VechicleDetails(VehiclwInformation vehiclwInformation, ManagerDispatchMob managerDispatchMob)
        {
            vehicleDetailsMV = new VehicleDetailsMV(managerDispatchMob, Convert.ToInt32(vehiclwInformation.Id), this) { Navigationn = this.Navigation };
            InitializeComponent();
            BindingContext = vehicleDetailsMV;
        }

        public async Task InitPhoto(VehiclwInformation vehiclwInformation)
        {
            if (vehiclwInformation.Ask != null)
            {
                AddBlocDocumentPhoto(vehiclwInformation);
                AddBlocItemPhoto(vehiclwInformation);
            }
            if (vehiclwInformation.Ask1 != null)
            {
                AddBlocBeen(vehiclwInformation);
                AddBlocDocumentationBeen(vehiclwInformation);
                AddBlocSeatBelts(vehiclwInformation);
                AddBlocTakePictures(vehiclwInformation);
            }
            if(vehiclwInformation.PhotoInspections != null)
            {
                AddBlocInspectionPhoto(vehiclwInformation);
            }
        }

        private void AddBlocDocumentPhoto(VehiclwInformation vehiclwInformation)
        {
            foreach (var document in vehiclwInformation.Ask.Any_paperwork_or_documentation)
            {
                blockDocument.Children.Add(new Image()
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(document.Base64))),
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = 3
                });
            }
        }

        private void AddBlocItemPhoto(VehiclwInformation vehiclwInformation)
        {
            foreach (var item in vehiclwInformation.Ask.Any_personal_or_additional_items_with_or_in_vehicle)
            {
                blockItems.Children.Add(new Image()
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Base64))),
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = 3
                });
            }
        }

        private void AddBlocBeen(VehiclwInformation vehiclwInformation)
        {
            foreach (var item in vehiclwInformation.Ask1.Any_additional_parts_been_given_to_you)
            {
                blockBeen.Children.Add(new Image()
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Base64))),
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = 3
                });
            }
        }

        private void AddBlocDocumentationBeen(VehiclwInformation vehiclwInformation)
        {
            foreach (var item in vehiclwInformation.Ask1.Any_additional_documentation_been_given_after_loading)
            {
                blockDocumentationBeen.Children.Add(new Image()
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Base64))),
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = 3
                });
            }
        }

        private void AddBlocSeatBelts(VehiclwInformation vehiclwInformation)
        {
            foreach (var item in vehiclwInformation.Ask1.App_will_force_driver_to_take_pictures_of_each_strap)
            {
                blockSeatBelts.Children.Add(new Image()
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Base64))),
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = 3
                });
            }
        }

        private void AddBlocTakePictures(VehiclwInformation vehiclwInformation)
        {
            foreach (var item in vehiclwInformation.Ask1.Photo_after_loading_in_the_truck)
            {
                blockTakePictures.Children.Add(new Image()
                {
                    Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Base64))),
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Margin = 3
                });
            }
        }

        private void AddBlocInspectionPhoto(VehiclwInformation vehiclwInformation)
        {
            foreach (var item in vehiclwInformation.PhotoInspections)
            {
                if (item.CurrentStatusPhoto == "PikedUp")
                {
                    foreach (var photo in item.Photos)
                    {
                        blockPhotoInspection.Children.Add(new Image()
                        {
                            Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(photo.Base64))),
                            HeightRequest = 70,
                            WidthRequest = 70,
                            Margin = 3
                        });
                    }
                }
            }
        }
    }
}