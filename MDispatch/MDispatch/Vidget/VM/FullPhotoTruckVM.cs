using MDispatch.Models;
using MDispatch.NewElement;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
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

        [System.Obsolete]
        public FullPhotoTruckVM(ManagerDispatchMob managerDispatchMob, string idDriver, int indexCurent, INavigation navigation, InitDasbordDelegate initDasbordDelegate = null)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            this.navigation = navigation;
            truckCar = new TruckCar();
            IdDriver = idDriver;
            IndexCurent = indexCurent;
            //NextCommand = new DelegateCommand(NextPage);
            truckCar.GetModalAlert(IndexCurent);
            Init();
        }

        private async void Init()
        {
            await Task.Run(() =>
            {
                NameLayute = truckCar.GetNameTruck(IndexCurent);
                truckCar.Orinteble(IndexCurent);
            });
        }

        public string IdDriver { get; set; }

        private int IndexCurent { get; set; }
        private string nameLayute = null;
        public string NameLayute
        {
            get => nameLayute;
            set => SetProperty(ref nameLayute, value);
        }

        private ImageSource imageSourceTake = null;
        public ImageSource ImageSourceTake
        {
            get => imageSourceTake;
            set => SetProperty(ref imageSourceTake, value);
        }
         
        public Photo Photo { get; set; }

        [System.Obsolete]
        public async Task AddPhoto(byte[] photoRes)
        {
            Photo photo = new Photo();
            photo.Base64 = JsonConvert.SerializeObject(photoRes);
            photo.path = $"../Photo/Driver/{IdDriver}/{IndexCurent}.jpg";
            Photo = photo;
            MemoryStream stream = new MemoryStream(photoRes);
            stream.Position = 0;
            var byteArray = stream.ToArray();
            ImageSourceTake = ImageSource.FromStream(() => new MemoryStream(byteArray));
            await NextPage();
        }

        [System.Obsolete]
        private async Task NextPage()
        {
            bool isNavigationMany = false;
            bool isEndInspection = false;
            if (navigation.NavigationStack.Count > 2)
            {
                await PopupNavigation.PushAsync(new LoadPage());
                isNavigationMany = true;
            }
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                if (IndexCurent < 45)
                {
                    isEndInspection = true;
                    await navigation.PushAsync(new View.CameraPage(managerDispatchMob, IdDriver, IndexCurent + 1, initDasbordDelegate));
                }
                else
                {
                    DependencyService.Get<IOrientationHandler>().ForceSensor();
                    UpdateInspectionDriver();
                }
                await Task.Run(() =>
                {
                    state = managerDispatchMob.AskWork("SaveInspactionDriver", token, IdDriver, Photo, ref description, IndexCurent);
                });
                if (state == 1)
                {
                    if(isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (navigation.NavigationStack.Count > 1)
                    {
                        await navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror("Not Network", null));
                }
                else if (state == 2)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (navigation.NavigationStack.Count > 1)
                    {
                        await navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (isEndInspection)
                    {
                        navigation.RemovePage(navigation.NavigationStack[1]);
                    }
                    DependencyService.Get<IToast>().ShowMessage($"Photo {truckCar.GetNameTruck(IndexCurent)} saved");
                }
                else if (state == 4)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (navigation.NavigationStack.Count > 1)
                    {
                        await navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                    if(IndexCurent > 45)
                    { 
                    await navigation.PopToRootAsync();}
                }
            }
        }

        [System.Obsolete]
        private async void UpdateInspectionDriver()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            await PopupNavigation.PopAsync();
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.DriverWork("UpdateInspectionDriver", token, ref description, IdDriver);
                });
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