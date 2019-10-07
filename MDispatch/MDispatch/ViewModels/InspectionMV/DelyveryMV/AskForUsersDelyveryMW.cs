using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.AskPhoto;
using MDispatch.ViewModels.InspectionMV.Models;
using MDispatch.ViewModels.InspectionMV.Servise.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV.DelyveryMV
{
    public class AskForUsersDelyveryMW : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;
        private GetShiping getShiping = null; 
        private GetVechicleDelegate getVechicleDelegate = null;
        public DelegateCommand GoToFeedBackCommand { get; set; }

        public AskForUsersDelyveryMW(ManagerDispatchMob managerDispatchMob, string idShip, INavigation navigation, GetShiping getShiping, InitDasbordDelegate initDasbordDelegate,
            GetVechicleDelegate getVechicleDelegate, VehiclwInformation vehiclwInformation, string totalPaymentToCarrier, string paymmant = null)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.getVechicleDelegate = getVechicleDelegate;
            this.getShiping = getShiping;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            IdShip = idShip;
            TotalPaymentToCarrier = totalPaymentToCarrier;
            GoToFeedBackCommand = new DelegateCommand(GoToFeedBack);
            if (paymmant != null)
            {
                Payment = paymmant;
            }
        }

        public string IdShip { get; set; }
        private string TotalPaymentToCarrier { get; set; }
        public string Payment { get; set; }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        private AskForUserDelyveryM askForUserDelyveryM = null;
        public AskForUserDelyveryM AskForUserDelyveryM
        {
            get => askForUserDelyveryM;
            set => SetProperty(ref askForUserDelyveryM, value);
        }

        private string email = null;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private int inderxPhotoInspektion = 0;
        public int InderxPhotoInspektion
        {
            get => inderxPhotoInspektion;
            set => SetProperty(ref inderxPhotoInspektion, value);
        }

        private Video videoRecount = null;
        public Video VideoRecount
        {
            get => videoRecount;
            set => SetProperty(ref videoRecount, value);
        }

        public List<DamageForUser> damageForUsers { get; set; }

        [System.Obsolete]
        public async void SaveAsk(string paymmant)
        {
            bool isPaymantPhoto = false;
            bool isNavigationMany = false;
            if (Navigation.NavigationStack.Count > 2)
            {
                await PopupNavigation.PushAsync(new LoadPage());
                isNavigationMany = true;
            }
            Payment = paymmant;
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            //await PopupNavigation.PushAsync(new TempDialogPage1(this));
            if (Payment == "COD" || Payment == "COP" || Payment == "Biling")
            {
                ICar Car = GetTypeCar(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                FullPagePhotoDelyvery fullPagePhotoDelyvery = new FullPagePhotoDelyvery(managerDispatchMob, VehiclwInformation, IdShip, $"{Car.typeIndex.Replace(" ", "")}{InderxPhotoInspektion + 1}.png", Car.typeIndex.Replace(" ", ""), inderxPhotoInspektion + 1, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(Car.GetIndexCarFullPhoto(inderxPhotoInspektion + 1)), Payment, TotalPaymentToCarrier);
                await Navigation.PushAsync(fullPagePhotoDelyvery);
                await Navigation.PushAsync(new CameraPagePhoto1($"{Car.typeIndex.Replace(" ", "")}{InderxPhotoInspektion + 1}.png", fullPagePhotoDelyvery));
                isPaymantPhoto = true;
            }
            else
            {
                await Navigation.PushAsync(new CameraPaymmant(this, ""));
            }
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                Task.Run(async () => await SaveRecountVideo());
                await Task.Run(() =>
                {
                    state = managerDispatchMob.AskWork("AskForUserDelyvery", token, IdShip, AskForUserDelyveryM, ref description);
                    initDasbordDelegate.Invoke();
                });
                if (state == 2)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (Navigation.NavigationStack.Count > 1)
                    {
                        await Navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror(description, Navigation));
                }
                else if (state == 3)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (isPaymantPhoto)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[1]);
                    }
                    DependencyService.Get<IToast>().ShowMessage("Answers to questions saved");
                }
                else if (state == 4)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (Navigation.NavigationStack.Count > 1)
                    {
                        await Navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
                }
            }
        }

        private ICar GetTypeCar(string typeCar)
        {
            ICar car = null;
            switch (typeCar)
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
                case "Suv":
                    {
                        car = new CarSuv();
                        break;
                    }
                case "Sedan":
                    {
                        car = new CarSedan();
                        break;
                    }
            }
            return car;
        }

        [System.Obsolete]
        public async Task SaveRecountVideo()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                if (videoRecount != null)
                {
                    state = managerDispatchMob.SavePay("SaveRecount", token, IdShip, 2, videoRecount, ref description);
                }
                if (state == 2)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.PushAsync(new Errror(description, Navigation));
                    });
                }
                else if (state == 3)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        DependencyService.Get<IToast>().ShowMessage("Video capture saved successfully");
                    });
                }
                else if (state == 4)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
                    });
                }
            }
        }

        [System.Obsolete]
        public async void AddPhoto(byte[] photoResult)
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            Photo photo = new Photo();
            photo.Base64 = JsonConvert.SerializeObject(photoResult);
            photo.path = $"../Photo/{VehiclwInformation.Id}/Pay/DelyverySig.jpg";
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.SavePay("SaveSig", token, IdShip, 2, photo, ref description);
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, Navigation));
                }
                else if (state == 3)
                {
                    DependencyService.Get<IToast>().ShowMessage("Payment method photo saved");
                    ICar Car = GetTypeCar(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                    FullPagePhotoDelyvery fullPagePhotoDelyvery = new FullPagePhotoDelyvery(managerDispatchMob, VehiclwInformation, IdShip, $"{Car.typeIndex.Replace(" ", "")}{InderxPhotoInspektion + 1}.png", Car.typeIndex.Replace(" ", ""), inderxPhotoInspektion + 1, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(Car.GetIndexCarFullPhoto(inderxPhotoInspektion + 1)), Payment, TotalPaymentToCarrier);
                    await Navigation.PushAsync(fullPagePhotoDelyvery);
                    await Navigation.PushAsync(new CameraPagePhoto1($"{Car.typeIndex.Replace(" ", "")}{InderxPhotoInspektion + 1}.png", fullPagePhotoDelyvery));
                    Navigation.RemovePage(Navigation.NavigationStack[1]);
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
                }
            }
            await PopupNavigation.PopAsync();
        }

        public void RemmoveDamage(Image image, StackLayout stackLayout)
        {
            if (image != null && damageForUsers != null && damageForUsers.FirstOrDefault(d => d.Image == image) != null)
            {
                List<ImageSource> AllSourseImage = new List<ImageSource>();
                stackLayout.Children.ToList().ForEach((imageV) => 
                {
                    Image tempImage = (Image)imageV;
                    AllSourseImage.Add(tempImage.Source);
                });
                List<ImageSource> imageSources2 = new List<ImageSource>(AllSourseImage);
                DamageForUser damageForUser = damageForUsers.FirstOrDefault(d => d.Image == image);
                imageSources2.Remove(imageSources2.FirstOrDefault(i => i == damageForUser.ImageSource));
                AllSourseImage = imageSources2;
                damageForUsers.Remove(damageForUser);
                stackLayout.Children.Remove(image);
            }
        }

        internal void SetDamage(string nameDamage, int indexDamage, string prefNameDamage, double xInterest, double yInterest, Image image, ImageSource imageSource1)
        {
            DamageForUser damageForUser = new DamageForUser();
            damageForUser.FullNameDamage = $"{prefNameDamage} - {nameDamage}";
            damageForUser.TypePrefDamage = prefNameDamage;
            damageForUser.IndexDamage = indexDamage;
            damageForUser.XInterest = xInterest;
            damageForUser.YInterest = yInterest;
            damageForUser.Image = image;
            damageForUser.TypeCurrentStatus = "D";
            damageForUser.ImageSource = imageSource1;
            if (damageForUsers == null)
            {
                damageForUsers = new List<DamageForUser>();
            }
            damageForUsers.Add(damageForUser);
        }

        

        [System.Obsolete]
        public async void GoToFeedBack()
        {
            await PopupNavigation.PopAllAsync(true);
            await Navigation.PushAsync(new View.Inspection.Feedback(managerDispatchMob, VehiclwInformation, this));
        }
    }
}