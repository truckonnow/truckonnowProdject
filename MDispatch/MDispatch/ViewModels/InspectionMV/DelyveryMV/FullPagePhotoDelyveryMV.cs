using MDispatch.Models;
using MDispatch.NewElement;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.Delyvery;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.AskPhoto;
using MDispatch.ViewModels.InspectionMV.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV.DelyveryMV
{
    public class FullPagePhotoDelyveryMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        public ICar Car = null;
        private InitDasbordDelegate initDasbordDelegate = null;
        private GetVechicleDelegate getVechicleDelegate = null;

        public FullPagePhotoDelyveryMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, string typeCar, 
            int inderxPhotoInspektion, INavigation navigation, InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate)
        {
            this.getVechicleDelegate = getVechicleDelegate;
            Navigation = navigation;
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            VehiclwInformation = vehiclwInformation;
            InderxPhotoInspektion = inderxPhotoInspektion;
            if (typeCar != null)
            {
                Car = GetTypeCar(typeCar);
                Car.OrintableScreen(inderxPhotoInspektion);
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

        internal void SetDamage(string nameDamage, int indexDamage, string prefNameDamage, double xInterest, double yInterest, Image image, ImageSource imageSource1)
        {
            Damage damage = new Damage();
            damage.FullNameDamage = $"{prefNameDamage} - {nameDamage}";
            damage.IndexImageVech = InderxPhotoInspektion.ToString();
            damage.TypeDamage = nameDamage;
            damage.TypePrefDamage = prefNameDamage;
            damage.IndexDamage = indexDamage;
            damage.XInterest = xInterest;
            damage.YInterest = yInterest;
            damage.Image = image;
            damage.TypeCurrentStatus = "D";
            damage.ImageSource = imageSource1;
            if (PhotoInspection.Damages == null)
            {
                PhotoInspection.Damages = new List<Damage>();
            }
            PhotoInspection.Damages.Add(damage);
        }

        public void RemmoveDamage(Image image)
        {
            if (image != null && PhotoInspection.Damages != null && PhotoInspection.Damages.FirstOrDefault(d => d.Image == image) != null)
            {
                List<ImageSource> imageSources2 = new List<ImageSource>(AllSourseImage);
                Damage damage = PhotoInspection.Damages.FirstOrDefault(d => d.Image == image);
                imageSources2.Remove(imageSources2.FirstOrDefault(i => i == damage.ImageSource));
                AllSourseImage = imageSources2;
                PhotoInspection.Damages.Remove(damage);
            }
        }

        public ImageSource SelectPhotoForDamage(Image image)
        {
            if (PhotoInspection != null && PhotoInspection.Damages != null)
            {
                Damage damage1 = PhotoInspection.Damages.FirstOrDefault(d => d.Image == image);
                SourseImage = damage1.ImageSource;
                return damage1.ImageSource;
            }
            return null;
        }

        private List<Damage> damages = null;
        public List<Damage> Damages
        {
            get => damages;
            set => SetProperty(ref damages, value);
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
                case "Coupe":
                    {
                        car = new CarCoupe();
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
                PhotoInspection.Photos = new List<Photo>();
            }
            PhotoInspection.IndexPhoto = InderxPhotoInspektion;
            PhotoInspection.CurrentStatusPhoto = "Delyvery";
            Photo photo = new Photo();
            string photoJson = JsonConvert.SerializeObject(PhotoInArrayByte);
            string pathIndePhoto = PhotoInspection.Photos.Count == 0 ? PhotoInspection.IndexPhoto.ToString() : $"{PhotoInspection.IndexPhoto}.{PhotoInspection.Photos.Count}";
            PhotoInspection.CurrentStatusPhoto = "Delyvery";
            photo.Base64 = photoJson;
            photo.path = $"../Photo/{VehiclwInformation.Id}/Delyvery/PhotoInspection/{pathIndePhoto}.Jpeg";
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
                if(InderxPhotoInspektion == 7)
                {
                    await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, VehiclwInformation, IdShip, $"{Car.typeIndex}{19}.png", Car.typeIndex, 19, initDasbordDelegate, getVechicleDelegate));
                }
                else if (InderxPhotoInspektion == 19)
                {
                    await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, VehiclwInformation, IdShip, $"{Car.typeIndex}{2}.png", Car.typeIndex, 2, initDasbordDelegate, getVechicleDelegate));
                }
                else if (InderxPhotoInspektion == 2)
                {
                    await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, VehiclwInformation, IdShip, $"{Car.typeIndex}{16}.png", Car.typeIndex, 16, initDasbordDelegate, getVechicleDelegate));
                }
                else if (InderxPhotoInspektion == 16)
                {
                    await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, VehiclwInformation, IdShip, $"{Car.typeIndex}{23}.png", Car.typeIndex, 23, initDasbordDelegate, getVechicleDelegate));
                }
                else if (InderxPhotoInspektion == 23)
                {
                    await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, VehiclwInformation, IdShip, $"{Car.typeIndex}{20}.png", Car.typeIndex, 20, initDasbordDelegate, getVechicleDelegate));
                }
                else if (InderxPhotoInspektion == 20)
                {
                    CheckVechicleAndGoToResultPage();
                }
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }

        private async void CheckVechicleAndGoToResultPage()
        {
            List<VehiclwInformation> vehiclwInformation1s = getVechicleDelegate.Invoke();
            int indexCurrentVechecle = vehiclwInformation1s.FindIndex(v => v == VehiclwInformation);
            if (vehiclwInformation1s.Count - 1 == indexCurrentVechecle)
            {
                DependencyService.Get<IOrientationHandler>().ForceSensor();
                await PopupNavigation.PushAsync(new TempDialogPage());
                await Navigation.PushAsync(new AskForUserDelyvery(managerDispatchMob, VehiclwInformation, IdShip, initDasbordDelegate));
            }
            else
            {
                await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation1s[indexCurrentVechecle + 1]));
                await Navigation.PushAsync(new AskPageDelyvery(managerDispatchMob, vehiclwInformation, IdShip, initDasbordDelegate, getVechicleDelegate), true);
                Navigation.RemovePage(Navigation.NavigationStack[2]);
            }
        }
    }
}