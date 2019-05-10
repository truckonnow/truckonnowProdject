using MDispatch.Models;
using MDispatch.NewElement;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.VidgetFolder.View;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using Newtonsoft.Json;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.Vidget.VM
{
    public class FullPhotoTruckVM : BindableBase
    {
        private ManagerDispatchMob managerDispatchMob = null;
        private INavigation navigation = null;
        public TruckCar truckCar = null;
        public DelegateCommand NextCommand { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;

        public FullPhotoTruckVM(ManagerDispatchMob managerDispatchMob, string idDriver, int indexCurent, INavigation navigation, InitDasbordDelegate initDasbordDelegate = null)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            this.navigation = navigation;
            truckCar = new TruckCar();
            IdDriver = idDriver;
            IndexCurent = indexCurent;
            NextCommand = new DelegateCommand(NextPage);
            NameLayute = truckCar.GetNameTruck(IndexCurent);
            truckCar.GetModalAlert(IndexCurent);
            truckCar.Orinteble(IndexCurent);
        }

        public string IdDriver { get; set; }

        private int IndexCurent { get; set; }

        private bool isVisableNext = false;
        public bool IsVisableNext
        {
            get => isVisableNext;
            set => SetProperty(ref isVisableNext, value);
        }

        private bool isVisableAdd = true;
        public bool IsVisableAdd
        {
            get => isVisableAdd;
            set => SetProperty(ref isVisableAdd, value);
        }

        private string nameLayute = null;
        public string NameLayute
        {
            get => nameLayute;
            set => SetProperty(ref nameLayute, value);
        }

        private ImageSource source = null;
        public ImageSource Source
        {
            get => source;
            set => SetProperty(ref source, value);
        }

        private ImageSource imageSource = null;
        public ImageSource ImageSource
        { 
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        private ImageSource imageSourceTake = null;
        public ImageSource ImageSourceTake
        {
            get => imageSourceTake;
            set => SetProperty(ref imageSourceTake, value);
        }

        public Photo Photo { get; set; }

        public void AddPhoto(byte[] photoRes)
        {
            Photo photo = new Photo();
            photo.Base64 = JsonConvert.SerializeObject(photoRes);
            photo.path = $"../Photo/Driver/{IdDriver}/{IndexCurent}.jpg";
            Photo = photo; 
            Stream stream = new MemoryStream(photoRes);
            ImageSourceTake = ImageSource.FromStream(() => { return stream; });
            IsVisableAdd = false;
            IsVisableNext = true;
        }

        [System.Obsolete]
        private async void NextPage()
        {
            bool isEndInspection = false;
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                if (IndexCurent < 45)
                {
                    isEndInspection = true;
                    FullPhotoTruck fullPhotoTruck = new FullPhotoTruck(managerDispatchMob, IdDriver, IndexCurent + 1, initDasbordDelegate);
                    await navigation.PushAsync(fullPhotoTruck);
                }
                else
                {
                    DependencyService.Get<IOrientationHandler>().ForceSensor();
                    UpdateInspectionDriver();
                    initDasbordDelegate.Invoke();
                    await navigation.PopToRootAsync();
                }
                await Task.Run(() =>
                {
                    state = managerDispatchMob.AskWork("SaveInspactionDriver", token, IdDriver, Photo, ref description, IndexCurent);
                });
                if (state == 1)
                {
                    await navigation.PopAsync();
                    await PopupNavigation.PushAsync(new Errror("Not Network", null));
                }
                else if (state == 2)
                {
                    await navigation.PopAsync();
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    if (isEndInspection)
                    {
                        navigation.RemovePage(navigation.NavigationStack[1]);
                    }
                }
                else if (state == 4)
                {
                    await navigation.PopAsync();
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
        }

        [System.Obsolete]
        private async void UpdateInspectionDriver()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.DriverWork("UpdateInspectionDriver", token, ref description, IdDriver);
                });
                await PopupNavigation.PopAsync(true);

                if (state == 1)
                {
                    await PopupNavigation.PushAsync(new Errror("Not Network", null));
                }
                else if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    initDasbordDelegate.Invoke();
                    await navigation.PopToRootAsync();
                    //await Task.Run(() => SetInspectionDriver());
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
        }

        [System.Obsolete]
        private async void SetInspectionDriver()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                //state = managerDispatchMob.DriverWork("SetInspectionDriver", token, ref description, IdDriver, InspectionDriver);
            });
            await PopupNavigation.PopAsync(true);
            if (state == 1)
            {
                await PopupNavigation.PushAsync(new Errror("Not Network", null));
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, null));
            }
            else if (state == 3)
            {
                
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
            }
        }

        
    }
}
