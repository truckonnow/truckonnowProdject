using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.AskPhoto;
using MDispatch.ViewModels.InspectionMV.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.PageAppMV
{
    public class FullPagePhotoMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        public ICar Car = null;
        private InitDasbordDelegate initDasbordDelegate = null;

        public FullPagePhotoMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, string typeCar, int inderxPhotoInspektion, INavigation navigation, InitDasbordDelegate initDasbordDelegate)
        {
            Navigation = navigation;
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            VehiclwInformation = vehiclwInformation;
            InderxPhotoInspektion = inderxPhotoInspektion;
            if (typeCar != null)
            {
                Car = GetTypeCar(typeCar);
            }
            IdShip = idShip;
        }

        public string IdShip { get; set; }

        private int inderxPhotoInspektion = 0;
        public int InderxPhotoInspektion
        {
            get => inderxPhotoInspektion;
            set => SetProperty(ref inderxPhotoInspektion, value);
        }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        private ImageSource sourseImage = null;
        public ImageSource SourseImage
        {
            get => sourseImage;
            set => SetProperty(ref sourseImage, value);
        }

        private List<ImageSource> allSourseImage = null;
        public List<ImageSource> AllSourseImage
        {
            get => allSourseImage;
            set => SetProperty(ref allSourseImage, value);
        }


        private PhotoInspection photoInspection = null;
        public PhotoInspection PhotoInspection
        {
            get => photoInspection;
            set => SetProperty(ref photoInspection, value);
        }

        private ICar GetTypeCar(string typeCar)
        {
            ICar car = null;
            switch(typeCar)
            {
                case "PickUp":
                    {
                        car = new CarPickUp();
                        break;
                    }
            }
            return car;
        }

        public void AddNewFotoSourse(byte[] imageSorseByte)
        {
            if (AllSourseImage == null)
            {
                AllSourseImage = new List<ImageSource>();
            }
            List<ImageSource> imageSources1 = new List<ImageSource>(AllSourseImage);
            imageSources1.Add(ImageSource.FromStream(() => new MemoryStream(imageSorseByte)));
            AllSourseImage = imageSources1;
        }

        public async void SetPhoto(byte[] PhotoInArrayByte)
        {
            if (PhotoInspection == null)
            {
                PhotoInspection = new PhotoInspection();
            }
            if (PhotoInspection.Photos == null)
            {
                PhotoInspection.Photos = new List<Models.Photo>();
            }
            PhotoInspection.IndexPhoto = InderxPhotoInspektion;
            Models.Photo photo = new Models.Photo();
            string photoJson = JsonConvert.SerializeObject(PhotoInArrayByte);
            string pathIndePhoto = PhotoInspection.Photos.Count == 0 ? PhotoInspection.IndexPhoto.ToString() : $"{PhotoInspection.IndexPhoto}.{PhotoInspection.Photos.Count}"; ;
            photo.Base64 = photoJson;
            photo.path = $"Photo/{VehiclwInformation.Id}/PikedUp/PhotoInspection/{pathIndePhoto}.Jpeg";
            PhotoInspection.Photos.Add(photo);
        }

        public async void SavePhoto()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.AskWork("SavePhoto", token, VehiclwInformation.Id, PhotoInspection, ref description);
                initDasbordDelegate.Invoke();
            });
            await PopupNavigation.PopAsync(true);
            if (state == 1)
            {
                //FeedBack = "Not Network";
            }
            else if (state == 2)
            {
                //FeedBack = description;
            }
            else if (state == 3)
            {
                if (InderxPhotoInspektion < 39)
                {
                    await Navigation.PushAsync(new FullPagePhoto(managerDispatchMob, VehiclwInformation, IdShip, $"{Car.typeIndex}{InderxPhotoInspektion + 1}.png", Car.typeIndex, InderxPhotoInspektion + 1, initDasbordDelegate));
                    Navigation.RemovePage(Navigation.NavigationStack[2]);
                }
                else
                {
                    await PopupNavigation.PushAsync(new TempPageHint());
                    await Navigation.PushAsync(new Ask1Page(managerDispatchMob, VehiclwInformation, IdShip, initDasbordDelegate), true);
                    Navigation.RemovePage(Navigation.NavigationStack[2]);
                }
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}